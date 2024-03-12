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
            if (_semesterRepository.Add(_mapper.MapSemesterDTOToSemester(req)))
            {
                message= "Thêm thành thành công !";
                return true;
            }
            else
            {
                message = "học kỳ đã tồn tại !";
                return false;
            }

        }

        public List<Semester> GetListSemester(SemesterDTO req)
        {
            return _semesterRepository.GetList(req);
        }

    }
}
