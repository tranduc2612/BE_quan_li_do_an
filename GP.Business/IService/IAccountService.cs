using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.Models.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.IService
{
    public interface IAccountService
    {
        public bool CheckStudent(StudentModel accountDTO, out string message);
        public bool CheckTeacher(TeacherModel accountDTO, out string message);
        public LoginResponseDTO GetProfile(string username);

        /// <summary>
        /// ytutututu
        /// </summary>
        /// <param name="roleCode">vai trò</param>
        /// <returns></returns>
        public bool CheckRole(string roleCode);
        public bool VerifyLoginInfo(AccountLogin login, out string message, out string typeError);
        public void RegisterStudent(StudentModel studentReq);
        public void RegisterTeacher(TeacherModel teacherDTO);
        public string UpdateStudent(StudentModel studentReq);
        public bool DeleteStudent(string username);
        public bool DeleteTeacher(string username);
        public bool CheckValidRefreshToken(string refreshToken, out string message);
        public AccountLogin GetCurrentUsername();
        public LoginResponseDTO CreateToken(AccountLogin login);
        public PaginatedResultBase<StudentDTO> GetListStudent(StudentListModel data);
        public PaginatedResultBase<TeacherDTO> GetListTeacher(TeacherListModel data);
        public StudentDTO GetStudent(string username);
    }
}
