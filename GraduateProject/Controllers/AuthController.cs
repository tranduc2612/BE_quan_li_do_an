using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Wordprocessing;
using GP.Business.IService;
using GP.Business.Service;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.Models.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OfficeOpenXml;
using System;
using System.Globalization;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ISemesterService _semesterService;

        private readonly HelperCommon _common;

        public AuthController(IAccountService accountService, ISemesterService semesterService, HelperCommon common)
        {
            _accountService = accountService;
            _semesterService= semesterService;
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

        [HttpPost("change-password")]
        public Response ChangePassword([FromBody] ChangePassword model)
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

                if (!_accountService.ChangePassword(model, out string message))
                {
                    response.SetError(message);
                    return response;
                }

                response.Msg = message;
                response.Code = 200;
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }


        [HttpPost("forgot-password")]
        public Response ForGotPassword([FromBody] ChangePassword model)
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

                if (!_accountService.ForgotPassword(model, out string message))
                {
                    response.SetError(message);
                    return response;
                }

                response.Msg = message;
                response.Code = 200;
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        private bool IsImage(string contentType)
        {
            return contentType.StartsWith("image/");
        }

        [HttpPost("change-avatar")]
        public async Task<Response> ChangeAvatar([FromForm] ChangeAvatar model)
        {
            Response response = new Response();

            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Lỗi tham số đầu vào");
                return response;
            }
            if (model.file == null || model.file.Length == 0)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Lỗi tham số đầu vào");
                return response;
            }
            if (!IsImage(model.file.ContentType))
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Chỉ chập nhận file ảnh");
                return response;
            }

            try
            {
                bool check = await _accountService.ChangeAvatar(model);
                if (!check)
                {
                    response.SetError("Lỗi");
                    return response;
                }

                response.Msg = "";
                response.Code = 200;
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpGet("avatar")]
        public IActionResult GetImageAvatar(string role, string username)
        {
            var namefilePath = _accountService.GetFileAvatar(username, role, out string type_image);
            if (namefilePath == null)
            {
                return NotFound();
            }
            return File(namefilePath, type_image);
        }


        [HttpPost("register-student-list-excel")]
        public IActionResult RegisterStudentExcel([FromForm] ListStudentModel model)
        {
            Response response = new Response();
            if (model.file == null || model.file.Length == 0)
            {
                response.SetError("File không hợp lệ !");
                return BadRequest();
            }
            if (!_common.IsExcelFile(model.file.FileName))
            {
                response.SetError("File không đúng định dạng !");
                return BadRequest(response.Msg);
            }
            Semester semester = _semesterService.getSemester(model.SemesterId);
            if (semester == null)
            {
                response.SetError("Không tồn tại học kỳ !");
                return BadRequest(response.Msg);
            }
            string str = "";
            try
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    List<StudentModel> errorList = new List<StudentModel>();
                    var ws = wb.AddWorksheet("DanhSachSinhVienLoi");
                    ws.Cell("A1").Value = "DANH SÁCH SINH VIÊN ĐĂNG KÝ LỖI";
                    ws.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Range("A1:E1").Row(1).Merge().Style.Font.Bold = true;
                    ws.Cell("A1").Style.Font.FontName = "Times New Roman";
                    ws.Cell("A1").Style.Font.FontSize = 20;

                    ws.Column("A").Width = 10;
                    ws.Column("B").Width = 20;
                    ws.Column("C").Width = 40;
                    ws.Column("D").Width = 20;
                    ws.Column("E").Width = 40;

                    int Cell = 1;
                    //Dùng để bo viền
                    int Coll = 1;

                    ws.Range("A1:E1").Row(2).Merge();
                    ws.Cell(2, 1).Value = $"Mã học kỳ: {semester.SemesterId}";
                    ws.Row(2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Row(2).Style.Font.FontName = "Times New Roman";
                    ws.Range("A1:E1").Row(2).Merge().Style.Font.Bold = true;
                    ws.Row(2).Style.Font.FontSize = 13;
                    Cell = 3;
                    Coll = 3;

                    System.Data.DataTable dataError = new System.Data.DataTable();
                    dataError.Columns.Add("STT", typeof(string));
                    dataError.Columns.Add("Mã sinh viên",typeof(string));
                    dataError.Columns.Add("Họ và tên", typeof(string));
                    dataError.Columns.Add("Ngày sinh", typeof(string));
                    dataError.Columns.Add("Thông tin lỗi", typeof(string));

                    for (var i = 0; i < dataError.Columns.Count; i++)
                    {
                        ws.Cell(Cell, i + 1).Value = dataError.Columns[i].ToString();
                    }

                    ws.Row(3).Style.Font.Bold = true;
                    ws.Row(3).Style.Alignment.WrapText = true;
                    ws.Row(3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    ws.Row(3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Row(3).Style.Font.FontSize = 12;
                    ws.Row(3).Style.Font.FontName = "Times New Roman";
                    Cell++;

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                // Ví dụ đọc dữ liệu từ file Excel
                using (var stream = model.file.OpenReadStream())
                {
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0]; // Lấy worksheet đầu tiên

                        // Lặp qua các cell trong worksheet và đọc dữusing OfficeOpenXml; liệu
                        for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                        {
                            var cellValue = worksheet.Cells[row, 2].Value;
                            string msgError = "";
                            
                            StudentModel newStudent = new StudentModel();
                            newStudent.StudentCode = worksheet.Cells[row, 2].Value?.ToString();
                            newStudent.FullName = worksheet.Cells[row, 3].Value?.ToString();
                            newStudent.SemesterId = model.SemesterId;
                            str += newStudent.FullName + " " + worksheet.Cells[row, 4].Value;

                            string dateString = worksheet.Cells[row, 4].Value?.ToString();
                            string format = "dd/MM/yyyy";
                            newStudent.DOBOriginal = dateString;


                            newStudent.Address = worksheet.Cells[row, 5].Value?.ToString();
                            newStudent.Gender = worksheet.Cells[row, 6].Value?.ToString() == "Nữ" ? 1 : 0;
                            newStudent.ClassName = worksheet.Cells[row, 7].Value?.ToString();
                            newStudent.SchoolYearName = worksheet.Cells[row, 8].Value?.ToString();
                            newStudent.Phone = worksheet.Cells[row, 9].Value?.ToString();
                            newStudent.Email = worksheet.Cells[row, 10].Value?.ToString();
                            Double.TryParse(worksheet.Cells[row, 11].Value?.ToString(),out double gpa);
                            newStudent.Gpa = gpa;
                            if (newStudent.StudentCode == null && newStudent.FullName == null)
                            {
                                continue;
                            }

                            if (newStudent.StudentCode == null)
                            {
                                newStudent.MsgError = "Mã sinh viên không hợp lệ";
                                errorList.Add(newStudent);
                                continue;
                                }

                            if (dateString != null && DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
                            {
                                newStudent.PasswordText = worksheet.Cells[row, 4].Value?.ToString();
                                newStudent.Dob = dateTime;
                            }
                            else
                            {
                                newStudent.MsgError = "Ngày tháng năm sinh không hợp lệ";
                                errorList.Add(newStudent);
                                continue;
                            }
                            

                            if(newStudent.StudentCode != null && newStudent.FullName != null)
                            {
                                if (!_accountService.CheckStudent(newStudent, out string message))
                                {
                                    _accountService.RegisterStudent(newStudent);
                                }
                                else
                                {
                                    newStudent.MsgError = "Tài khoản này đã tồn tại !";
                                    errorList.Add(newStudent);
                                    continue;
                                    }
                                }
                        }
                    }
                }
                int index = 1;
                foreach (StudentModel error in errorList)
                {
                    int row = 0;
                    ws.Cell(Cell, row += 1).Value = index++;
                    ws.Cell(Cell, row += 1).Value = error.StudentCode;
                    ws.Cell(Cell, row += 1).Value = error.FullName;
                    ws.Cell(Cell, row += 1).Value = error.DOBOriginal;
                    //ws.Cell(Cell, row += 1).Value = error.Address;
                    //ws.Cell(Cell, row += 1).Value = error.Gender == 1 ? "Nữ":"Nam";
                    //ws.Cell(Cell, row += 1).Value = error.ClassName;
                    //ws.Cell(Cell, row += 1).Value = error.SchoolYearName;
                    //ws.Cell(Cell, row += 1).Value = error.Phone;
                    //ws.Cell(Cell, row += 1).Value = error.Email;
                    //ws.Cell(Cell, row += 1).Value = error.Gpa;
                    ws.Cell(Cell, row += 1).Value = error.MsgError;
                    Cell++;
                }
                int lastCellRow = Cell - 1;
                ws.Range(Coll, 1, lastCellRow, dataError.Columns.Count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                ws.Range(Coll, 1, lastCellRow, dataError.Columns.Count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                ws.Range(Coll, 1, lastCellRow, dataError.Columns.Count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                ws.Range(Coll, 1, lastCellRow, dataError.Columns.Count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Range(Coll, 1, lastCellRow, dataError.Columns.Count).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.Range(Coll, 1, lastCellRow, dataError.Columns.Count).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                ws.Range(Coll, 1, lastCellRow, dataError.Columns.Count).Style.Font.FontName = "Times New Roman";
                ws.Range(Coll, 1, lastCellRow, dataError.Columns.Count).Style.Font.FontSize = 13;
                ws.Range(Coll, 1, lastCellRow, dataError.Columns.Count).Style.Alignment.WrapText = true;
                //ws.Range("D" + Coll + ":C5000").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    if(errorList.Count > 0)
                        {
                            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sample.xlsx");
                        }
                        else
                        {
                            return Ok();
                        }
                }
                }
                

            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
                response.ReturnObj = str;
                return BadRequest();
            }
        }


        [HttpGet("get-file-mau-add-student")]
        public IActionResult DownloadFileScheduleWeek()
        {

            try
            {
                string path = System.IO.Path.Combine("file","template","add_student", "sample_add_student.xlsx");
                if (System.IO.File.Exists(path))
                {
                    byte[] fileData = System.IO.File.ReadAllBytes(path);
                    // Lấy tên file từ đường dẫn
                    string fileName = System.IO.Path.GetFileName(path);

                    // Trả về file dưới dạng phản hồi HTTP để tải xuống
                    return File(fileData, "application/octet-stream", fileName);
                }
                else
                {
                    return NotFound();
                }
                

            }
            catch (Exception ex)
            {
                return NotFound();
            }
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

        /// <summary>
        /// 788878
        /// </summary>
        /// <param name="check">aksnansdaksdnlkasdnkads</param>
        /// <returns></returns>
        [HttpPost("check-user")]
        public Response CheckUserName([FromBody] CheckUserName check)
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
                if (!_accountService.CheckRole(check.Role))
                {
                    response.SetError("Quyền trong hệ thống không hợp lệ !");
                    return response;
                }
                // Nếu thông tin đăng nhập ko đúng
                LoginResponseDTO checkUserName = _accountService.VerifyUserName(check, out string message, out string typeError);
                if (checkUserName == null)
                {
                    response.SetError(message);
                    response.ReturnObj = new
                    {
                        typeError = typeError,
                        messageError = message
                    };
                    return response;
                }
                else
                {
                    response.ReturnObj = new
                    {
                        typeError = typeError,
                        messageError = message,
                        data = checkUserName
                    };
                }
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
        [HttpGet("get-profile")]
        public Response GetProfile(string username)
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
                //var currentAccount = _accountService.GetInfoAccount(username);
                response.Msg = "Sucess";
                response.Code = 200;
                response.ReturnObj = _accountService.GetProfile(username);
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
