using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IAccountService _accountService;


        public TeacherController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // lấy thông tin tài khoản dăng nhập
        [HttpPost("get-list-teacher")]
        public Response GetListStudent([FromBody] TeacherListModel req)
        {
            Response response = new Response();

            try
            {
                //var currentAccount = _accountService.GetInfoAccount(username);
                response.Msg = "Sucess";
                response.Code = 200;
                response.ReturnObj = _accountService.GetListTeacher(req);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpDelete("delete-teacher")]
        public Response DeleteTeacher(string username)
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
                bool UserName = _accountService.DeleteTeacher(username);
                if (UserName)
                {
                    response.ReturnObj = null;
                    response.Msg = "Xóa giảng viên thành công !";
                    response.Code = 200;
                }
                else
                {
                    response.SetError(400, "Xóa giảng viên thất bại !");
                }
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpPut("update-teacher")]
        public Response UpdateTeacher(TeacherModel model)
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
                string UserName = _accountService.UpdateTeacher(model);
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
