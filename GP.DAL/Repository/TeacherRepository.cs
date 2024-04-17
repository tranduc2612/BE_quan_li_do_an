using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ManagementGraduationProjectContext _dbContext;
        private readonly MappingProfile _mapper;

        public TeacherRepository(ManagementGraduationProjectContext dbContext, MappingProfile mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Teacher Create(Teacher teacher)
        {
            _dbContext.Teachers.Add(teacher);
            _dbContext.SaveChanges();
            return teacher;
        }

        public bool Delete(string username)
        {
            Teacher teacher = _dbContext.Teachers.FirstOrDefault(x => x.UserName == username);
            if (teacher != null)
            {
                teacher.IsDelete = 1;
                _dbContext.Teachers.Update(teacher);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Teacher Get(string username)
        {
            return _dbContext.Teachers.Include(x=>x.Major).FirstOrDefault(x => x.UserName == username);
        }

        public PaginatedResultBase<TeacherDTO> GetList(TeacherListModel data)
        {
            PaginatedResultBase<TeacherDTO> result = new PaginatedResultBase<TeacherDTO>();
            int totalItem = _dbContext.Teachers.Where(x =>
                   x.IsDelete == 0 &&
                   (String.IsNullOrEmpty(data.FullName) || x.FullName.Contains(data.FullName))
                   &&
                   (String.IsNullOrEmpty(data.MajorId) || x.MajorId.Contains(data.MajorId))
                   &&
                   (String.IsNullOrEmpty(data.UserName) || x.UserName.Contains(data.UserName))
                   &&
                   (String.IsNullOrEmpty(data.Status) || x.Status.Contains(data.Status))
                   )
                    .Count();

            List<Teacher> lstTeacher = _dbContext.Teachers.Where(x =>
                   x.IsDelete == 0 &&
                   (String.IsNullOrEmpty(data.FullName) || x.FullName.Contains(data.FullName))
                   &&
                   (String.IsNullOrEmpty(data.MajorId) || x.MajorId.Contains(data.MajorId))
                   &&
                   (String.IsNullOrEmpty(data.UserName) || x.UserName.Contains(data.UserName))
                   &&
                   (String.IsNullOrEmpty(data.Status) || x.Status.Contains(data.Status))
                   )
                   .Include(x => x.Major).ToList();

            result.ListResult = _mapper.MapTeachersToTeacherDTOs(lstTeacher);
            result.TotalItem = totalItem;
            result.PageIndex = data.PageIndex;

            return result;
        }

        public Teacher Update(Teacher student)
        {
            _dbContext.Teachers.Update(student);
            _dbContext.SaveChanges();
            return student;
        }
    }
}
