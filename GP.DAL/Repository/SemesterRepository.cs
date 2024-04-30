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

            var query = _dbContext.Semesters
                    .Where(x => ((req.FromDate.HasValue && x.FromDate >= req.FromDate.Value) || !req.FromDate.HasValue) &&
                                ((req.ToDate.HasValue && x.ToDate <= req.ToDate.Value) || !req.ToDate.HasValue) &&
                                (string.IsNullOrEmpty(req.SemesterId) || x.SemesterId.Contains(req.SemesterId)) &&
                                (string.IsNullOrEmpty(req.NameSemester) || x.NameSemester.Contains(req.NameSemester))).ToList().Select(semester =>
                                {
                                    var lstProject = _dbContext.Projects.Where(p => p.SemesterId == semester.SemesterId).ToList();
                                    double avgScoreProject = lstProject.Any() ? Math.Round(lstProject.Average(p => p.ScoreFinal ?? 0),3) : 0;
                                    return new SemesterDTO
                                    {
                                        SemesterId = semester.SemesterId,
                                        NameSemester = semester.NameSemester,
                                        FromDate = semester.FromDate,
                                        ToDate = semester.ToDate,
                                        CreatedAt = semester.CreatedAt,
                                        CreatedBy = semester.CreatedBy,
                                        TotalProjectAmount = lstProject.Count,
                                        RejectProjectAmount = lstProject.Count(p => p.StatusProject == "REJECT"),
                                        DoingProjectAmount = lstProject.Count(p => p.StatusProject == "DOING"),
                                        AcceptProjectAmount = lstProject.Count(p => p.StatusProject == "ACCEPT"),
                                        PauseProjectAmount = lstProject.Count(p => p.StatusProject == "PAUSE"),
                                        AvgScoreProject = avgScoreProject,
                                        IsDelete = semester.IsDelete
                                    };
                                });

            int totalItem = query.Count();

            List<SemesterDTO> lstSemester = query
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .ToList();

            result.ListResult = lstSemester;
            result.TotalItem = totalItem;
            result.PageIndex = req.PageIndex;

            return result;
        }
    }
}
