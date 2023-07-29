using EntityLayer.Models;
using KUSYS_Demo.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUnitTest
{
    public class ApplicationUserUnitTest
    {
        [Test]
        public void UserAddUnitTest()
        {
            var userCont = new UserController();

            var user = new User()
            {
               
                Name = "Test",
                Age = 1,
                EMail = "Test@mail.com",
                Password = "Test",
                RoleId = 2,
                Surname = "Test",
            };
            //Ekleme sayfasından name veya surname gelmez ise fail dönüş sağlanır.
            
            var Id = 1;
            var result = userCont.Create(user);
            if (result.Value.ToString() == "1")
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}
