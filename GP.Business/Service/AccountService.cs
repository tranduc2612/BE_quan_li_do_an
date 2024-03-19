using Azure;
using Azure.Identity;
using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.DAL.Repository;
using GP.Models.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace GP.Business.Service
{
    public class AccountService : IAccountService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly IProjectOutlineRepository _projectOutlineRepository;
        private readonly IClassificationRepository _classificationRepository;
        private readonly AuthHelper _authHelper;
        private readonly HelperCommon _common;
        private readonly MappingProfile _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(IClassificationRepository classificationRepository, IStudentRepository studentRepository, AuthHelper authHelper, IHttpContextAccessor httpContextAccessor, ITeacherRepository teacherRepository, IProjectRepository projectRepository, ISemesterRepository semesterRepository, MappingProfile mapper, HelperCommon common, IProjectOutlineRepository projectOutlineRepository)
        {
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _projectRepository = projectRepository;
            _semesterRepository = semesterRepository;
            _projectOutlineRepository = projectOutlineRepository;
            _classificationRepository = classificationRepository;
            _authHelper = authHelper;
            _mapper = mapper;
            _common = common;
            _httpContextAccessor = httpContextAccessor;
            _projectOutlineRepository = projectOutlineRepository;
        }

        /// <summary>
        /// Kiểm tra thông tin đăng ký đã tồn tại chưa
        /// </summary>
        /// <param name="studentDTO"></param>
        /// <returns></returns>
        public bool CheckStudent(StudentModel studentDTO, out string message)
        {
            message = string.Empty;
         
            Student student = _studentRepository.Get(_common.GennerationUsernameStudent(studentDTO.FullName, studentDTO.StudentCode, studentDTO.SemesterId));
            Semester semester = _semesterRepository.GetById(studentDTO.SemesterId);
            if (semester == null)
            {
                message = "Học kỳ này không tồn tại !";
                return true;
            }
            if (student != null)
            {
                message = "Tài khoản sinh viên này đã được tạo !";
                return true;
            }

            return false;
        }

        public bool CheckTeacher(TeacherModel teacherDTO, out string message)
        {
            message = string.Empty;

            Teacher teacher = _teacherRepository.Get(_common.GennerationUsernameTeacher(teacherDTO.FullName, teacherDTO.TeacherCode));
            if (teacher != null)
            {
                message = "Tài khoản giảng viên này đã được tạo !";
                return true;
            }

            return false;

        }

        public LoginResponseDTO CreateToken(AccountLogin login)
        {
            LoginResponseDTO account = null;
            Student student = null;
            Teacher teacher = null;
            if (login.Role == "STUDENT")
            {
                student = _studentRepository.Get(login.UserName);
                account = _mapper.MapStudentToLoginResponseDTO(student);
            }else if (login.Role == "TEACHER")
            {
                teacher = _teacherRepository.Get(login.UserName);
                account = _mapper.MapTeacherToLoginResponseDTO(teacher);
            } 
            if(account == null)
            {
                return null;
            }
            account.Role = login.Role;
            string token = _authHelper.CreateToken(account);
            account.Token = token;

            // Generate and set refresh token (như hàm GenAndSetRefreshToken bên dưới nhưng ko set vào cookies)
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            // cập nhật thông tin user 
            if (account != null)
            {
                account.RefreshToken = refreshToken.Token;
                account.TokenCreated = refreshToken.Created;
                account.TokenExpires = refreshToken.Expires;

                if (login.Role == "STUDENT")
                {
                    student.RefreshToken = refreshToken.Token;
                    student.TokenCreated = refreshToken.Created;
                    student.TokenExpires = refreshToken.Expires;
                    _studentRepository.Update(student);
                }
                if (login.Role == "TEACHER")
                {
                    teacher.RefreshToken = refreshToken.Token;
                    teacher.TokenCreated = refreshToken.Created;
                    teacher.TokenExpires = refreshToken.Expires;
                    _teacherRepository.Update(teacher);
                }
            }


            return account;
        }

        // Giống với bên AuthHelper.cs
        public AccountLogin GetCurrentUsername()
        {
            var username = string.Empty;
            var role = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                username = _httpContextAccessor.HttpContext.User.FindFirstValue("username");        
            }
            if (_httpContextAccessor.HttpContext != null)
            {
                role = _httpContextAccessor.HttpContext.User.FindFirstValue("rolee");
            }
            return new AccountLogin {
                UserName = username,
                Role = role
            };
        }

        public bool CheckValidRefreshToken(string refreshToken, out string message)
        {
            message = string.Empty;
            AccountLogin currentClamn = GetCurrentUsername();

            LoginResponseDTO account = null;
            if(currentClamn.Role == "STUDENT" && currentClamn.UserName !=null)
            {
                account = _mapper.MapStudentToLoginResponseDTO(_studentRepository.Get(currentClamn.UserName));
            }else if(currentClamn.Role == "TEACHER" && currentClamn.UserName != null)
            {
                account = _mapper.MapTeacherToLoginResponseDTO(_teacherRepository.Get(currentClamn.UserName));
            }
            else
            {
                account = new LoginResponseDTO();
            }


            if (account.RefreshToken == null || !account.RefreshToken.Equals(refreshToken))
            {
                message = "Invalid Refresh Token.";
                return false;
            }
            else if (account.TokenExpires < DateTime.Now)
            {
                message = "Toke expired.";
                return false;
            }

            return true;
        }

        public void RegisterStudent(StudentModel studentDTO)
        {
            _studentRepository.Create(studentDTO);
        }
        public void RegisterTeacher(TeacherModel teacherModel)
        {
            Teacher teacher = _mapper.MapTeacherModelToTeacher(teacherModel);
            AuthHelper.CreatePassHash(teacherModel.PasswordText, out byte[] passwordHash, out byte[] passwordSalt);
            teacher.IsAdmin = 0;
            teacher.Password = passwordHash;
            teacher.PasswordSalt = passwordSalt;
            teacher.UserName = _common.GennerationUsernameTeacher(teacherModel.FullName, teacherModel.TeacherCode);
            _teacherRepository.Create(teacher);

        }

        public bool VerifyLoginInfo(AccountLogin login, out string message, out string typeError)
        {
            dynamic check_data = null;
            if (login.Role == "STUDENT")
            {
                check_data = _studentRepository.Get(login.UserName);
            }

            if (login.Role == "TEACHER")
            {
                check_data = _teacherRepository.Get(login.UserName);
            }

            if (check_data == null)
            {
                message = "Tài khoản không tồn tại";
                typeError = "username";
                return false;
            }

            if (!AuthHelper.VerifyPasswordHash(login.Password, check_data.Password, check_data.PasswordSalt))
            {
                message = "Sai mật khẩu";
                typeError = "password";
                return false;
            }
            typeError = "username";
            message = "Thông tin đăng nhập đúng";
            return true;
        }


        public bool CheckRole(string roleCode)
        {
            Classification class_role = _classificationRepository.GetByCode(roleCode);
            if(class_role == null)
            {
                return false;
            }
            return true;
        }

        

        public PaginatedResultBase<StudentDTO> GetListStudent(StudentListModel data)
        {
            return _studentRepository.GetList(data);
        }

        public PaginatedResultBase<TeacherDTO> GetListTeacher(TeacherListModel data)
        {
            return _teacherRepository.GetList(data);
        }

        public string UpdateStudent(StudentModel studentReq)
        {
            Student student = _mapper.MapStudentModelToStudent(studentReq);
            _studentRepository.Update(student);
            return student.UserName;
        }

        public bool DeleteStudent(string username)
        {
            bool isDelete = _studentRepository.Delete(username);
            return isDelete;
        }

        public StudentDTO GetStudent(string username)
        {
            Student student = _studentRepository.Get(username);

            StudentDTO studentDTO = _mapper.MapStudentToStudentDTO(student);

            return studentDTO;
        }

        public bool DeleteTeacher(string username)
        {
            bool isDelete = _teacherRepository.Delete(username);
            return isDelete;
        }
    }
}
