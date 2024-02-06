using GP.Business.IService;
using GP.Common.Helpers;
using GP.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ICreditService _creditService;

        public CreditController(IAccountService accountService, ICreditService creditService)
        {
            _accountService = accountService;
            _creditService = creditService;
        }

        [HttpPost, Authorize]
        public Response GetListCreditByUser(SearchBase searchBase)
        {
            Response response = new Response();

            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status400BadRequest, "Validate Error");
                return response;
            }
            try
            {
                response.ReturnObj = _creditService.GetCreditByUser(searchBase);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex;
            }
            response.Msg = "Success";
            return response;
        }
    }
}
