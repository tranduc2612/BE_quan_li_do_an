using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Globalization;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly HelperCommon _common;

        public AuthController(IAccountService accountService, HelperCommon common)
        {
            _accountService = accountService;
            _common = common;
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


        [HttpPost("register-student-list-excel")]
        public Response RegisterStudentExcel(IFormFile file,string idSemester)
        {
            Response response = new Response();
            if (file == null || file.Length == 0)
            {
                response.SetError("File không hợp lệ !");
                return response;
            }
            if (!_common.IsExcelFile(file.FileName))
            {
                response.SetError("File không đúng định dạng !");
                return response;
            }
            string str = "";
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                // Ví dụ đọc dữ liệu từ file Excel
                using (var stream = file.OpenReadStream())
                {
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0]; // Lấy worksheet đầu tiên

                        // Lặp qua các cell trong worksheet và đọc dữusing OfficeOpenXml; liệu
                        for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                        {
                            var cellValue = worksheet.Cells[row, 2].Value;
                            StudentModel newStudent = new StudentModel();
                            newStudent.StudentCode = worksheet.Cells[row, 2].Value?.ToString();
                            newStudent.FullName = worksheet.Cells[row, 3].Value?.ToString();
                            newStudent.SemesterId = idSemester;

                            str += newStudent.FullName + " " + worksheet.Cells[row, 4].Value;

                            string dateString = worksheet.Cells[row, 4].Value?.ToString();
                            string format = "dd/MM/yyyy";
                            if(dateString != null)
                            {
                                newStudent.PasswordText = worksheet.Cells[row, 4].Value?.ToString();
                                DateTime.TryParse(dateString,out DateTime dateTime);
                                newStudent.Dob = dateTime;
                            }
                            else
                            {
                                newStudent.PasswordText = worksheet.Cells[row, 2].Value?.ToString();
                            }
                            newStudent.Address = worksheet.Cells[row, 5].Value?.ToString();
                            newStudent.Gender = worksheet.Cells[row, 6].Value?.ToString() == "Nữ" ? 1 : 0;
                            newStudent.ClassName = worksheet.Cells[row, 7].Value?.ToString();
                            newStudent.SchoolYearName = worksheet.Cells[row, 8].Value?.ToString();
                            newStudent.Phone = worksheet.Cells[row, 9].Value?.ToString();
                            newStudent.Email = worksheet.Cells[row, 10].Value?.ToString();
                            Double.TryParse(worksheet.Cells[row, 11].Value?.ToString(),out double gpa);
                            newStudent.Gpa = gpa;
                            if(newStudent.StudentCode != null && newStudent.FullName != null)
                            {
                                if (!_accountService.CheckStudent(newStudent, out string message))
                                {
                                    _accountService.RegisterStudent(newStudent);
                                }
                            }
                            
                            //for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                            //{
                            //    var cellValue = worksheet.Cells[row, col].Value;
                            //    // kiểm tra đã tồn tại user trong hệ thống chưa


                            //    //newStudent
                            //    //if (_accountService.CheckStudent(studentDTO, out string message))
                            //    //{
                            //    //    response.SetError(message);
                            //    //    return response;
                            //    //}

                            //    //response.Msg = "Đăng ký thành công !";
                            //    //response.Code = 201;
                            //    //// đăng kí: hash pass, tạo user mới ...
                            //    //_accountService.RegisterStudent(studentDTO);
                            //    str += cellValue;
                            //}
                        }
                    }
                }
                response.Msg = "Thêm thành công !";
                    response.Code = 201;
                    response.Success = true;
                    response.ReturnObj = str;
                return response;
                

            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
                response.ReturnObj = str;
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
