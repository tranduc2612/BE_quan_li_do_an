using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using GP.Business.IService;
using GP.Business.Service;
using GP.Common.DTO;
using GP.Common.Models;
using GP.Models.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Globalization;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ICouncilService _councilService;


        public ReportController(IAccountService accountService, ICouncilService councilService)
        {
            _accountService = accountService;
            _councilService = councilService;
        }
        [HttpPost("export-excel-list-student")]
        public IActionResult ExportExcelListStudent([FromBody] StudentListModel req)
        {
            try
            {
                // Validate 
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.AddWorksheet("DanhSachSinhVien");
                    ws.Cell("A1").Value = "DANH SÁCH SINH VIÊN";
                    ws.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Range("A1:K1").Row(1).Merge().Style.Font.Bold = true;
                    ws.Cell("A1").Style.Font.FontName = "Times New Roman";
                    ws.Cell("A1").Style.Font.FontSize = 20;

                    ws.Column("A").Width = 10;
                    ws.Column("B").Width = 20;
                    ws.Column("C").Width = 40;
                    ws.Column("D").Width = 40;
                    ws.Column("E").Width = 20;
                    ws.Column("F").Width = 20;
                    ws.Column("G").Width = 20;
                    ws.Column("H").Width = 40;
                    ws.Column("I").Width = 40;
                    ws.Column("J").Width = 40;
                    ws.Column("K").Width = 40;
                    ws.Column("L").Width = 40;

                    int Cell = 1;
                    //Dùng để bo viền
                    int Coll = 1;

                    ws.Range("A1:L1").Row(2).Merge();
                    ws.Cell(2, 1).Value = $"";
                    ws.Row(2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Row(2).Style.Font.FontName = "Times New Roman";
                    ws.Range("A1:L1").Row(2).Merge().Style.Font.Bold = true;
                    ws.Row(2).Style.Font.FontSize = 13;
                    Cell = 3;
                    Coll = 3;

                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    dataTable.Columns.Add("STT", typeof(string));
                    dataTable.Columns.Add("Mã sinh viên", typeof(string));
                    dataTable.Columns.Add("Tên tài khoản", typeof(string));
                    dataTable.Columns.Add("Họ và tên", typeof(string));
                    dataTable.Columns.Add("Giới tính", typeof(string));
                    dataTable.Columns.Add("Khóa", typeof(string));
                    dataTable.Columns.Add("Lớp", typeof(string));
                    dataTable.Columns.Add("Số điện thoại", typeof(string));
                    dataTable.Columns.Add("Email", typeof(string));
                    dataTable.Columns.Add("Học kỳ", typeof(string));
                    dataTable.Columns.Add("Chuyên ngành", typeof(string));
                    dataTable.Columns.Add("Giảng viên hướng dẫn", typeof(string));

                    for (var i = 0; i < dataTable.Columns.Count; i++)
                    {
                        ws.Cell(Cell, i + 1).Value = dataTable.Columns[i].ToString();
                    }

                    ws.Row(3).Style.Font.Bold = true;
                    ws.Row(3).Style.Alignment.WrapText = true;
                    ws.Row(3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    ws.Row(3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Row(3).Style.Font.FontSize = 12;
                    ws.Row(3).Style.Font.FontName = "Times New Roman";
                    Cell++;

                    PaginatedResultBase<StudentDTO> data = _accountService.GetListStudent(req);

                    int index = 1;
                    foreach (StudentDTO student in data.ListResult)
                    {
                        int row = 0;
                        ws.Cell(Cell, row += 1).Value = index++;
                        ws.Cell(Cell, row += 1).Value = student.StudentCode;
                        ws.Cell(Cell, row += 1).Value = student.UserName;
                        ws.Cell(Cell, row += 1).Value = student.FullName;
                        ws.Cell(Cell, row += 1).Value = student.Gender == 1 ? "Nữ" : "Nam";
                        ws.Cell(Cell, row += 1).Value = student.SchoolYearName;
                        ws.Cell(Cell, row += 1).Value = student.ClassName;
                        ws.Cell(Cell, row += 1).Value = "'" + student.Phone;
                        ws.Cell(Cell, row += 1).Value = student.Email;
                        ws.Cell(Cell, row += 1).Value = student.Project?.Semester?.NameSemester;
                        ws.Cell(Cell, row += 1).Value = student.Major?.MajorName;
                        Cell++;
                    }
                    int lastCellRow = Cell - 1;
                    ws.Range(Coll, 1, lastCellRow, dataTable.Columns.Count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    ws.Range(Coll, 1, lastCellRow, dataTable.Columns.Count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    ws.Range(Coll, 1, lastCellRow, dataTable.Columns.Count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    ws.Range(Coll, 1, lastCellRow, dataTable.Columns.Count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Range(Coll, 1, lastCellRow, dataTable.Columns.Count).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Range(Coll, 1, lastCellRow, dataTable.Columns.Count).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Range(Coll, 1, lastCellRow, dataTable.Columns.Count).Style.Font.FontName = "Times New Roman";
                    ws.Range(Coll, 1, lastCellRow, dataTable.Columns.Count).Style.Font.FontSize = 13;
                    ws.Range(Coll, 1, lastCellRow, dataTable.Columns.Count).Style.Alignment.WrapText = true;
                    //ws.Range("D" + Coll + ":C5000").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        wb.SaveAs(ms);
                        return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sample.xlsx");
                    }
                }


            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost("export-excel-list-teacher")]
        public IActionResult ExportExcelListTeacher([FromBody] TeacherListModel req)
        {
            try
            {
                // Validate 
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.AddWorksheet("DanhSachGiangVien");
                    ws.Cell("A1").Value = "DANH SÁCH GIẢNG VIÊN";
                    ws.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Range("A1:K1").Row(1).Merge().Style.Font.Bold = true;
                    ws.Cell("A1").Style.Font.FontName = "Times New Roman";
                    ws.Cell("A1").Style.Font.FontSize = 20;

                    ws.Column("A").Width = 10;
                    ws.Column("B").Width = 40;
                    ws.Column("C").Width = 40;
                    ws.Column("D").Width = 40;
                    ws.Column("E").Width = 40;
                    ws.Column("F").Width = 20;
                    ws.Column("G").Width = 40;
                    ws.Column("H").Width = 40;

                    int Cell = 1;
                    //Dùng để bo viền
                    int Coll = 1;

                    ws.Range("A1:L1").Row(2).Merge();
                    ws.Cell(2, 1).Value = $"";
                    ws.Row(2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Row(2).Style.Font.FontName = "Times New Roman";
                    ws.Range("A1:L1").Row(2).Merge().Style.Font.Bold = true;
                    ws.Row(2).Style.Font.FontSize = 13;
                    Cell = 3;
                    Coll = 3;

                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    dataTable.Columns.Add("STT", typeof(string));
                    dataTable.Columns.Add("Tên tài khoản", typeof(string));
                    dataTable.Columns.Add("Họ và tên", typeof(string));
                    dataTable.Columns.Add("Số điện thoại", typeof(string));
                    dataTable.Columns.Add("Email", typeof(string));
                    dataTable.Columns.Add("Giới tính", typeof(string));
                    dataTable.Columns.Add("Chuyên ngành", typeof(string));
                    dataTable.Columns.Add("Học vị", typeof(string));


                    for (var i = 0; i < dataTable.Columns.Count; i++)
                    {
                        ws.Cell(Cell, i + 1).Value = dataTable.Columns[i].ToString();
                    }

                    ws.Row(3).Style.Font.Bold = true;
                    ws.Row(3).Style.Alignment.WrapText = true;
                    ws.Row(3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    ws.Row(3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Row(3).Style.Font.FontSize = 12;
                    ws.Row(3).Style.Font.FontName = "Times New Roman";
                    Cell++;

                    PaginatedResultBase<TeacherDTO> data = _accountService.GetListTeacher(req);

                    int index = 1;
                    foreach (TeacherDTO teacher in data.ListResult)
                    {
                        int row = 0;
                        ws.Cell(Cell, row += 1).Value = index++;
                        ws.Cell(Cell, row += 1).Value = teacher.UserName;
                        ws.Cell(Cell, row += 1).Value = teacher.FullName;
                        ws.Cell(Cell, row += 1).Value = "'" + teacher.Phone;
                        ws.Cell(Cell, row += 1).Value = teacher.Email;
                        ws.Cell(Cell, row += 1).Value = teacher.Gender == 1 ? "Nữ" : "Nam";
                        ws.Cell(Cell, row += 1).Value = teacher.Major?.MajorName;
                        ws.Cell(Cell, row += 1).Value = teacher.Education?.EducationName;

                        Cell++;
                    }
                    int lastCellRow = Cell - 1;
                    ws.Range(Coll, 1, lastCellRow, dataTable.Columns.Count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    ws.Range(Coll, 1, lastCellRow, dataTable.Columns.Count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    ws.Range(Coll, 1, lastCellRow, dataTable.Columns.Count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    ws.Range(Coll, 1, lastCellRow, dataTable.Columns.Count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Range(Coll, 1, lastCellRow, dataTable.Columns.Count).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Range(Coll, 1, lastCellRow, dataTable.Columns.Count).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Range(Coll, 1, lastCellRow, dataTable.Columns.Count).Style.Font.FontName = "Times New Roman";
                    ws.Range(Coll, 1, lastCellRow, dataTable.Columns.Count).Style.Font.FontSize = 13;
                    ws.Range(Coll, 1, lastCellRow, dataTable.Columns.Count).Style.Alignment.WrapText = true;
                    //ws.Range("D" + Coll + ":C5000").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        wb.SaveAs(ms);
                        return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sample.xlsx");
                    }
                }


            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }


        [HttpPost("export-excel-council-by-id")]
        public IActionResult ExportExcelCouncilById([FromBody] StudentCouncilListModel req)
        {
            try
            {
                // Validate 
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    List<ProjectDTO> data = _councilService.getListProjectInCouncil(req);
                    Council council = _councilService.GetCoucnil(req.CouncilId);

                    var ws = wb.AddWorksheet("DanhSachGiangVien");
                    ws.Cell("A2").Value = "BẢNG ĐIỂM TỔNG HỢP "+ council?.CouncilName;
                    ws.Cell("A2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Range("A2:O2").Row(1).Merge().Style.Font.Bold = true;
                    ws.Cell("A2").Style.Font.FontName = "Times New Roman";
                    ws.Cell("A2").Value = ws.Cell("A2").Value.ToString().ToUpper();

                    ws.Cell("A2").Style.Font.FontSize = 20;

                    ws.Column("A").Width = 10;
                    ws.Column("B").Width = 20;
                    ws.Column("C").Width = 30;
                    ws.Column("D").Width = 30;
                    ws.Column("E").Width = 10;
                    ws.Column("F").Width = 10;
                    ws.Column("O").Width = 10;
                    int Cell = 1;
                    //Dùng để bo viền
                    int Coll = 1;

                    

                    

                    Cell = 4;
                    Coll = 4;

                    


                    System.Data.DataTable titleColumn = new System.Data.DataTable();
                    titleColumn.Columns.Add("STT", typeof(string));
                    titleColumn.Columns.Add("Lớp", typeof(string));
                    titleColumn.Columns.Add("Mã sinh viên", typeof(string));
                    titleColumn.Columns.Add("Họ và tên", typeof(string));


                    titleColumn.Columns.Add("Tên đề tài", typeof(string));
                    titleColumn.Columns.Add("Trạng thái đồ án", typeof(string));
                    titleColumn.Columns.Add("Số điện thoại", typeof(string));
                    titleColumn.Columns.Add("Email", typeof(string));
                    titleColumn.Columns.Add("Học vị", typeof(string));
                    titleColumn.Columns.Add("Chuyên nghành", typeof(string));

                    ws.Range("A4:A5").Merge();
                    ws.Cell(Cell,1).Value = "STT";

                    ws.Range("B4:B5").Merge();
                    ws.Cell(Cell,2).Value = "Lớp";

                    ws.Range("C4:C5").Merge();
                    ws.Cell(Cell,3).Value = "Mã sinh viên";

                    ws.Range("D4:E5").Merge();
                    ws.Range("D4:E5").Value = "Họ và tên";

                    ws.Range("F4:O4").Merge();
                    ws.Range("F4:O4").Value = "Điểm";

                    ws.Cell("F5").Value = "Chủ tịch";
                    ws.Cell("G5").Value = "Thư ký";
                    ws.Cell("H5").Value = "UV1";
                    ws.Cell("I5").Value = "UV2";
                    ws.Cell("J5").Value = "UV3";
                    ws.Cell("K5").Value = "TBHĐ";
                    ws.Cell("L5").Value = "GVHD";
                    ws.Cell("M5").Value = "GVPB";
                    ws.Cell("N5").Value = "QT";
                    ws.Cell("O5").Value = "Tổng kết";


                    ws.Range("A4:O5").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Range("A4:O5").Style.Font.FontName = "Times New Roman";
                    ws.Range("A4:O5").Style.Font.FontSize = 13;
                    ws.Range("A4:O5").Style.Font.Bold = true;
                    ws.Range("A4:O5").Style.Alignment.WrapText = true;
                    ws.Range("A4:O5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    Cell = 6;


                    

                    int index = 1;
                    foreach (ProjectDTO project in data)
                    {
                        string fullName = project.UserNameNavigation.FullName;
                        string firstName = fullName.Substring(0, fullName.LastIndexOf(' '));
                        string lastName = fullName.Substring(fullName.LastIndexOf(' ') + 1);
                        int row = 0;
                        ws.Cell(Cell, row += 1).Value = index++;
                        ws.Cell(Cell, row += 1).Value = project.UserNameNavigation != null ? project.UserNameNavigation.ClassName : "";
                        ws.Cell(Cell, row += 1).Value = project.UserNameNavigation != null ? project.UserNameNavigation.StudentCode : "";
                        ws.Cell(Cell, row += 1).Value = firstName;
                        ws.Cell(Cell, row += 1).Value = lastName;
                        ws.Cell(Cell, row += 1).Value = project.ScoreCt; // điểm chủ tịch
                        ws.Cell(Cell, row += 1).Value = project.ScoreTk; // thư ký
                        ws.Cell(Cell, row += 1).Value = project.ScoreUv1; // UV1
                        ws.Cell(Cell, row += 1).Value = project.ScoreUv2; // UV2
                        ws.Cell(Cell, row += 1).Value = project.ScoreUv3; // UV3
                        ws.Cell(Cell, row += 1).Value = "0"; // TBHD
                        ws.Cell(Cell, row += 1).Value = project.ScoreMentor; // GVHD
                        ws.Cell(Cell, row += 1).Value = project.ScoreCommentator; // GVPB
                        ws.Cell(Cell, row += 1).Value = "0"; // QT
                        ws.Cell(Cell, row += 1).Value = "0"; // Tổng kết
                        Cell++;
                    }
                    int lastCellRow = Cell - 1;
                    ws.Range(Coll, 1, lastCellRow, 15).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    ws.Range(Coll, 1, lastCellRow, 15).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    ws.Range(Coll, 1, lastCellRow, 15).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    ws.Range(Coll, 1, lastCellRow, 15).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Range(Coll, 1, lastCellRow, 15).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Range(Coll, 1, lastCellRow, 15).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Range(Coll, 1, lastCellRow, 15).Style.Font.FontName = "Times New Roman";
                    ws.Range(Coll, 1, lastCellRow, 15).Style.Font.FontSize = 13;
                    ws.Range(Coll, 1, lastCellRow, 15).Style.Alignment.WrapText = true;
                    //ws.Range("D" + Coll + ":C5000").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        wb.SaveAs(ms);
                        return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sample.xlsx");
                    }
                }


            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }


    }
}
