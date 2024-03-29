﻿using GP.Common.Models;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.IRepository
{
    public interface IProjectRepository
    {
        public Project GetProjectByUsername(string username);
        public List<Project> GetListProjectByUsernameMentor(string username_mentor,string semesterId);
        public Project AssignMentor(Teacher teacher,string username);
    }
}
