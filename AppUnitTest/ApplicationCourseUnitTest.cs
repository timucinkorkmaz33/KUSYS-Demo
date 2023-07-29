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
    public class ApplicationCourseUnitTest
    {
        [Test]
        public void AddCourseUnitTest()
        {
            var courseCont = new CourseController();

            var course = new Course()
            {

              CourseName = "Test",
              CourseCode = "Test",
              
            };
            //Ekleme sayfasından CourseName veya CourseCode gelmez ise fail dönüş sağlanır.

            var Id = 1;
            var result = courseCont.Create(course);
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
