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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GP.DAL.Repository
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly ManagementGraduationProjectContext _dbContext;
        private readonly MappingProfile _mapper;

        public SemesterRepository(ManagementGraduationProjectContext dbContext, MappingProfile mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool Add(Semester req)
        {
            _dbContext.Semesters.Add(req);

            var query = from teacher in _dbContext.Teachers
                        where teacher.IsDelete == 0 && teacher.Status == "AUTH"
                        select new Teaching
                        {
                            UserNameTeacher = teacher.UserName,
                            SemesterId = req.SemesterId
                        };

            _dbContext.Teachings.AddRange(query);

            _dbContext.SaveChanges();
            return true;
        }

        public List<Semester> GetByDate(DateTime? fromDate, DateTime? toDate)
        {
            var query = _dbContext.Semesters.AsQueryable();

            if (fromDate.HasValue)
            {
                query = query.Where(x => x.FromDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(x => x.ToDate <= toDate.Value);
            }

            var semesters = query.ToList();

            return semesters;
        }

        public Semester GetById(string id)
        {
            return _dbContext.Semesters.FirstOrDefault(x=>x.SemesterId == id);
        }

        public List<Semester> GetList(SemesterDTO req)
        {

            return _dbContext.Semesters.Where(x =>
                   (String.IsNullOrEmpty(req.NameSemester) || x.NameSemester.Contains(req.NameSemester))).OrderBy(x=>x.CreatedAt).ToList();
        }

        public PaginatedResultBase<SemesterDTO> GetListPage(SemesterListModel req)
        {
            PaginatedResultBase<SemesterDTO> result = new PaginatedResultBase<SemesterDTO>();
            var query = (from semester in _dbContext.Semesters
                         join teacher in _dbContext.Teachers on semester.CreatedBy equals teacher.UserName
                         join project in _dbContext.Projects on semester.SemesterId equals project.SemesterId into projectJoin
                         from project in projectJoin.DefaultIfEmpty()
                         join project_outline in _dbContext.ProjectOutlines on project.UserName equals project_outline.UserName into projectOutlineJoin
                         from project_outline in projectOutlineJoin.DefaultIfEmpty()
                         where
                             ((req.FromDate.HasValue && semester.FromDate >= req.FromDate.Value) || !req.FromDate.HasValue) &&
                            ((req.ToDate.HasValue && semester.ToDate <= req.ToDate.Value) || !req.ToDate.HasValue) &&
                            (string.IsNullOrEmpty(req.SemesterId) || semester.SemesterId.Contains(req.SemesterId)) &&
                            (string.IsNullOrEmpty(req.NameSemester) || semester.NameSemester.Contains(req.NameSemester))
                         select new
                         {
                             Semester = semester,
                             Teacher = teacher,
                             Project = project,
                             ProjectOutline = project_outline
                         });

            var groupedQuery = query.AsEnumerable()
                .GroupBy(x => new
                {
                    SemesterId = x.Semester.SemesterId,
                    NameSemester = x.Semester.NameSemester,
                    FromDate = x.Semester.FromDate,
                    ToDate = x.Semester.ToDate,
                    CreatedAt = x.Semester.CreatedAt,
                    IsDelete = x.Semester.IsDelete,
                });

            int totalItem = groupedQuery.Count();

            List<SemesterDTO> lstSemester = groupedQuery
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .Select(x => new SemesterDTO(
                    x.Key.SemesterId,
                    x.Key.NameSemester,
                    x.Key.FromDate,
                    x.Key.ToDate,
                    x.Key.CreatedAt,
                    x.Count(p => p.ProjectOutline != null),
                    x.Count(p => p.Project != null && p.Project.StatusProject == "REJECT"),
                    x.Count(p => p.Project != null && p.Project.StatusProject == "DOING"),
                    x.Count(p => p.Project != null && p.Project.StatusProject == "ACCEPT"),
                    x.Count(p => p.Project != null && p.Project.StatusProject == "PAUSE"),
                    x.Average(p => p.Project?.ScoreFinal ?? 0),
                    _mapper.MapTeacherToTeacherDTO(x.First().Teacher),
                    x.Key.IsDelete,
                    x.Count(p => p.Project != null)
                ))
                .ToList();

            result.ListResult = lstSemester;
            result.TotalItem = totalItem;
            result.PageIndex = req.PageIndex;

            return result;
        }
    }
}
