using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register-teacher")]
        public Response RegisterTeacher([FromBody] TeacherModel teacherDTO)
        {
            Response response = new Response();

            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Lỗi tham số đầu vào");
                return response;
            }

            try
            {
                // kiểm tra đã tồn tại user trong hệ thống chưa
                if(_accountService.CheckTeacher(teacherDTO, out string message))
                {
                    response.SetError(message);
                    return response;
                }

                response.Msg = "Đăng ký thành công !";
                // đăng kí: hash pass, tạo user mới ...
                _accountService.RegisterTeacher(teacherDTO);
            } catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpPost("register-student")]
        public Response RegisterStudent([FromBody] StudentModel studentDTO)
        {
            Response response = new Response();

            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Lỗi tham số đầu vào");
                return response;
            }

            try
            {
                // kiểm tra đã tồn tại user trong hệ thống chưa
                if (_accountService.CheckStudent(studentDTO, out string message))
                {
                    response.SetError(message);
                    return response;
                }

                response.Msg = "Đăng ký thành công !";
                response.Code = 201;
                // đăng kí: hash pass, tạo user mới ...
                _accountService.RegisterStudent(studentDTO);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        /// <summary>
        /// 788878
        /// </summary>
        /// <param name="login">aksnansdaksdnlkasdnkads</param>
        /// <returns></returns>
        [HttpPost("login")]
        public Response Login([FromBody] AccountLogin login)
        {
            Response response = new Response();
            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Lỗi tham số đầu vào");
                return response;
            }
            try
            {
                if (!_accountService.CheckRole(login.Role))
                {
                    response.SetError("Quyền trong hệ thống không hợp lệ !");
                    return response;
                }
                // Nếu thông tin đăng nhập ko đúng
                if (!_accountService.VerifyLoginInfo(login, out string message, out string typeError))
                {
                    response.SetError("Sai thông tin đăng nhập");
                    response.ReturnObj = new
                    {
                        typeError = typeError,
                        messageError = message
                    };
                    return response;
                }
                

                LoginResponseDTO account_dto = _accountService.CreateToken(login);

                response.ReturnObj = account_dto;
                response.Msg = "Đăng nhập thành công !";
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            
            return response;
        }


        [HttpPost("refresh-token"), Authorize]
        public Response RefreshToken([FromBody] string refreshToken)
        {
            Response response = new Response();
            //var refreshToken = Request.Cookies["refreshToken"];
            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Lỗi tham số đầu vào");
                return response;
            }
            try
            {
                if (!_accountService.CheckValidRefreshToken(refreshToken, out string message))
                {
                    response.SetError(message);
                    return response;
                }

                AccountLogin curentUsername = _accountService.GetCurrentUsername();

                LoginResponseDTO accountDTO = _accountService.CreateToken(curentUsername);
                //string token = _accountService.CreateToken(curentUsername);
                //_accountService.GenAndSetRefreshToken(Response);

                //response.ReturnObj = token;
                response.ReturnObj = accountDTO;
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            response.Msg = "Refresh token sucess";
            return response;
        }

    }
}
