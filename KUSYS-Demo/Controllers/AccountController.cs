using BusinessLayer.Management;
using DataAccessLayer.EntityFramework;
using EntityLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using DataAccessLayer.Context;

namespace KUSYS_Demo.Controllers
{

    public class AccountController : Controller
    {
        UserManagement userMan = new UserManagement(new EFUserRepository()); 
        CourseManagement courseMan = new CourseManagement(new EFCourseRepository());
        private readonly DatabaseContext context = new DatabaseContext();

        public IActionResult Login()
        {
            IlkDataEkle();
            return View();
            
        }

        //ilk tablo kayıtlarının atılması
        public void IlkDataEkle()
        {
            var rolesorgu = context.UserRole.Count();//burada  roller 1 kez ekleniyor
            if (rolesorgu == 0)
            {
                UserRole adminrole = new UserRole();
                adminrole.Name = "Admin";
                adminrole.Description = "Full Yetkili";
                context.Add(adminrole);
                context.SaveChanges();
                UserRole userrole = new UserRole();
                userrole.Name = "User";
                userrole.Description = "Kısıtlı Yetkili";
                context.Add(userrole);
                context.SaveChanges();
            }
            var coursesorgu = courseMan.GetAllCourse().Count();
            if (coursesorgu==0)
            {
                Course course1 = new Course();
                course1.CourseCode = "CSI101";
                course1.CourseName = "Introduction to Computer Science";
                courseMan.AddCourse(course1);
                Course course2 = new Course();
                course2.CourseCode = "CSI102";
                course2.CourseName = "Algorithms";
                courseMan.AddCourse(course2); 
                Course course3 = new Course();
                course3.CourseCode = "MAT101";
                course3.CourseName = "Calculus";
                courseMan.AddCourse(course3); 
                Course course4 = new Course();
                course4.CourseCode = "PHY101";
                course4.CourseName = "Physics";
                courseMan.AddCourse(course4);
            }
            var usersorgu=userMan.GetAllUsers().Count();
            if (usersorgu == 0)
            {
                User user = new User();
                user.Name = "Admin";
                user.Surname = "Admin";
                user.Age = 99;
                user.EMail = "admin@mail.com";
                user.RoleId = context.UserRole.Where(u => u.Name == "Admin").FirstOrDefault().Id;
                user.Password = "123123";
                userMan.AddUser(user);

            }
        }

        //kullanıcı login işlemi
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Login(User usr)
        {
            var allUser = userMan.GetAllUsers();
            var sorgu = allUser.Where(u => u.EMail == usr.EMail && u.Password == usr.Password).FirstOrDefault();

            if (sorgu != null)
            {

                HttpContext.Session.SetString("KUSYSUser", usr.EMail.ToString());
                HttpContext.Session.SetString("Name", sorgu.Name.ToString() + " " + sorgu.Surname.ToString());
                HttpContext.Session.SetString("SurName", sorgu.Surname.ToString());
                var rolename = context.UserRole.Where(u => u.Id == sorgu.RoleId).FirstOrDefault().Name;
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Role,rolename),
                    new Claim(ClaimTypes.Name,sorgu.Name.ToString()+" "+sorgu.Surname.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }
        public IActionResult Register()
        {
            return View();
        }

        //yeni kullanıcı eklenmesi
        [HttpPost]
        public JsonResult Register(User user)
        {
            if (user != null)
            {
                var kontrol = userMan.GetAllUsers().Where(u => u.EMail == user.EMail);

                if (kontrol.Count() > 0)
                {
                    return Json(0);
                }
                //register olan tüm kullanıcılar user olarak kaydedilir.

                user.RoleId = context.UserRole.Where(u=>u.Name=="User").FirstOrDefault().Id;
                user.Password = user.Password;
                userMan.AddUser(user);

                return Json(1);//kayıt edildi
            }

            return Json(0);
        }
        //çıkış işlemi
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("KUSYSUser");
            return RedirectToAction("Login");
        }

    }
}
