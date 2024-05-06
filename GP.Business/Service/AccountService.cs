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
using System.Reflection;
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
         
            Student student = _studentRepository.Get(_common.GennerationUsernameStudent(studentDTO.StudentCode, studentDTO.SemesterId));
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

            Teacher teacher = _teacherRepository.Get(teacherDTO.UserName);
            if (teacher != null)
            {
                message = "Tài khoản giảng viên này đã được tạo !";
                return true;
            }

            return false;

        }

        public LoginResponseDTO GetProfile(string username) {
            LoginResponseDTO account = null;
            Student student = null;
            Teacher teacher = null;
            student = _studentRepository.Get(username);
            if(student != null)
            {
                account = _mapper.MapStudentToLoginResponseDTO(student);
                return account;
            }
            teacher = _teacherRepository.Get(username);
            if (teacher != null)
            {
                account = _mapper.MapTeacherToLoginResponseDTO(teacher);
                return account;
            }
            return account;

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
            teacher.Password = passwordHash;
            teacher.PasswordSalt = passwordSalt;
            if(teacher.IsAdmin == 1)
            {
                teacher.Role = "ADMIN";
            }
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

            if(check_data?.Status == "BLOCK")
            {
                message = "Tài khoản đã bị khóa";
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
            //Student student = _mapper.MapStudentModelToStudent(studentReq);
            Student find = _studentRepository.Get(studentReq.UserName);
            if(find == null)
            {
                return null;
            }
            find.StudentCode = studentReq.StudentCode != null ? studentReq.StudentCode : find.StudentCode;
            find.FullName= studentReq.FullName != null ? studentReq.FullName : find.FullName;
            find.Phone = studentReq.Phone != null ? studentReq.Phone : find.Phone;
            find.Email= studentReq.Email != null ? studentReq.Email : find.Email;
            find.Address= studentReq.Address != null ? studentReq.Address : find.Address;
            find.Gender = studentReq.Gender != null ? studentReq.Gender : find.Gender;
            find.Dob = studentReq.Dob != null ? studentReq.Dob : find.Dob;
            find.SchoolYearName = studentReq.SchoolYearName != null ? studentReq.SchoolYearName : find.SchoolYearName;
            find.ClassName = studentReq.ClassName != null ? studentReq.ClassName : find.ClassName;
            find.Gpa = studentReq.Gpa != null ? studentReq.Gpa : find.Gpa;
            find.MajorId = studentReq.MajorId != null ? studentReq.MajorId : find.MajorId;
            find.Avatar = studentReq.Avatar != null ? studentReq.Avatar : find.Avatar;
            find.UserNameMentorRegister = studentReq.UserNameMentorRegister != null ? studentReq.UserNameMentorRegister : find.UserNameMentorRegister;
            find.IsFirstTime = studentReq.IsFirstTime != null ? studentReq.IsFirstTime : find.IsFirstTime;
            find.Status = studentReq.Status != null ? studentReq.Status : find.Status;
            if (studentReq.StatusProject != null)
            {
                Project findProject = _projectRepository.GetProjectByUsername(studentReq.UserName);
                findProject.StatusProject = studentReq.StatusProject;
                _projectRepository.Update(findProject);
            }
            _studentRepository.Update(find);
            return find.UserName;
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

        public string UpdateTeacher(TeacherModel teacherReq)
        {
            //Student student = _mapper.MapStudentModelToStudent(studentReq);
            Teacher find = _teacherRepository.Get(teacherReq.UserName);
            if (find == null)
            {
                return null;
            }
            find.FullName = teacherReq.FullName != null ? teacherReq.FullName : find.FullName;
            find.Phone = teacherReq.Phone != null ? teacherReq.Phone : find.Phone;
            find.Email = teacherReq.Email != null ? teacherReq.Email : find.Email;
            find.Address = teacherReq.Address != null ? teacherReq.Address : find.Address;
            find.Gender = teacherReq.Gender != null ? teacherReq.Gender : find.Gender;
            find.Dob = teacherReq.Dob != null ? teacherReq.Dob : find.Dob;
            find.MajorId = teacherReq.MajorId != null ? teacherReq.MajorId : find.MajorId;
            find.Avatar = teacherReq.Avatar != null ? teacherReq.Avatar : find.Avatar;
            find.EducationId = teacherReq.EducationId != null ? teacherReq.EducationId : find.EducationId;
            find.Status = teacherReq.Status != null ? teacherReq.Status : find.Status;
            find.IsAdmin = teacherReq.IsAdmin != null ? teacherReq.IsAdmin : find.IsAdmin;
            if (find.IsAdmin == 1)
            {
                find.Role = "ADMIN";
            }
            _teacherRepository.Update(find);
            return find.UserName;
        }

        public bool ChangePassword(ChangePassword login, out string message)
        {
            if (login.Role == "STUDENT")
            {
                Student student = _studentRepository.Get(login.UserName);
                if(student == null)
                {
                    message = "Sinh viên không hợp lệ !";
                    return false;
                }
                if (!AuthHelper.VerifyPasswordHash(login.PasswordOld, student.Password, student.PasswordSalt))
                {
                    message = "Bạn đã nhập sai mật khẩu";
                    return false;
                }
                AuthHelper.CreatePassHash(login.PasswordNew, out byte[] passwordHash, out byte[] passwordSalt);
                student.Password = passwordHash;
                student.PasswordSalt = passwordSalt;
                _studentRepository.Update(student);
                message = "Đổi mật khẩu thành công !";
                return true;
            }
            if (login.Role == "TEACHER")
            {
                Teacher teacher =_teacherRepository.Get(login.UserName);
                if (teacher == null)
                {
                    message = "Giảng viên không hợp lệ !";
                    return false;
                }
                if (!AuthHelper.VerifyPasswordHash(login.PasswordOld, teacher.Password, teacher.PasswordSalt))
                {
                    message = "Bạn đã nhập sai mật khẩu";
                    return false;
                }
                AuthHelper.CreatePassHash(login.PasswordNew, out byte[] passwordHash, out byte[] passwordSalt);
                teacher.Password = passwordHash;
                teacher.PasswordSalt = passwordSalt;
                _teacherRepository.Update(teacher);
                message = "Đổi mật khẩu thành công !";
                return true;
            }
            message = "Tài khoản không tồn tại !";
            return false;
        }

        public async Task<bool> ChangeAvatar(ChangeAvatar model)
        {
            if (model.Role == "STUDENT")
            {
                Student student = _studentRepository.Get(model.UserName);
                if (student == null)
                {
                    //message = "Sinh viên không hợp lệ !";
                    return false;
                }
                var filePath = Path.Combine("file", "user","student",student.UserName);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                var typeFile = model.file.ContentType;
                filePath = Path.Combine("file", "user", "student", student.UserName, "avatar_"+ DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(model.file.FileName));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.file.CopyToAsync(stream);
                }
                student.Avatar = filePath;
                student.TypeFileAvatar = typeFile;
                _studentRepository.Update(student);
                //message = "Cập nhật ảnh đại diện thành công !";
                return true;
            }
            if (model.Role == "TEACHER")
            {
                Teacher teacher = _teacherRepository.Get(model.UserName);
                if (teacher == null)
                {
                    //message = "Giảng viên không hợp lệ !";
                    return false;
                }
                var filePath = Path.Combine("file", "user", "teacher",teacher.UserName);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                filePath = Path.Combine("file", "user", "teacher", teacher.UserName, "avatar_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(model.file.FileName));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.file.CopyToAsync(stream);
                }
                var typeFile = model.file.ContentType;
                teacher.Avatar = filePath;
                teacher.TypeFileAvatar = typeFile;
                _teacherRepository.Update(teacher);
                //message = "Cập nhật ảnh đại diện thành công !";
                return true;
            }
            //message = "Tài khoản không tồn tại";
            return false;

        }

        public byte[] GetFileAvatar(string username, string role, out string image_type)
        {
            if (role == "STUDENT")
            {
                Student student = _studentRepository.Get(username);
                if (student == null)
                {
                    image_type = "";
                    return null;
                }
                if (!System.IO.File.Exists(student.Avatar))
                {
                    image_type = "";
                    return null;
                }

                var imageBytes = System.IO.File.ReadAllBytes(student.Avatar);
                image_type = student.TypeFileAvatar;
                return imageBytes;
            }
            if (role == "TEACHER")
            {
                Teacher teacher = _teacherRepository.Get(username);
                if (teacher == null)
                {
                    image_type = "";
                    return null;
                }
                if (!System.IO.File.Exists(teacher.Avatar))
                {
                    image_type = "";
                    return null;
                }

                var imageBytes = System.IO.File.ReadAllBytes(teacher.Avatar);
                image_type = teacher.TypeFileAvatar;
                return imageBytes;
            }
            image_type = "";
            return null;
        }

        public LoginResponseDTO VerifyUserName(CheckUserName check, out string message, out string typeError)
        {
            if (check.Role == "STUDENT")
            {
                LoginResponseDTO studentCheck = _mapper.MapStudentToLoginResponseDTO(_studentRepository.Get(check.UserName));
                if(studentCheck == null)
                {
                    typeError = "username";
                    message = "Tài khoản không tồn tại !";
                    return null;
                }
                typeError = "";
                message = "";
                return studentCheck;
            }

            if (check.Role == "TEACHER")
            {
                LoginResponseDTO studentCheck = _mapper.MapTeacherToLoginResponseDTO(_teacherRepository.Get(check.UserName));
                if (studentCheck == null)
                {
                    typeError = "username";
                    message = "Tài khoản không tồn tại !";
                    return null;
                }
                typeError = "";
                message = "";
                return studentCheck;
            }
            typeError = "role";
            message = "Vai trò không hợp lệ";
            return null;
        }

        public bool ForgotPassword(ChangePassword login, out string message)
        {
            if (login.Role == "STUDENT")
            {
                Student student = _studentRepository.Get(login.UserName);
                if (student == null)
                {
                    message = "Sinh viên không hợp lệ !";
                    return false;
                }
                AuthHelper.CreatePassHash(login.PasswordNew, out byte[] passwordHash, out byte[] passwordSalt);
                student.Password = passwordHash;
                student.PasswordSalt = passwordSalt;
                _studentRepository.Update(student);
                message = "Đổi mật khẩu thành công !";
                return true;
            }
            if (login.Role == "TEACHER")
            {
                Teacher teacher = _teacherRepository.Get(login.UserName);
                if (teacher == null)
                {
                    message = "Giảng viên không hợp lệ !";
                    return false;
                }
                AuthHelper.CreatePassHash(login.PasswordNew, out byte[] passwordHash, out byte[] passwordSalt);
                teacher.Password = passwordHash;
                teacher.PasswordSalt = passwordSalt;
                _teacherRepository.Update(teacher);
                message = "Đổi mật khẩu thành công !";
                return true;
            }
            message = "Tài khoản không tồn tại !";
            return false;
        }
    }
}
