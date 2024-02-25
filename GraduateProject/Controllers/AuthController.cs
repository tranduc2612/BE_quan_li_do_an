using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
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

        [HttpPost("register")]
        public Response Register(AccountDTO accountDTO)
        {
            Response response = new Response();

            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Validate Error");
                return response;
            }

            try
            {
                // Nếu username hoặc email đã tồn tại
                if(_accountService.CheckUserExist(accountDTO, out string message))
                {
                    response.SetError(message);
                    return response;
                }

                response.Msg = "Register sucess";

                // đăng kí: hash pass, tạo user mới ...
                _accountService.Register(accountDTO);
            } catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }
        
        [HttpPost("login")]
        public Response Login(AccountLogin account)
        {
            Response response = new Response();
            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Validate Error");
                return response;
            }
            try
            {
                // Nếu thông tin đăng nhập ko đúng
                if (!_accountService.VerifyLoginInfo(account.UserName, account.Password, out string message))
                {
                    response.SetError("Sai thông tin đăng nhập");
                    response.ReturnObj = new
                    {
                        typeError = "username",
                        messageError = message
                    };
                    return response;
                }

                AccountDTO account_dto = _accountService.CreateToken(account.UserName);

                response.ReturnObj = account_dto;
                response.Msg = "Login sucess";
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
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Validate Error");
                return response;
            }
            try
            {
                if (!_accountService.CheckValidRefreshToken(refreshToken, out string message))
                {
                    response.SetError(message);
                    return response;
                }

                string curentUsername = _accountService.GetCurrentUsername();

                AccountDTO accountDTO = _accountService.CreateToken(curentUsername);
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
