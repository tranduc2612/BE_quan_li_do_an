using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.Service
{
    public class ScheduleSemesterService : IScheduleSemesterService
    {
        IScheduleSemesterRepository _scheduleSemesterRepository;
        ISemesterRepository _semesterRepository;
        ITeacherRepository _teacherRepository;
        private readonly MappingProfile _mapper;
        public ScheduleSemesterService(IScheduleSemesterRepository scheduleSemesterRepository, ISemesterRepository semesterRepository,ITeacherRepository teacherRepository, MappingProfile mapper)
        {
            _scheduleSemesterRepository = scheduleSemesterRepository;
            _mapper = mapper;
            _semesterRepository = semesterRepository;
            _teacherRepository = teacherRepository;
        }

        public bool AddScheduleSemester(ScheduleSemesterModel data, out string message)
        {
            Semester semester = _semesterRepository.GetById(data.SemesterId);
            if(semester == null)
            {
                message = "Học kỳ không tồn tại !";
                return false;
            }
            Teacher teacher = _teacherRepository.Get(data.CreatedBy);
            if (teacher == null)
            {
                message = "Quản trị viên không tồn tại !";
                return false;
            }
            ScheduleSemester dataAdd = _mapper.MapScheduleSemesterModelToScheduleSemester(data);
            message = _scheduleSemesterRepository.Add(dataAdd);
            return true;
        }

        public bool UpdateScheduleSemester(ScheduleSemesterModel data, out string message)
        {
            ScheduleSemester find = _scheduleSemesterRepository.GetById(data.ScheduleSemesterId);
            if(find == null)
            {
                message = "Lịch không tồn tại !";
                return false;
            }

            find.FromDate = data.FromDate;
            find.ToDate = data.ToDate;
            find.TypeSchedule = data.TypeSchedule;
            find.Implementer = data.Implementer;
            find.Content = data.Content;
            find.Note = data.Note;
            //if(files != null)
            //{
            //    foreach (var file in files)
            //    {
            //        if (file.Length > 0)
            //        {
            //            var fileInformation = new ScheduleSemesterFile();
            //            fileInformation.FileName = file.Name;
            //            fileInformation.FileSize = file.Length.ToString();
            //            fileInformation.FileType = file.ContentType;
            //            fileInformation.ScheduleSemesterId = find.ScheduleSemesterId;

            //            // Lưu thông tin vào cơ sở dữ liệu ở đây

            //            // Lưu tệp tin vào máy chủ
            //            _scheduleSemesterRepository.UpFile(fileInformation);
            //            var filePath = Path.Combine("file/file_semester", file.FileName+ fileInformation.FileId);
            //            using (var stream = new FileStream(filePath, FileMode.Create))
            //            {
            //                file.CopyToAsync(stream);
            //            }

            //        }
            //    }
            //}
            _scheduleSemesterRepository.Update(find);
            message = "Cập nhật thành công !";
            return true;

        }

        public List<ScheduleSemesterDTO> GetListScheduleSemester(string semesterId)
        {
            List<ScheduleSemester> lst = _scheduleSemesterRepository.GetListScheduleSemester(semesterId);

            return _mapper.MapScheduleSemestersToScheduleSemesterDTOs(lst);
        }

        public bool DeleteScheduleSemester(string id, out string message)
        {
            ScheduleSemester scheduleSemester = _scheduleSemesterRepository.GetById(id);
            if (scheduleSemester == null)
            {
                message = "Lịch học kỳ không hợp lệ !";
                return false;
            }
            _scheduleSemesterRepository.Delete(scheduleSemester);
            message = "Xóa thành công !"; 
            return true;
        }

        public ScheduleSemesterDTO GetById(string id)
        {
            ScheduleSemester find = _scheduleSemesterRepository.GetById(id);

            return _mapper.MapScheduleSemesterToScheduleSemesterDTO(find);
        }
    }
}
