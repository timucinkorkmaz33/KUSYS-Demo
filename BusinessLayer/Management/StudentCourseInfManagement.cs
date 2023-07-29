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
    public class StudentCourseInfManagement : IStudentCourseInfService
    {
        IStudentCourseInfDal _stcDal;

        public StudentCourseInfManagement(IStudentCourseInfDal stcDal)
        {
            _stcDal = stcDal;
        }

        public void AddStudentCourseInf(StudentCourseInf stc)
        {
            _stcDal.Add(stc);
        }
        public void DeleteStudentCourseInf(StudentCourseInf stc)
        {
            _stcDal.Delete(stc);
        }


        public List<StudentCourseInf> GetAllStudentCourseInf()
        {
            return _stcDal.GetList();
        }

        public StudentCourseInf GetStudentCourseInfById(int id)
        {
            return _stcDal.GetById(id);
        }

        public void UpdateStudentCourseInf(StudentCourseInf stc)
        {
            _stcDal.Update(stc);
        }
    }
}
