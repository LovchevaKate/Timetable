using System;
using System.Collections.Generic;
using TimetableCore.BLL.DTO;
using TimetableCore.BLL.Interfaces;
using TimetableCore.DAL.Entities;
using TimetableCore.DAL.Repositories;

namespace TimetableCore.BLL.Services
{
    public class CourseService : ICourseService
    {
        EFUnitOfWork Database { get; set; }

        public CourseService(EFUnitOfWork uof)
        {
            Database = uof;
        }
        public void CreateCourse(CourseDTO courseDto)
        {
            if (courseDto == null)
            {
                throw new Exception("Error. course == null");
            }

            Course course = new Course
            {
                CourseNumber = courseDto.CourseNumber
            };

            Database.Courses.Create(course);
            Database.Save();
        }

        public void DeleteCourse(int id)
        {
            if (id == 0)
                throw new Exception("id == null");

            Database.Courses.Delete(id);
            Database.Save();
        }

        //public void Dispose()
        //{
        //    Database.Dispose();
        //}

        public CourseDTO GetCourse(int? id)
        {
            if (id == null)
                throw new Exception("id == null");

            var course = Database.Courses.Get(id.Value);

            if (course == null)
                throw new Exception("Course don't find");

            return new CourseDTO
            {
                Id = course.Id,
                CourseNumber = course.CourseNumber
            };
        }

        public IEnumerable<CourseDTO> GetCourses()
        {
            var courses = Database.Courses.GetAll();
            List<CourseDTO> coursedto = new List<CourseDTO>();

            if (courses == null)
            {
                throw new Exception("Error create list of courses");
            }

            foreach (var c in courses)
            {
                coursedto.Add(new CourseDTO
                {
                    Id = c.Id,
                    CourseNumber = c.CourseNumber
                });
            }

            return coursedto;
        }

        public void UpdateCourse(CourseDTO courseDto)
        {
            if (courseDto == null)
            {
                throw new Exception("error. update course bll");
            }

            Course course = new Course
            {
                Id = courseDto.Id,
                CourseNumber = courseDto.CourseNumber
            };

            Database.Courses.Update(course);
            Database.Save();
        }
    }
}
