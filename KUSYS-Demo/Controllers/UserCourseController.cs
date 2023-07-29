using BusinessLayer.Management;
using DataAccessLayer.Context;
using DataAccessLayer.EntityFramework;
using EntityLayer.Models;
using KUSYSDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.Controllers
{
    [Authorize]
    public class UserCourseController : Controller
    {
        CourseManagement courseMan = new CourseManagement(new EFCourseRepository());
        UserManagement userMan = new UserManagement(new EFUserRepository());
        StudentCourseInfManagement stcMan = new StudentCourseInfManagement(new EFStudentCourseInfRepository());
        private readonly DatabaseContext context = new DatabaseContext();

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult DataGetir()
        {
            List<UserInformationViewModel> lstModel = new List<UserInformationViewModel>();

            var list = stcMan.GetAllStudentCourseInf();
            var userInf = getUser();
            var role=context.UserRole.Where(u=>u.Id==userInf.RoleId).FirstOrDefault().Name;
            if (role == "Admin")
            {
                foreach (var item in list)
                {
                    var user = userMan.GetUserById(item.UserId);
                    var course = courseMan.GetCourseById(item.CourseId);
                    UserInformationViewModel userInformationViewModel = new UserInformationViewModel();
                    userInformationViewModel.CourseId = item.CourseId;
                    userInformationViewModel.CourseName = course.CourseName;
                    userInformationViewModel.CourseCode = course.CourseCode;
                    userInformationViewModel.NameSurname = user.Name + " " + user.Surname;
                    userInformationViewModel.UserId = user.Id;
                    userInformationViewModel.Id = item.Id;
                    lstModel.Add(userInformationViewModel);
                }
            }
            else
            {
                var getUserInf = list.Where(u => u.UserId == userInf.Id).ToList();
                foreach (var item in getUserInf)
                {
                    var user = userMan.GetUserById(item.UserId);
                    var course = courseMan.GetCourseById(item.CourseId);
                    UserInformationViewModel userInformationViewModel = new UserInformationViewModel();
                    userInformationViewModel.CourseId = item.CourseId;
                    userInformationViewModel.CourseName = course.CourseName;
                    userInformationViewModel.CourseCode = course.CourseCode;
                    userInformationViewModel.NameSurname = user.Name + " " + user.Surname;
                    userInformationViewModel.UserId = user.Id;
                    userInformationViewModel.Id = item.Id;
                    lstModel.Add(userInformationViewModel);
                }
            }

            return Json(lstModel);
        }
        public User getUser()
        {
            var getuser = HttpContext.Session.GetString("KUSYSUser");
            var userInf = userMan.GetAllUsers().Where(u => u.EMail == getuser).FirstOrDefault();
            
            return userInf;

        }
        public IActionResult Create()
        {
            var userInf = getUser();
            var role = context.UserRole.Where(u => u.Id == userInf.RoleId).FirstOrDefault().Name;
            if (role == "Admin")
            {
                ViewBag.UserList = userMan.GetAllUsers();
            }
            else
            {
                var list= userMan.GetAllUsers().Where(u => u.Id == userInf.Id).ToList();
                ViewBag.UserList = list;
            }
           
            ViewBag.CourseList = courseMan.GetAllCourse();
            return View();
        }
        [HttpPost]
        public JsonResult Create(StudentCourseInf data)
        {
            var lst = stcMan.GetAllStudentCourseInf();
            var count = 0;
            if (lst.Count != 0)
            {
                foreach (var item in lst)
                {
                    if (item.UserId == data.UserId && item.CourseId == data.CourseId)
                    {
                        count++;
                    }
                }
            }
            else
            {
                stcMan.AddStudentCourseInf(data);

                return Json(1);
            }

            if (count == 0)
            {
                stcMan.AddStudentCourseInf(data);
                return Json(1);
            }

            return Json(0);//aynı kullanıcı daha once bu dersi almıs

        }
        public IActionResult Edit(int Id)
        {
            var userInf = getUser();
            var role = context.UserRole.Where(u => u.Id == userInf.RoleId).FirstOrDefault().Name;
            if (role == "Admin")
            {
                ViewBag.UserList = userMan.GetAllUsers();
            }
            else
            {
                ViewBag.UserList = userMan.GetAllUsers().Where(u => u.Id == userInf.Id);
            }
            ViewBag.CourseList = courseMan.GetAllCourse();
            var data = stcMan.GetStudentCourseInfById(Id);
            return View(data);
        }
        [HttpPost]
        public JsonResult Edit(int id, StudentCourseInf data)
        {
            var lst = stcMan.GetAllStudentCourseInf();
            var count = 0;
            if (id == data.Id)
            {
                foreach (var item in lst)
                {
                    if (item.UserId != data.UserId && item.CourseId != data.CourseId)
                    {
                        stcMan.UpdateStudentCourseInf(data);
                        count++;
                    }
                }
            }
            if (count == 0 && lst.Count != 0)
            {
                return Json(0);//aynı kullanıcı daha once bu dersi almıs
            }
            else
            {
                return Json(1);
            }
        }
        public ActionResult Delete(int id)
        {

            var data = stcMan.GetStudentCourseInfById(id);
            if (data != null)
            {
                stcMan.DeleteStudentCourseInf(data);
            }

            return RedirectToAction(nameof(Index));
        }



    }
}
