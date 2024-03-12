using AutoMapper;
using GP.Common.DTO;
using GP.Models.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GP.Common.Models;

namespace GP.Common.Helpers
{
    public class MappingProfile
    {
        private readonly IMapper _mapper;
        public MappingProfile()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // example//.ForMember(account => account.CreatedAt, act => act.MapFrom(dto => dto.CreatedAt)).ReverseMap();
                cfg.CreateMap<Teacher, AccountDTO>().ReverseMap(); 
                cfg.CreateMap<Student, AccountDTO>().ReverseMap();
                cfg.CreateMap<StudentModel, Student>().ReverseMap();
                cfg.CreateMap<TeacherModel, Teacher>().ReverseMap();

                cfg.CreateMap<TeacherDTO, Teacher>().ReverseMap();
                cfg.CreateMap<SemesterDTO, Semester>().ReverseMap();


                cfg.CreateMap<Student, LoginResponseDTO>().ReverseMap();
                cfg.CreateMap<Student, StudentDTO>().ReverseMap();

                cfg.CreateMap<Major, MajorDTO>().ReverseMap();


                cfg.CreateMap<Teacher, LoginResponseDTO>().ReverseMap();
                cfg.CreateMap<Project, ProjectDTO>().ReverseMap();

            });

            _mapper = config.CreateMapper();

        }
        public Teacher MapAccountDTOToTeacher(AccountDTO accountDTO)
        {
            return _mapper.Map<Teacher>(accountDTO);
        }
        public Student MapAccountDTOToStudent(AccountDTO accountDTO)
        {
            return _mapper.Map<Student>(accountDTO);
        }

        public Teacher MapTeacherDTOToTeacher(TeacherDTO teacherDTO)
        {
            return _mapper.Map<Teacher>(teacherDTO);
        }

        public Teacher MapTeacherModelToTeacher(TeacherModel teacherModel)
        {
            return _mapper.Map<Teacher>(teacherModel);
        }

        public TeacherDTO MapTeacherToTeacherDTO(Teacher teacher)
        {
            return _mapper.Map<TeacherDTO>(teacher);
        }

        public List<TeacherDTO> MapTeachersToTeacherDTOs(List<Teacher> teacher)
        {
            return _mapper.Map<List<TeacherDTO>>(teacher);
        }

        public Student MapStudentModelToStudent(StudentModel studentDTO)
        {
            return _mapper.Map<Student>(studentDTO);
        }

        public StudentModel MapStudentToStudentModel(Student student)
        {
            return _mapper.Map<StudentModel>(student);
        }

        public LoginResponseDTO MapTeacherToLoginResponseDTO(Teacher teacher)
        {
            return _mapper.Map<LoginResponseDTO>(teacher);
        }

        public LoginResponseDTO MapStudentToLoginResponseDTO(Student student)
        {
            return _mapper.Map<LoginResponseDTO>(student);
        }

        public Teacher MapLoginResponseDTOToTeacher(LoginResponseDTO loginResponseDTO)
        {
            return _mapper.Map<Teacher>(loginResponseDTO);
        }

        public Student MapLoginResponseDTOToStudent(LoginResponseDTO loginResponseDTO)
        {
            return _mapper.Map<Student>(loginResponseDTO);
        }

        public Project MapProjectDTOToProject(ProjectDTO projectDTO)
        {
            return _mapper.Map<Project>(projectDTO);
        }

        public ProjectDTO MapProjectToProjectDTO(Project project)
        {
            return _mapper.Map<ProjectDTO>(project);
        }

        public ProjectOutline MapProjectOutlineDTOToProjectOutline(ProjectOutlineDTO projectOutlineDTO)
        {
            return _mapper.Map<ProjectOutline>(projectOutlineDTO);
        }

        public ProjectOutlineDTO MapProjectOutlineToProjectOutlineDTO(ProjectOutline projectOutline)
        {
            return _mapper.Map<ProjectOutlineDTO>(projectOutline);
        }

        public StudentDTO MapStudentToStudentDTO(Student student)
        {
            return _mapper.Map<StudentDTO>(student);
        }

        public List<StudentDTO> MapStudentsToStudentDTOs(List<Student> students)
        {
            return _mapper.Map<List<StudentDTO>>(students);
        }

        public Major MapMajorDTOToMajor(MajorDTO students)
        {
            return _mapper.Map<Major>(students);
        }

        public Semester MapSemesterDTOToSemester(SemesterDTO semesterDTO)
        {
            return _mapper.Map<Semester>(semesterDTO);
        }
    }
}