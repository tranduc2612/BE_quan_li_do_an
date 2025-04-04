﻿using GP.DAL.IRepository;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.Repository
{
    public class ClassificationRepository : IClassificationRepository
    {
        private readonly ManagementGraduationProjectContext _dbContext;
        public ClassificationRepository(ManagementGraduationProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Classification GetByCode(string code)
        {
            return _dbContext.Classifications.FirstOrDefault(x => x.Code == code);
        }

        public Classification GetByTypeCodeAndCode(string type_code, string code)
        {
            return _dbContext.Classifications.FirstOrDefault(x => x.Code == code && x.TypeCode == type_code);
        }

        public List<Classification> GetListByTypeCode(string code)
        {
            return _dbContext.Classifications.Where(x => x.TypeCode == code).ToList();
        }

        public Classification Update(Classification model)
        {
            _dbContext.Update(model);
            _dbContext.SaveChanges();
            return model;
        }
    }
}
