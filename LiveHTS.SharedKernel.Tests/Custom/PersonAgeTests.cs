using System;
using LiveHTS.SharedKernel.Custom;
using NUnit.Framework;

namespace LiveHTS.SharedKernel.Tests.Custom
{
    [TestFixture]
    public class PersonAgeTests
    {
        private PersonAge _personAge;

        [SetUp]
        public void SetUp()
        {
            _personAge=new PersonAge(50);
        }

        [Test]
        public void should_Display_AgeUnit()
        {
            var display = _personAge.ToString();

            Assert.That(display,Does.Contain("Years"));
            Console.WriteLine(_personAge);

            _personAge.AgeUnit = "M";
            display = _personAge.ToString();
            Assert.That(display, Does.Contain("Months"));
            Console.WriteLine(_personAge);

            _personAge.AgeUnit = "D";
            display = _personAge.ToString();
            Assert.That(display, Does.Contain("Days"));
            Console.WriteLine(_personAge);
        }

        [Test]
        public void should_Display_Full_AgeUnit()
        {
            _personAge=new PersonAge(10);
            var display = _personAge.ToFullAgeString();
            Assert.That(display, Does.Contain("Years"));
            Console.WriteLine(_personAge.ToFullAgeString());

            _personAge.Months = 2;
            display = _personAge.ToFullAgeString();
            Assert.That(display, Does.Contain("Months"));
            Console.WriteLine(_personAge.ToFullAgeString());

            _personAge.Days = 4;
            display = _personAge.ToFullAgeString();
            Assert.That(display, Does.Contain("Days"));
            Console.WriteLine(_personAge.ToFullAgeString());
        }
    }
}