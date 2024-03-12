using GP.Business.IService;
using GP.Common.Helpers;
using GP.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public StudentController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // lấy thông tin tài khoản dăng nhập
        [HttpGet("get-student")]
        public Response GetListStudent(string username)
        {
            Response response = new Response();

            try
            {
                //var currentAccount = _accountService.GetInfoAccount(username);
                response.Msg = "Sucess";
                response.Code = 200;
                response.ReturnObj = _accountService.GetStudent(username);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        // lấy thông tin tài khoản dăng nhập
        [HttpPost("get-list-student")]
        public Response GetListStudent([FromBody] StudentListModel req)
        {
            Response response = new Response();

            try
            {
                //var currentAccount = _accountService.GetInfoAccount(username);
                response.Msg = "Sucess";
                response.Code = 200;
                response.ReturnObj = _accountService.GetListStudent(req);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpDelete("delete-student")]
        public Response DeleteStudent(string username)
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
                bool UserName = _accountService.DeleteStudent(username);
                if (UserName)
                {
                    response.ReturnObj = null;
                    response.Msg = "Xóa sinh viên thành công !";
                    response.Code = 200;
                }
                else
                {
                    response.SetError(400, "Xóa sinh viên thất bại !");
                }
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }


        [HttpPut("update-student")]
        public Response UpdateStudent([FromBody] StudentModel studentDTO)
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
                string UserName = _accountService.UpdateStudent(studentDTO);
                if (UserName != null)
                {
                    response.ReturnObj = UserName;
                    response.Msg = "Cập nhật thành công !";
                    response.Code = 200;
                }
                else
                {
                    response.SetError(400, "Cập nhật thất bại !");
                }
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
