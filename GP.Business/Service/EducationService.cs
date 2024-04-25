using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.DAL.IRepository;
using GP.DAL.Repository;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.Service
{
    public class EducationService : IEducationService
    {
        IEducationRepository _educationRepository;
        private readonly MappingProfile _mapper;
        public EducationService(IEducationRepository educationRepository, MappingProfile mapper)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
        }
        public bool AddEducation(EducationDTO req, out string message)
        {
            Education educationFind = _educationRepository.GetById(req.EducationId);
            if (educationFind == null)
            {
                _educationRepository.Add(_mapper.MapEducationDTOToEducation(req));
                message = "Thêm thành công !";
                return true;
            }
            else
            {
                message = "Học vấn này đã tồn tại trong hệ thống !";
                return false;
            }
        }

        public List<Education> GetList(EducationDTO req)
        {
            return _educationRepository.GetList(req);
        }

        public bool UpdateEducation(EducationDTO req, out string message)
        {
            Education educationFind = _educationRepository.GetById(req.EducationId);
            if (educationFind == null)
            {
                message = "Học vấn này không tồn tại trong hệ thống !";
                return false;
            }
            else
            {
                educationFind.EducationName = req.EducationName == null ? educationFind.EducationName : req.EducationName;
                educationFind.MaxStudentMentor = req.MaxStudentMentor == null ? educationFind.MaxStudentMentor : req.MaxStudentMentor;
                _educationRepository.Update(educationFind);
                message = "Sửa thành công !";
                return true;
                
            }
        }
    }
}
