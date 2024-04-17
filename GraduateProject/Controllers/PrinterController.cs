using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using GP.Business.IService;
using GP.Business.Service;
using GP.Common.DTO;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrinterController : ControllerBase
    {
        private readonly IProjectOutlineRepository _outlineService;
        private readonly IProjectService _projectService;


        public PrinterController(IProjectOutlineRepository outlineService, IProjectService projectService)
        {
            _outlineService = outlineService;
            _projectService = projectService;
        }



        private static DocumentFormat.OpenXml.Wordprocessing.Paragraph GetText(string cellText)
        {
            var run = new DocumentFormat.OpenXml.Wordprocessing.Run(new Text(cellText));
            FontSize fontSize1 = new FontSize() { Val = "26" };

            // Thiết lập font chữ
            run.RunProperties = new DocumentFormat.OpenXml.Wordprocessing.RunProperties();
            run.RunProperties.Append(new RunFonts() { Ascii = "Times New Roman" }); // Font chữ Arial
            run.RunProperties.Append(fontSize1);

            return new DocumentFormat.OpenXml.Wordprocessing.Paragraph(run);
        }
        
        private static DocumentFormat.OpenXml.Wordprocessing.Paragraph GetTextTitel(string cellText)
        {
            var run = new DocumentFormat.OpenXml.Wordprocessing.Run(new Text(cellText));
            FontSize fontSize1 = new FontSize() { Val = "26" };

            // Thiết lập font chữ
            run.RunProperties = new DocumentFormat.OpenXml.Wordprocessing.RunProperties();
            run.RunProperties.Append(new RunFonts() { Ascii = "Times New Roman" }); // Font chữ Arial
            run.RunProperties.Append(fontSize1);
            run.RunProperties.Append(new Bold());
            run.RunProperties.Append(new DocumentFormat.OpenXml.Wordprocessing.Justification() { Val = DocumentFormat.OpenXml.Wordprocessing.JustificationValues.Center });


            var paragraphProperties = new DocumentFormat.OpenXml.Wordprocessing.ParagraphProperties();
            paragraphProperties.Append(new DocumentFormat.OpenXml.Wordprocessing.Justification() { Val = DocumentFormat.OpenXml.Wordprocessing.JustificationValues.Center });
            //paragraphProperties.Append(run);

            var newParagraph = new DocumentFormat.OpenXml.Wordprocessing.Paragraph(run);
            newParagraph.Append(paragraphProperties);
            return newParagraph;
        }

        [HttpGet("print-word-outline")]
        public async Task<IActionResult> DowloadWordOutline(string id)
        {
            ProjectDTO project = _projectService.GetProjectByUserName(id);
            if(project == null)
            {
                return BadRequest();
            }
            

            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "file", "template", "outline");

            var templateFile = System.IO.Path.Combine(path, "template.docx");
            var copiedFilePath = System.IO.Path.Combine(path, project.UserName);

            if (!Directory.Exists(copiedFilePath))
            {
                Directory.CreateDirectory(copiedFilePath);
            }

            copiedFilePath = System.IO.Path.Combine(copiedFilePath, "outline_"+ project.UserName + ".docx");

            System.IO.File.Copy(templateFile, copiedFilePath, true);

            string name_file = "DeCuong_";

            using (var word = WordprocessingDocument.Open(copiedFilePath, true))
            {
                var body = word.MainDocumentPart.Document.Body;
                DateTime currentDate = DateTime.Now;
                int day = currentDate.Day;       // Lấy ngày
                int month = currentDate.Month;   // Lấy tháng
                int year = currentDate.Year;     // Lấy năm

                // thây thế ngày tháng năm hiện tại
                foreach (var text in body.Descendants<Text>()) // <<< Here
                {
                    if (text.Text.Contains("CURDATE"))
                    {
                        text.Text = text.Text.Replace("CURDATE", day.ToString());
                    }
                    if (text.Text.Contains("CURMONTH"))
                    {
                        text.Text = text.Text.Replace("CURMONTH", month.ToString());
                    }
                    if (text.Text.Contains("CURYEAR"))
                    {
                        text.Text = text.Text.Replace("CURYEAR", year.ToString());
                    }
                }

                // thêm thông tin dựa theo bookmark
                IDictionary<String, BookmarkStart> bookMarkMap = new Dictionary<String, BookmarkStart>();
                foreach (BookmarkStart bookMarkStart in word.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                {
                    bookMarkMap[bookMarkStart.Name] = bookMarkStart;
                }

                foreach (BookmarkStart bookMarkStart in bookMarkMap.Values)
                {
                    var parent = bookMarkStart.Parent;
                    if (bookMarkStart.Name == "FULLNAMESTUDENT")
                    {
                        parent.InsertBeforeSelf(GetText(project?.UserNameNavigation?.FullName));
                        //create paragraph to insert
                        parent.Remove();
                    }
                    if (bookMarkStart.Name == "STUDENTCODE")
                    {
                        parent.InsertBeforeSelf(GetText(project?.UserNameNavigation?.StudentCode));
                        //create paragraph to insert
                        parent.Remove();
                    }
                    if (bookMarkStart.Name == "CLASSNAME")
                    {
                        parent.InsertBeforeSelf(GetText(project?.UserNameNavigation?.ClassName));
                        //create paragraph to insert
                        parent.Remove();
                    }
                    if (bookMarkStart.Name == "PHONESTUDENT")
                    {
                        parent.InsertBeforeSelf(GetText(project?.UserNameNavigation?.Phone));
                        //create paragraph to insert
                        parent.Remove();
                    }
                    if (bookMarkStart.Name == "EMAILSTUDENT")
                    {
                        parent.InsertBeforeSelf(GetText(project?.UserNameNavigation?.Email));
                        //create paragraph to insert
                        parent.Remove();
                    }
                    if (bookMarkStart.Name == "SCHOOLYEAR")
                    {
                        parent.InsertBeforeSelf(GetText(project?.UserNameNavigation?.SchoolYearName));
                        //create paragraph to insert
                        parent.Remove();
                    }

                    /// Teachhher
                    if (bookMarkStart.Name == "FULLNAMETEACHER")
                    {
                        parent.InsertBeforeSelf(GetText(project?.UserNameMentorNavigation?.FullName));
                        //create paragraph to insert
                        parent.Remove();
                    }
                    if (bookMarkStart.Name == "MAJORTEACHER")
                    {
                        parent.InsertBeforeSelf(GetText(project?.UserNameMentorNavigation?.Major?.MajorName));
                        //create paragraph to insert
                        parent.Remove();
                    }
                    if (bookMarkStart.Name == "PHONETEACHER")
                    {
                        parent.InsertBeforeSelf(GetText(project?.UserNameMentorNavigation?.Phone));
                        //create paragraph to insert
                        parent.Remove();
                    }
                    if (bookMarkStart.Name == "EMAILTEACHER")
                    {
                        parent.InsertBeforeSelf(GetText(project?.UserNameMentorNavigation?.Email));
                        //create paragraph to insert
                        parent.Remove();
                    }

                    // Content
                    if (bookMarkStart.Name == "NAMEPROJECT")
                    {
                        parent.InsertBeforeSelf(GetText(project?.ProjectOutline?.NameProject));
                        //create paragraph to insert
                        parent.Remove();
                    }

                    if (bookMarkStart.Name == "CONTENT")
                    {
                        //do insert here  
                        if(project?.ProjectOutline?.ContentProject != null)
                        {
                            string[] contents = JsonConvert.DeserializeObject(project?.ProjectOutline?.ContentProject).ToString().Split("\n");
                            foreach (string text in contents)
                            {
                                parent.InsertBeforeSelf(GetText(text));
                            }
                            //create paragraph to insert
                            parent.Remove();
                        }
                    }

                    if (bookMarkStart.Name == "TECHUSE")
                    {
                        //do insert here  
                        if(project?.ProjectOutline?.TechProject != null)
                        {
                            string[] techUse = JsonConvert.DeserializeObject(project?.ProjectOutline?.TechProject).ToString().Split("\n");
                            foreach (string textTech in techUse)
                            {
                                parent.InsertBeforeSelf(GetText(textTech));
                            }
                            //create paragraph to insert
                            parent.Remove();
                        }
                    }

                    if (bookMarkStart.Name == "RESULTEXPECT")
                    {
                        //do insert here  
                        if(project?.ProjectOutline?.ExpectResult != null)
                        {
                            string[] expect = JsonConvert.DeserializeObject(project?.ProjectOutline?.ExpectResult).ToString().Split("\n");
                            foreach (string text in expect)
                            {
                                parent.InsertBeforeSelf(GetText(text));
                            }
                            //create paragraph to insert
                            parent.Remove();
                        }
                    }

                    if (bookMarkStart.Name == "PLAN")
                    {
                        DocumentFormat.OpenXml.Wordprocessing.Table tbl = new DocumentFormat.OpenXml.Wordprocessing.Table();

                        // Set the style and width for the table.
                        DocumentFormat.OpenXml.Wordprocessing.TableProperties tableProp = new DocumentFormat.OpenXml.Wordprocessing.TableProperties();
                        DocumentFormat.OpenXml.Wordprocessing.TableStyle tableStyle = new DocumentFormat.OpenXml.Wordprocessing.TableStyle() { Val = "TableGrid" };

                        // Make the table width 100% of the page width.
                        TableWidth tableWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct };

                        // Apply
                        tableProp.Append(tableStyle, tableWidth);
                        tbl.AppendChild(tableProp);

                        // Add 3 columns to the table.
                        DocumentFormat.OpenXml.Wordprocessing.TableGrid tg = new DocumentFormat.OpenXml.Wordprocessing.TableGrid(new DocumentFormat.OpenXml.Wordprocessing.GridColumn(), new DocumentFormat.OpenXml.Wordprocessing.GridColumn(), new DocumentFormat.OpenXml.Wordprocessing.GridColumn(), new DocumentFormat.OpenXml.Wordprocessing.GridColumn());
                        tbl.AppendChild(tg);

                        // Create 1 row to the table.
                        DocumentFormat.OpenXml.Wordprocessing.TableRow tr1 = new DocumentFormat.OpenXml.Wordprocessing.TableRow(new TableRowProperties(new TableRowHeight() { Val = Convert.ToUInt32("450") }));
                        // Add a cell to each column in the row.
                        DocumentFormat.OpenXml.Wordprocessing.TableCell tc1 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(GetTextTitel("STT"));
                        DocumentFormat.OpenXml.Wordprocessing.TableCell tc2 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(GetTextTitel("Nội dung công việc"));
                        DocumentFormat.OpenXml.Wordprocessing.TableCell tc3 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(GetTextTitel("Thời gian dự kiến"));
                        DocumentFormat.OpenXml.Wordprocessing.TableCell tc4 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(GetTextTitel("Ghi chú"));

                        tr1.Append(tc1, tc2, tc3, tc4);

                        // Add row to the table.
                        tbl.AppendChild(tr1);

                        if(project?.ProjectOutline?.PlantOutline != null)
                        {
                            List<PlantOutline> platOutline = JsonConvert.DeserializeObject<List<PlantOutline>>(project?.ProjectOutline?.PlantOutline);
                            for (int i = 0; i < platOutline.Count; i++)
                            {
                                
                            // Create 1 row to the table.
                            DocumentFormat.OpenXml.Wordprocessing.TableRow tr = new DocumentFormat.OpenXml.Wordprocessing.TableRow(new TableRowProperties(new TableRowHeight() { Val = Convert.ToUInt32("450") }));
                            // Add a cell to each column in the row.
                            DocumentFormat.OpenXml.Wordprocessing.TableCell value1 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(GetText(platOutline[i].stt));
                            DocumentFormat.OpenXml.Wordprocessing.TableCell value2 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(GetText(platOutline[i].content));
                                string convertDateTime = "";
                                if(platOutline[i].fromDate != null && platOutline[i].toDate != null && platOutline[i].fromDate is DateTime && platOutline[i].toDate is DateTime)
                                {
                                    DateTime fromDate = (DateTime)platOutline[i].fromDate;
                                    DateTime toDate = (DateTime)platOutline[i].toDate;
                                    convertDateTime = fromDate.ToString("dd/MM/yyyy") + "-" + toDate.ToString("dd/MM/yyyy");
                                }
                            DocumentFormat.OpenXml.Wordprocessing.TableCell value3 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(GetText(convertDateTime));
                            DocumentFormat.OpenXml.Wordprocessing.TableCell value4 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(GetText(platOutline[i].note));

                            tr.Append(value1, value2, value3, value4);

                            // Add row to the table.
                            tbl.AppendChild(tr);
                        }
                        }


                        parent.InsertBeforeSelf(tbl);
                    }
                }
            }

            // Đọc nội dung đã được lưu vào MemoryStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (FileStream fileStream = new FileStream(copiedFilePath, FileMode.Open, FileAccess.Read))
                {
                    await fileStream.CopyToAsync(memoryStream);
                }

                byte[] fileBytes = memoryStream.ToArray();

                // Trả về FileContentResult để tải xuống
                return new FileContentResult(fileBytes, "application/msword")
                {
                    FileDownloadName = name_file + $"{project.UserName}.docx"
                };
            }
        }

    }
}
