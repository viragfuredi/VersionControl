﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample
{
    public class AccountControllerTestFixture
    {
        [Test,
         TestCase("abcd1234", false),
         TestCase("irf@uni-corvinus", false),
         TestCase("irf.uni-corvinus.hu", false),
         TestCase("irf@uni-corvinus.hu", true)
        ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            //Arrange TEST 1

            var accountController = new AccountController();

            //Act TEST2

            var actualResult = accountController.ValidateEmail(email);

            //Assert TEST 3

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test,
         TestCase("abcd", false),
         TestCase("ABCD1234", false),
         TestCase("abcd1234", false),
         TestCase("a1", false),
         TestCase("aBcdeF1024", true)
        ]

        public void TestValidatePassword(string password, bool expectedResult)
        {
            //Arrange TEST 1

            var accountController = new AccountController();

            //Act TEST2

            var actualResult = accountController.ValidatePassword(password);

            //Assert TEST 3

            Assert.AreEqual(expectedResult, actualResult);
        }

        public bool RegexHívóVarázslóEmail(string email)
        {
            return Regex.IsMatch(
                email,
                @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
        }

        public bool RegexHívóVarázslóJelszó(string password)
        {
            return Regex.IsMatch(
                password,
                 @".{8}[a-z]+[A-Z]+[0-9]");
        }
    }

}

