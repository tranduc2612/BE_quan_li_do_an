using GP.Business.IService;
using GP.Common.Helpers;
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
    public class ClassificationService : IClassificationService
    {
        private readonly IClassificationRepository _classificationRepository;


        private readonly MappingProfile _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClassificationService(IClassificationRepository classificationRepository, IHttpContextAccessor httpContextAccessor, MappingProfile mapper)
        {
            _classificationRepository = classificationRepository;
            _mapper = mapper;
        }

        public Classification GetClassification(string type_code, string code)
        {
            return _classificationRepository.GetByTypeCodeAndCode(type_code, code);
        }

        public List<Classification> GetListClassification(string code)
        {
            return _classificationRepository.GetListByTypeCode(code);
        }

        public Classification Update(Classification model)
        {
            Classification classification = _classificationRepository.GetByTypeCodeAndCode(model.TypeCode, model.Code);
            if(classification == null)
            {
                return null;
            }
            _classificationRepository.Update(model);
            return model;
        }
    }
}
