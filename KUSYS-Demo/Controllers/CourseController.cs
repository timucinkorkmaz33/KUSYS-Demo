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
       
        CourseManagement courseMan = new CourseManagement(new EFCourseRepository());
        UserManagement userMan = new UserManagement(new EFUserRepository());

       

        public IActionResult Index()
        {

            return View();
        }
        public JsonResult DataGetir()
        {
            var list = courseMan.GetAllCourse();
            return Json(list);
        }

        public IActionResult Create()
        {
            //ViewBag.UserList = userMan.GetAllUsers();
            return View();
        }


        [HttpPost]
        public JsonResult Create(Course course)
        {
            courseMan.AddCourse(course);
            return Json(1);
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

        [HttpPost]
        public JsonResult Edit(int id, Course course)
        {
            if (id== course.Id)
            {
                courseMan.UpdateCourse(course);
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

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
