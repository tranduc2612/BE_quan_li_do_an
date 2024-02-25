using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Models.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet, Authorize]
        public Response GetHeaderUserInfo()
        {
            Response response = new Response();

            try
            {
                AccountDTO currentAccount = _accountService.GetCurrentAccount();
                response.Msg = "Sucess";
                response.Code= 200;
                response.ReturnObj = currentAccount;
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }
    }
}
