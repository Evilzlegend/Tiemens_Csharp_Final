using Microsoft.VisualStudio.TestTools.UnitTesting;
using DMVLaw.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMVLaw.Controllers;
using DMVLaw.Models;

namespace DMVLaw.Controllers.Tests
{
    [TestClass()]
    public class UnitTests
    {


        [TestMethod()]
        public void ValidRole()
        {

            User user = new User
            {
                FirstName = "Test",
                LastName = "User",
                Password = "admin",
                Role = "Law Enforcement"
            };

            Assert.AreEqual(user.Role, "Law Enforcement");
        }

        [TestMethod()]
        public void AuthenticationFailTest()
        {
            JwtAuthenticationManager manager = new JwtAuthenticationManager("thisisthekey1234");

            user testuser = new user
            {
                username = "FakeUser",
                password = "FakePassword",
                role = "Administrator"
            };

            var ret = manager.Authentication(testuser.username, testuser.password, testuser.role);

            Assert.IsNull(ret);
        }

        [TestMethod()]
        public void HasDriver()
        {

            Vehicle vehicle = new Vehicle
            {
                VehicleMake = "Toyota",
                VehicleModel = "Previa",
                VehicleColor = "Purple",
                VehiclePlate = "6BL-591",
                Driver = new Driver
                {
                    FirstName = "Dennis"
                }
            };

            Assert.IsNotNull(vehicle.Driver.FirstName);
        }

        [TestMethod()]
        public void AuthenticationSucceedTest()
        {
            JwtAuthenticationManager manager = new JwtAuthenticationManager("thisisthekey1234");

            user testuser = new user
            {
                username = "SKing",
                password = "Password11",
                role = "DMV Personnel"
            };

            var ret = manager.Authentication(testuser.username, testuser.password, testuser.role);

            Assert.IsNotNull(ret);
        }

        [TestMethod()]
        public void ValidInfraction()
        {

            Infraction inf = new Infraction
            {
                InfractionType = "Eating While Driving"
            };

            Assert.IsNotNull(inf.InfractionType);
        }


    }
}