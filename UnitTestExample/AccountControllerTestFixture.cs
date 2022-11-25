using NUnit.Framework;
using System;
using System.Activities;
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
         TestCase("abcd1024", false),
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
         TestCase("ABCD1024", false),
         TestCase("abcd1024", false),
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

            //nem jó :(
        }

        public bool RegexHívóVarázslóJelszó(string password)
        {
            return Regex.IsMatch(
                password,
                 @".{8}[a-z]+[A-Z]+[0-9]");

            //nem jó :(
        }

        [Test,
         TestCase("irf@uni-corvinus.hu", "Abcd1024"),
         TestCase("irf@uni-corvinus.hu", "Abcd1024"),
        ]
        public void TestRegisterHappyPath(string email, string password)
        {
            //arrange

            var accountController = new AccountController();

            //act

            var actualResult = accountController.Register(email, password);

            //assert

            Assert.AreEqual(email, actualResult.Email);
            Assert.AreEqual(password, actualResult.Password);
            Assert.AreNotEqual(Guid.Empty, actualResult.ID);
        }

        [Test,
         TestCase("irf@uni-corvinus", "Abcd1024"),
         TestCase("irf.uni-corvinus.hu", "Abcd1024"),
         TestCase("irf@uni-corvinus.hu", "abcd1024"),
         TestCase("irf@uni-corvinus.hu", "ABCD1024"),
         TestCase("irf@uni-corvinus.hu", "abcdABCD"),
         TestCase("irf@uni-corvinus.hu", "a1"),
        ]

        public void TestRegisterValidateException(string email, string password)
        {
            //arrange
            var accountController = new AccountController();

            //act
            try
            {
                var actualResult = accountController.Register(email, password);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ValidationException>(ex);
            }

            //assert nincs, belecsúszott az act-ba
        }
    }

}

