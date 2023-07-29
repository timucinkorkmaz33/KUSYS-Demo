using BusinessLayer.Management;
using DataAccessLayer.Context;
using DataAccessLayer.EntityFramework;
using EntityLayer.Models;
using KUSYS_Demo.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace KUSYS_Demo.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
     //bu işlem yapılabilemesi için yazıldı fakat sonrasında seed method yazıldığı için butonlar kapatıldı.  
        CourseManagement courseMan = new CourseManagement(new EFCourseRepository());
        UserManagement userMan = new UserManagement(new EFUserRepository());

        public IActionResult Index()
        {
            return View();
        }
        //tüm derslerin listelenmesi
        public JsonResult DataGetir()
        {
            var list = courseMan.GetAllCourse();
            return Json(list);
        }
        //yeni ders ekleme
        public IActionResult Create()
        {
            //ViewBag.UserList = userMan.GetAllUsers();
            return View();
        }
        //ders ekleme işlemi
        [HttpPost]
        public JsonResult Create(Course course)
        {
            if(course.CourseCode!="" && course.CourseName != "")
            {
                courseMan.AddCourse(course);
                return Json(1);
            }
            return Json(0);
        }
            
        
        public IActionResult Edit(int id)
        {
                var course = courseMan.GetCourseById(id);
                if (course == null)
                {
                    return NotFound();
                }
                return View(course);
            
        }
        //ders güncelleme işlemi
        [HttpPost]
        public JsonResult Edit(int id, Course course)
        {
            if (id==course.Id && course.CourseCode != "" && course.CourseName != "")
            {
                    courseMan.UpdateCourse(course);
                    return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
        //ders silme işlemi
        public ActionResult Delete(int id)
        {

            var course = courseMan.GetCourseById(id);
            if (course != null)
            {
                courseMan.DeleteCourse(course);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
