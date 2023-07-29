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
        private readonly DatabaseContext context = new DatabaseContext();

        public IActionResult Login()
        {
            //var user = HttpContext.Session.GetString("ViterapiUser");
            //if (user != null)
            //{
            //    return RedirectToAction("Index", "User");
            //}
            //else
            //{
            return View();
            //}
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Login(User usr)
        {
            var allUser = userMan.GetAllUsers();
            var sorgu = allUser.Where(u => u.EMail == usr.EMail && u.Password == usr.Password).FirstOrDefault();

            if (sorgu != null)
            {

                //HttpContext.Session.SetString("JWToken", usr.Token);
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

                user.RoleId = 2;
                user.Password = user.Password;
                userMan.AddUser(user);

                return Json(1);//kayıt edildi
            }

            return Json(0);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("KUSYSUser");
            return RedirectToAction("Login");
        }

    }
}
