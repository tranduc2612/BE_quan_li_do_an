using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.Service
{
    public class SemesterService : ISemesterService
    {
        ISemesterRepository _semesterRepository;
        private readonly MappingProfile _mapper;
        public SemesterService(ISemesterRepository semesterRepository, MappingProfile mapper) {
            _semesterRepository = semesterRepository;
            _mapper = mapper;
        }

        public bool Add(SemesterDTO req, out string message)
        {
            Semester semesterFind = _semesterRepository.GetById(req.SemesterId);
            if (semesterFind != null)
            {
                message = "học kỳ đã tồn tại !";
                return false;
            }

            //List<Semester> lstSemesterCheck = _semesterRepository.GetByDate(req.FromDate, req.ToDate);
            //if(lstSemesterCheck.Count() > 0) {
            //    message = "Khoảng thời gian này đã có học kỳ tồn tại !";
            //    return false;
            //}

            message = "Thêm thành công !";

            return _semesterRepository.Add(_mapper.MapSemesterDTOToSemester(req));

        }

        public List<Semester> GetListSemester(SemesterDTO req)
        {
            return _semesterRepository.GetList(req);
        }

        public PaginatedResultBase<SemesterDTO> GetListSemesterPage(SemesterListModel data)
        {
            return _semesterRepository.GetListPage(data);
        }

        public Semester getSemester(string id)
        {
            return _semesterRepository.GetById(id);
        }
    }
}
