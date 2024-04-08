using GP.Common.DTO;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Numerics;
using GP.Common.Helpers;
using static Azure.Core.HttpHeader;
using System.Drawing;

namespace GP.DAL.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ManagementGraduationProjectContext _dbContext;
        private readonly MappingProfile _mapper;
        private readonly HelperCommon _common;

        public StudentRepository(ManagementGraduationProjectContext dbContext, MappingProfile mapper, HelperCommon common)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _common = common;
        }

        public PaginatedResultBase<StudentDTO> GetList(StudentListModel data)
        {
            PaginatedResultBase<StudentDTO> result = new PaginatedResultBase<StudentDTO>();
            var query = (from student in _dbContext.Students
                         join project in _dbContext.Projects on student.UserName equals project.UserName
                         join Classification in _dbContext.Classifications on student.Status equals Classification.Code
                         join teacherMentor in _dbContext.Teachers on project.UserNameMentor equals teacherMentor.UserName into mentorJoin
                         from mentor in mentorJoin.DefaultIfEmpty()
                         join teacherCommentator in _dbContext.Teachers on project.UserNameCommentator equals teacherCommentator.UserName
                         into commentatorJoin
                         from commentator in commentatorJoin.DefaultIfEmpty()
                         where student.IsDelete == 0 && Classification.TypeCode == "STATUS_SYSTEM" &&
                               (string.IsNullOrEmpty(data.FullName) || student.FullName.Contains(data.FullName)) &&
                               (string.IsNullOrEmpty(data.StudentCode) || student.StudentCode.Contains(data.StudentCode)) &&
                               (string.IsNullOrEmpty(data.ClassName) || student.ClassName == data.ClassName) &&
                               (string.IsNullOrEmpty(data.MajorId) || student.MajorId == data.MajorId) &&
                               (string.IsNullOrEmpty(data.UserName) || student.UserName == data.UserName) &&
                               (string.IsNullOrEmpty(data.SchoolYear) || student.SchoolYearName.Contains(data.SchoolYear)) &&
                               (string.IsNullOrEmpty(data.Status) || student.Status == data.Status) &&
                               (string.IsNullOrEmpty(data.SemesterId) || student.Project.SemesterId == data.SemesterId)
                         select new
                         {
                             Student = student,
                             Major = student.Major,
                             Project = student.Project,
                             Semester = student.Project.Semester,
                             MentorUserName = mentor,
                             CommentatorUserName = commentator,
                             StatusStudent = Classification.Value,
                         });
            int totalItem = query.Count();

            //int totalItem = _dbContext.Students.Count(x =>
            //       x.IsDelete == 0 &&
            //       (String.IsNullOrEmpty(data.FullName) || x.FullName.Contains(data.FullName)) 
            //       &&
            //       (String.IsNullOrEmpty(data.StudentCode) || x.StudentCode.Contains(data.StudentCode))
            //       &&
            //       (String.IsNullOrEmpty(data.ClassName) || x.ClassName.Contains(data.ClassName))
            //       &&
            //       (String.IsNullOrEmpty(data.MajorId) || x.MajorId == data.MajorId)
            //       &&
            //       (String.IsNullOrEmpty(data.UserName) || x.UserName.Contains(data.UserName))
            //       &&
            //       (String.IsNullOrEmpty(data.SchoolYear) || x.SchoolYearName.Contains(data.SchoolYear))
            //       &&
            //       (String.IsNullOrEmpty(data.Status) || x.Status == data.Status)
            //       &&
            //       (String.IsNullOrEmpty(data.SemesterId) || x.Project.SemesterId == data.SemesterId)
            //       );

            List<Student> lstStudent = query.Skip((data.PageIndex - 1) * data.PageSize)
                 .Take(data.PageSize)
                 .ToList()
                 .Select(x =>
                 {
                     x.Student.Major = x.Major;
                     x.Student.Project = x.Project;
                     x.Student.Project.Semester = x.Semester;
                     x.Student.Project.UserNameMentorNavigation = x.MentorUserName != null ? x.MentorUserName : null;
                     x.Student.Project.UserNameCommentatorNavigation = x.CommentatorUserName != null ? x.CommentatorUserName : null;
                     return  x.Student
;
                 })
                 .ToList();

            result.ListResult = _mapper.MapStudentsToStudentDTOs(lstStudent);
            result.TotalItem = totalItem;
            result.PageIndex = data.PageIndex;
            //var query = from student in _dbContext.Students
            //            join project in _dbContext.Projects on student.UserName equals project.UserName
            //            where ((String.IsNullOrEmpty(data.FullName) || student.FullName.Contains(data.FullName)) &&
            //                   (String.IsNullOrEmpty(data.StudentCode) || student.StudentCode.Contains(data.StudentCode)))
            //            select new
            //            {
            //                Student = student,
            //                Project = project
            //            };

            //var result = new PaginatedResultBase<ProfileStudentDTO>
            //{
            //    TotalPage = query.Count(),
            //    PageIndex = data.PageIndex,
            //    ListResult = query
            //        .Skip((data.PageIndex - 1) * data.PageSize)
            //        .Take(data.PageSize)
            //        .Select(x => new ProfileStudentDTO
            //            (
            //             x.Student.UserName,
            //            x.Student.FullName,
            //            x.Student.Dob,
            //            x.Student.Phone,
            //            x.Student.Email,
            //            x.Student.Avatar,
            //            x.Student.CreatedAt,
            //            x.Student.CreatedBy,
            //            x.Student.Status,
            //            x.Student.StudentCode,
            //            x.Student.ClassName,
            //            x.Student.MajorId,
            //            x.Student.SchoolYearName,
            //            x.Project.SemesterId,
            //            x.Project.ScoreFinal,
            //            x.Project.ScoreUv1,
            //            x.Project.ScoreGvhd,
            //            x.Project.ScoreGvpb,
            //            x.Project.CommentUv1,
            //            x.Project.ScoreUv2,
            //            x.Project.CommentUv2,
            //            x.Project.ScoreUv3,
            //            x.Project.CommentUv3
            //        ))
            //    .ToList()
            //};



            return result;
        }

        public Student Create(StudentModel studentDTO)
        {


            Student student = _mapper.MapStudentModelToStudent(studentDTO);
            Project new_project = new Project();
            AuthHelper.CreatePassHash(studentDTO.PasswordText, out byte[] passwordHash, out byte[] passwordSalt);
            student.Password = passwordHash;
            student.PasswordSalt = passwordSalt;
            student.UserName = _common.GennerationUsernameStudent(studentDTO.FullName, studentDTO.StudentCode, studentDTO.SemesterId);
            _dbContext.Students.Add(student);

            new_project.UserName = student.UserName;
            new_project.SemesterId = studentDTO.SemesterId;
            _dbContext.Projects.Add(new_project);
            
            _dbContext.SaveChanges();
            return student;
        }

        public bool Delete(string username)
        {
            Student studentFind = _dbContext.Students.FirstOrDefault(x => x.UserName == username);
            if(studentFind != null) {
                studentFind.IsDelete = 1;
                _dbContext.Students.Update(studentFind);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Student Get(string username)
        {
           return _dbContext.Students
                   .Include(x => x.Project)
                   .ThenInclude(x => x.UserNameCommentatorNavigation)
                   .Include(x => x.Project)
                   .ThenInclude(x => x.UserNameMentorNavigation)
                   .Include(x => x.Project)
                   .ThenInclude(x => x.Semester)
                   .Include(x => x.Project)
                   .ThenInclude(x => x.Council)
                   .FirstOrDefault(x=>x.UserName==username);
        }

        public Student Update(Student student)
        {
            _dbContext.Students.Update(student);
            _dbContext.SaveChanges();
            return student;
        }
    }
}
