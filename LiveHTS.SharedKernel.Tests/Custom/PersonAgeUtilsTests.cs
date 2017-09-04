using System;
using LiveHTS.SharedKernel.Custom;
using NUnit.Framework;

namespace LiveHTS.SharedKernel.Tests.Custom
{
    [TestFixture]
    public class PersonAgeUtilsTests
    {
        private PersonAge _personAge;
        private DateTime _dob;

        [SetUp]
        public void SetUp()
        {
            _personAge = new PersonAge(3);
            _dob = DateTime.Today.AddYears(-10);
            _dob = _dob.AddMonths(-2);
            _dob = _dob.AddDays(-1);
        }

        [Test]
        public void should_CalculateBirthDate_From_AgeInYears()
        {
            var checkDate = DateTime.Today.AddYears(-3);
            var dob = Utils.CalculateBirthDate(_personAge);
            Assert.AreEqual(checkDate,dob);
            Console.WriteLine($"{_personAge} Born: {dob:yyyy MMMM dd}");
        }

        [Test]
        public void should_CalculateBirthDate_From_AgeInMonths()
        {
            var checkDate = DateTime.Today.AddMonths(-1);
            _personAge =PersonAge.CreateFromMonths(1);
            var dob = Utils.CalculateBirthDate(_personAge);
            Assert.AreEqual(checkDate, dob);
            Console.WriteLine($"{_personAge} Born: {dob:yyyy MMMM dd}");
        }

        [Test]
        public void should_CalculateBirthDate_From_AgeInDays()
        {
            var checkDate = DateTime.Today.AddDays(-2);
            _personAge = PersonAge.CreateFromDays(2);
            var dob = Utils.CalculateBirthDate(_personAge);
            Assert.AreEqual(checkDate, dob);
            Console.WriteLine($"{_personAge} Born: {dob:yyyy MMMM dd}");
        }

        [Test]
        public void should_Calculate_Age_In_Years_From_BirthDate()
        {
            var dob = _dob;
            var _personAge = Utils.CalculateAge(dob);
            var display = _personAge.ToString();
            Assert.That(display, Does.Contain("Years"));
            Console.WriteLine($"{_personAge} Born: {dob:yyyy MMMM dd} |{_personAge.ToFullAgeString()}");
        }
        [Test]
        public void should_Calculate_Age_In_Months_From_BirthDate()
        {
            var dob = DateTime.Today.AddMonths(-2);
            var _personAge = Utils.CalculateAge(dob);
            var display = _personAge.ToString();
            Assert.That(display, Does.Contain("Months"));
            Console.WriteLine($"{_personAge} Born: {dob:yyyy MMMM dd} |{_personAge.ToFullAgeString()}");
        }
        [Test]
        public void should_Calculate_Age_In_Days_From_BirthDate()
        {
            var dob = DateTime.Today.AddDays(-4);
            var _personAge = Utils.CalculateAge(dob);
            var display = _personAge.ToString();
            Assert.That(display, Does.Contain("Days"));
            Console.WriteLine($"{_personAge} Born: {dob:yyyy MMMM dd} |{_personAge.ToFullAgeString()}");
        }
    }
}