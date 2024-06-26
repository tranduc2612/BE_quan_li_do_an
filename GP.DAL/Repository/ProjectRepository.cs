﻿using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.Repository
{
    public class ProjectRepository: IProjectRepository
    {
        private readonly ManagementGraduationProjectContext _dbContext;
        private readonly MappingProfile _mapper;

        public ProjectRepository(ManagementGraduationProjectContext dbContext, MappingProfile mapper) {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<Project> GetListProjectByCouncilId(string semesterId, string councilId)
        {
            return _dbContext.Projects.Include(x=>x.ProjectOutline).Include(x=>x.UserNameNavigation)
                .Where(x => x.SemesterId == semesterId 
                      && 
                      (String.IsNullOrEmpty(councilId) || councilId == x.CouncilId)
                      &&
                      x.ProjectOutline != null
                      ).ToList();
        }

        public List<ProjectDTO> GetListProjectByGroupId(string semesterId, string groupId)
        {

            var query = _dbContext.Projects.Include(x => x.ProjectOutline).ThenInclude(x => x.GroupReviewOutline).Include(x => x.UserNameNavigation)
                        .Where(p => p.SemesterId == semesterId && (string.IsNullOrEmpty(groupId) || p.ProjectOutline.GroupReviewOutlineId == groupId));
            List<ProjectDTO> result = _mapper.MapProjectsToProjectDTOs(query.ToList());
            return result;
        }

        public List<Project> GetListProjectByUsernameMentor(string username_mentor,string semesterId)
        {
            //var query = from project in _dbContext.Projects
            //join student in _dbContext.Students on project.UserName equals student.UserName
            //where project.SemesterId == semesterId && project.UserNameMentor == username_mentor
            //select project;
            //return query.ToList();
            return _dbContext.Projects.Include(x => x.UserNameNavigation).Include(x=>x.ProjectOutline).Where(x => x.SemesterId == semesterId && x.UserNameMentor == username_mentor).ToList();
        }

        public Project GetProjectByHashKeyMentor(string key)
        {
            return _dbContext.Projects.Include(x=>x.UserNameMentorNavigation).ThenInclude(x=>x.Major)
                .Include(x => x.UserNameMentorNavigation).ThenInclude(x => x.Education)
                .Include(x=>x.ProjectOutline).Include(x=>x.UserNameNavigation).FirstOrDefault(x => x.HashKeyMentor == key);
        }

        public Project GetProjectByHashKeyCommentator(string key)
        {
            return _dbContext.Projects.Include(x => x.UserNameCommentatorNavigation).ThenInclude(x => x.Major)
                .Include(x => x.UserNameCommentatorNavigation).ThenInclude(x => x.Education)
                .Include(x => x.ProjectOutline)
                .Include(x => x.UserNameNavigation)
                .FirstOrDefault(x => x.HashKeyCommentator == key);
        }

        public Project GetProjectByUsername(string username)
        {
            return _dbContext.Projects.FirstOrDefault(x=>x.UserName == username);
        }

        public Project GetProjectByUsernameData(string username)
        {
            return _dbContext.Projects.Include(x => x.ProjectOutline).Include(x => x.UserNameMentorNavigation).ThenInclude(x=>x.Major).Include(x => x.UserNameNavigation).FirstOrDefault(x => x.UserName == username);
        }

        public Project Update(Project project)
        {
            _dbContext.Update(project);
            _dbContext.SaveChanges();
           return project;
        }

        public List<Project> GetListProjectBySemesterId(string semesterId)
        {
            List<Project> lst = _dbContext.Projects.Include(x => x.UserNameNavigation).Include(x => x.ProjectOutline).Where(x => x.SemesterId == semesterId).ToList();
            return lst;
        }

        public void CallProcAutoMationAssignMentor(string semesterId)
        {

            _dbContext.Database.ExecuteSqlRaw("EXECUTE AutoAssignMentor @semesterId",
                new SqlParameter("@semesterId", semesterId));
        }
    }
}
