using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Repositories;
using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Management
{
    public class CourseManagement : ICourseService
    {
        ICourseDal _course;

        public CourseManagement(ICourseDal course)
        {
            _course = course;
        }

        public void AddCourse(Course course)
        {
            _course.Add(course);
        }

        public void DeleteCourse(Course course)
        {
            _course.Delete(course);
        }

        public List<Course> GetAllCourse()
        {
            return _course.GetList();
        }

        public Course GetCourseById(int id)
        {
            return _course.GetById(id);
        }

        public void UpdateCourse(Course course)
        {
            _course.Update(course);
        }
    }
}
