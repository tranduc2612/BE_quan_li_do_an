using DocumentFormat.OpenXml.InkML;
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
    public class ScheduleWeekService : IScheduleWeekService
    {
        IScheduleWeekRepository _scheduleWeekRepository;
        ISemesterRepository _semesterRepository;
        private readonly MappingProfile _mapper;
        public ScheduleWeekService(IScheduleWeekRepository scheduleWeekRepository, ISemesterRepository semesterRepository, MappingProfile mapper)
        {
            _scheduleWeekRepository = scheduleWeekRepository;
            _semesterRepository = semesterRepository;
            _mapper = mapper;
        }
        public string AddScheduleWeek(ScheduleWeekModel data, out string message, out bool check)
        {
            Semester semester = _semesterRepository.GetById(data.SemesterId);
            if(semester == null)
            {
                check = false;
                message = "Học kỳ không tồn tại";
                return null;
            }
            ScheduleWeek add = _mapper.MapScheduleWeekModelToScheduleWeek(data);
            _scheduleWeekRepository.Add(add);
            check = true;
            message = "Thêm thành công !";
            return add.ScheduleWeekId;
        }

        public bool DeleteScheduleWeek(string id, out string message)
        {
            ScheduleWeek find = _scheduleWeekRepository.GetById(id);
            if(find == null) {
                message = "Không tồn tại lịch này !";
                return false;
            }
            _scheduleWeekRepository.Delete(find);
            message = "Xóa thành công";
            return true;
        }

        public ScheduleWeekDTO GetById(string id)
        {
            return _mapper.MapScheduleWeekToScheduleWeekDTO(_scheduleWeekRepository.GetById(id));
        }

        public bool UpdateScheduleWeek(ScheduleWeekModel data, out string message)
        {
            ScheduleWeek find = _scheduleWeekRepository.GetById(data?.ScheduleWeekId);
            if(find == null)
            {
                message = "Không tồn tại mã lịch này !";
                return false;
            }
            find.Title = data.Title;
            find.Content = data.Content;
            find.FromDate = data.FromDate;
            find.ToDate = data.ToDate;
            _scheduleWeekRepository.Update(find);
            message = "Cập nhật thành công !";
            return true;
        }

        public List<ScheduleWeekDTO> GetListScheduleWeek(string semesterId,string usernameCreated)
        {
            return _mapper.MapScheduleWeeksToScheduleWeekDTOs(_scheduleWeekRepository.GetListScheduleWeek(semesterId, usernameCreated));
        }

        public DetailScheduleWeek HandleScheduleWeekDetail(ScheduleWeekDetailModel data, out string message, out bool check)
        {
            DetailScheduleWeek find = _scheduleWeekRepository.GetDetailScheduleWeek(data.UserNameProject, data.ScheduleWeekId);

            if (find != null && data.Function == "D")
            {
                _scheduleWeekRepository.DeleteDetailScheduleWeek(find);
                if (File.Exists(Path.Combine("file/file_week", find.NameFile)))
                {
                    File.Delete(Path.Combine("file/file_week", find.NameFile));
                }
                check = true;
                message = "Xóa thành công";
                return null;
            }
            if (data.file == null || data.file.Length == 0)
            {
                check = false;
                message = "file không hợp lệ !";
                return null;
            }
            var randomName = $"{Path.GetFileNameWithoutExtension(data.file.FileName)}_{DateTime.Now.ToString("yyyyMMddHHmmss")}_{new Random().Next(1000, 9999)}_{Path.GetExtension(data.file.FileName)}";
            var filePath = Path.Combine("file/file_week", randomName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                data.SizeFile = data.file.Length.ToString();
                data.NameFile = randomName;
                DetailScheduleWeek mapper = _mapper.MapScheduleWeekDetailModelToDetailScheduleWeekDTO(data);
                if (find != null && data.Function == "U")
                {
                    if(File.Exists(Path.Combine("file/file_week", find.NameFile)))
                    {
                        File.Delete(Path.Combine("file/file_week", find.NameFile));
                    }
                    find.NameFile = data.NameFile;
                    find.SizeFile = data.SizeFile;
                    check = true;
                    message = "Sửa thành công";
                    DetailScheduleWeek new_data = _scheduleWeekRepository.UpdateDetailScheduleWeek(find);
                    data.file.CopyTo(stream);
                    return new_data;
                }
                if (find == null && data.Function == "C")
                {
                    DetailScheduleWeek new_data = _scheduleWeekRepository.AddDetailScheduleWeek(mapper);
                    check = true;
                    message = "Thêm thành công";
                    data.file.CopyTo(stream);
                    return new_data;
                }
                check = false;
                message = "Chức năng không hợp lệ !";
                return null;
            }

        }


        public DetailScheduleWeek GetScheduleWeekDetail(string userName, string idScheduleWeek)
        {
            return _scheduleWeekRepository.GetDetailScheduleWeek(userName, idScheduleWeek);
        }

        public DetailScheduleWeek UpdateComment(ScheduleWeekDetailModel data, out string message)
        {
            DetailScheduleWeek find = _scheduleWeekRepository.GetDetailScheduleWeek(data.UserNameProject, data.ScheduleWeekId);
            if (find == null)
            {
                message = "Sinh viên chưa nộp file báo cáo tuần !";
                return null;
            }
            find.Comment = data.Comment;
            DetailScheduleWeek new_data = _scheduleWeekRepository.UpdateDetailScheduleWeek(find);
            message = "Cập nhật thành công !";
            return new_data;
        }


    }
}
