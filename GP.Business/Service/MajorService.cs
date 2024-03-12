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
    public class MajorService :IMajorService
    {
        IMajorRepository _majorRepository;
        private readonly MappingProfile _mapper;
        public MajorService(IMajorRepository majorRepository, MappingProfile mapper)
        {
            _majorRepository = majorRepository;
            _mapper = mapper;
        }

        public List<Major> GetList(MajorDTO req)
        {
            return _majorRepository.GetList(req);
        }

        public bool AddMajor(MajorDTO req, out string message)
        {
            Major majorFind = _majorRepository.Get(req);
            if (majorFind == null)
            {
                _majorRepository.Add(_mapper.MapMajorDTOToMajor(req));
                message = "Thêm thành công !";
                return true;
            }
            else
            {
                message = "Chuyên nghành này đã tồn tại trong hệ thống !";
                return false;

            }
        }
    }
}
