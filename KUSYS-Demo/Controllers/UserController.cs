using BusinessLayer.Management;
using DataAccessLayer.Context;
using DataAccessLayer.EntityFramework;
using EntityLayer.Models;
using KUSYS_Demo.Controllers;
using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KUSYS_Demo.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        UserManagement userMan = new UserManagement(new EFUserRepository());
        private readonly DatabaseContext context = new DatabaseContext();
   

        public IActionResult Index()
        {

            return View();
        }
        //tüm kullanıcıların listelenmesi
        public JsonResult DataGetir()
        {
            var list = userMan.GetAllUsers();
            var role=context.UserRole;
            List<UserVM> userList = new List<UserVM>();
            foreach(var item in list)
            {
                UserVM user=new UserVM();
                user.Name = item.Name;
                user.RoleName = role.Where(u => u.Id == item.RoleId).FirstOrDefault().Name;
                user.RoleId = item.RoleId;
                user.Surname = item.Surname;
                user.Age = item.Age;
                user.EMail = item.EMail;
                user.Id=item.Id;
                userList.Add(user);
            }
            return Json(userList);
        }

        public IActionResult Create()
        {
            ViewBag.RoleList = context.UserRole.ToList();
            return View();
        }
        //Yeni kullanıcı ekleme

        [HttpPost]
        public JsonResult Create(User user)
        {
            if (user.Name != "" && user.Surname != "" )
            {
               
                userMan.AddUser(user);
                return Json(1);
            }
            return Json(0);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.RoleList = context.UserRole.ToList();
            var User = userMan.GetUserById(id);
            if (User == null)
            {
                return NotFound();
            }
            return View(User);
        }
        //kullanıcı düzenleme
        
 
        [HttpPost]
        public JsonResult Edit(User user)
        {
            if (user.Name != "" && user.Surname != "")
            {
                userMan.UpdateUser(user);
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
        //kullanıcı detayı
        public PartialViewResult UserDetail(int id)
        {
            var User = userMan.GetUserById(id);
            return PartialView("_UserDetail", User);
        }

        //kullanıcı silme işlemi
        public ActionResult Delete(int id)
        {

            var user = userMan.GetUserById(id);
            if (user != null)
            {
              userMan.DeleteUser(user);
            }
         
            return RedirectToAction(nameof(Index));
        }

    }
}
