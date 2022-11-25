﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample
{
    public class AccountControllerTestFixture
    {
        [Test]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            //Arrange TEST 1

            var accountController = new AccountController();

            //Act TEST2

            var actualResult = accountController.ValidateEmail(email);

            //Assert TEST 3

            Assert.AreEqual(expectedResult, actualResult);

        }

    }
}
