﻿using GP.Common.DTO;
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
    public class GroupReviewOutlineRepository : IGroupReviewOutlineRepository
    {
        private readonly ManagementGraduationProjectContext _dbContext;
        private readonly MappingProfile _mapper;

        public GroupReviewOutlineRepository(ManagementGraduationProjectContext dbContext, MappingProfile mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Add(GroupReviewOutline data)
        {
            _dbContext.Add(data);
            _dbContext.SaveChanges();
        }

        public void AssignGroupToOutline(ProjectOutline outline)
        {
            _dbContext.ProjectOutlines.Update(outline);
            _dbContext.SaveChanges();
        }

        public void AssignGroupToTeaching(Teaching teaching)
        {
            _dbContext.Teachings.Update(teaching);
            _dbContext.SaveChanges();
        }

        public void Delete(GroupReviewOutline data)
        {
            List<ProjectOutline> poU = _dbContext.ProjectOutlines.Where(x=>x.GroupReviewOutlineId == data.GroupReviewOutlineId).ToList();
            List<Teaching> tcU = _dbContext.Teachings.Where(x => x.GroupReviewOutlineId == data.GroupReviewOutlineId).ToList();
            foreach (var obj in poU)
            {
                obj.GroupReviewOutlineId = null;
            }
            _dbContext.UpdateRange(poU);

            foreach (var obj in tcU)
            {
                obj.GroupReviewOutlineId = null;
            }
            _dbContext.UpdateRange(tcU);

            data.IsDelete = 1;
            _dbContext.Update(data);
            _dbContext.SaveChanges();
        }

        public GroupReviewOutline GetById(string id)
        {
            return _dbContext.GroupReviewOutlines.Include(x=>x.Teachings).Include(x=>x.ProjectOutlines).FirstOrDefault(x => x.GroupReviewOutlineId == id);
        }

        public List<GroupReviewOutline> GetListGroupBySemesterId(string semesterId)
        {
            return _dbContext.GroupReviewOutlines.Include(x=>x.Teachings).Where(x => x.SemesterId == semesterId && x.IsDelete == 0).ToList();
        }

        public PaginatedResultBase<GroupReviewOutline> GetListPage(GroupReviewOutlineListModel data)
        {
            PaginatedResultBase<GroupReviewOutline> result = new PaginatedResultBase<GroupReviewOutline>();
            var query = (from GroupReviewOutline in _dbContext.GroupReviewOutlines
                         where GroupReviewOutline.IsDelete == 0 &&
                               (string.IsNullOrEmpty(data.GroupReviewOutlineId) || GroupReviewOutline.GroupReviewOutlineId.Contains(data.GroupReviewOutlineId))
                               &&
                               (string.IsNullOrEmpty(data.NameGroupReviewOutline) || GroupReviewOutline.NameGroupReviewOutline.Contains(data.NameGroupReviewOutline))
                         select new
                         {
                             GroupReviewOutline = GroupReviewOutline,
                         });
            int totalItem = query.Count();

            List<GroupReviewOutline> lstStudent = query.Skip((data.PageIndex - 1) * data.PageSize)
                 .Take(data.PageSize).Select(x=>x.GroupReviewOutline)
                 .ToList();

            result.ListResult = lstStudent;
            result.TotalItem = totalItem;
            result.PageIndex = data.PageIndex;
            return result;
        }

        public List<GroupReviewOutlineDTO> GetListPageSemester(GroupReviewOutlineListSemesterModel data)
        {
            //var query = from groupReviewOutline in _dbContext.GroupReviewOutlines
            //            join teaching in _dbContext.Teachings on groupReviewOutline.GroupReviewOutlineId equals teaching.GroupReviewOutlineId into teachingGroup
            //            from teaching in teachingGroup.DefaultIfEmpty()
            //join t1 in (
            //                from gro in _dbContext.GroupReviewOutlines
            //                join projectOutline in _dbContext.ProjectOutlines on gro.GroupReviewOutlineId equals projectOutline.GroupReviewOutlineId into projectOutlineGroup
            //                from projectOutline in projectOutlineGroup.DefaultIfEmpty()
            //                join project in _dbContext.Projects on projectOutline.UserName equals project.UserName into projectGroup
            //                from project in projectGroup.DefaultIfEmpty()
            //                where gro.IsDelete == 0 && (project.SemesterId == "2_2024_2025" || project.SemesterId == null)
            //                group new { gro, project } by gro.GroupReviewOutlineId into g
            //                select new
            //                {
            //                    GroupReviewOutlineId = g.Key,
            //                    SLSV = g.Count(x => x.project != null)
            //                }
            //            ) on groupReviewOutline.GroupReviewOutlineId equals t1.GroupReviewOutlineId
            //            where groupReviewOutline.IsDelete == 0 && (teaching.SemesterId == "2_2024_2025" || teaching.SemesterId == null)
            //            group new { groupReviewOutline, teaching, t1 } by new { groupReviewOutline.GroupReviewOutlineId, groupReviewOutline.NameGroupReviewOutline, t1.SLSV } into g
            //            select new GroupReviewOutlineDTO
            //            {
            //                GroupReviewOutlineId = g.Key.GroupReviewOutlineId,
            //                NameGroupReviewOutline = g.Key.NameGroupReviewOutline,
            //                SLGV = g.Count(x => x.teaching != null),
            //                SLSV = g.Key.SLSV
            //            };

            //int totalItem = query.Count();
            List<GroupReviewOutlineDTO> lstGroupReview = _dbContext.GroupReviewOutlines
                .Where(x=>x.SemesterId == data.SemesterId && x.IsDelete == 0
                &&
                    (string.IsNullOrEmpty(data.GroupReviewOutlineId) || x.GroupReviewOutlineId.Contains(data.GroupReviewOutlineId))
                &&
                    (string.IsNullOrEmpty(data.NameGroupReviewOutline) || x.NameGroupReviewOutline.Contains(data.NameGroupReviewOutline))
                )
                //.Include(x=>x.ProjectOutlines)
                //.Include(x=>x.Teachings)
                .ToList()
                .Select(x =>
                {
                    return new GroupReviewOutlineDTO
                    {
                        GroupReviewOutlineId = x.GroupReviewOutlineId,
                        NameGroupReviewOutline = x.NameGroupReviewOutline,
                        CreatedDate = x.CreatedDate,
                        CreatedBy = x.CreatedBy,
                        SLGV = _dbContext.Teachings.Where(t=>t.GroupReviewOutlineId == x.GroupReviewOutlineId).Count(),
                        SLSV = _dbContext.ProjectOutlines.Where(t => t.GroupReviewOutlineId == x.GroupReviewOutlineId).Count()
                    };
                })
                .ToList();
            return lstGroupReview;
        }

        public void Update(GroupReviewOutline data)
        {
            _dbContext.Update(data);
            _dbContext.SaveChanges();
        }
    }
}
