using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IStudentCourseInfService
    {
        void AddStudentCourseInf(StudentCourseInf stc);
        void UpdateStudentCourseInf(StudentCourseInf stc);
        void DeleteStudentCourseInf(StudentCourseInf stc);
        List<StudentCourseInf> GetAllStudentCourseInf();
        StudentCourseInf GetStudentCourseInfById(int id);
    }
}
