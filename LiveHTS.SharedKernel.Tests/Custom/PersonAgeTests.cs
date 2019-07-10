﻿using System;
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

        [Test]
        public void should_Check_IsPead()
        {
            var dob = DateTime.Now.AddYears(-14);
            var personAge=SharedKernel.Custom.Utils.CalculateAge(dob);
            Assert.True(personAge.Age<15);
            Console.WriteLine( personAge.ToFullAgeString());
        }

        [Test]
        public void should_Check_IsUnderAge()
        {
            var dob = DateTime.Now.AddMonths(-18);
            var personAge=SharedKernel.Custom.Utils.CalculateAge(dob);
            Assert.True(Utils.CheckDateGreaterThanLimit(dob,1,5));
            Console.WriteLine($"{personAge.ToFullAgeString()} over 1.5" );

            dob = DateTime.Now.AddMonths(-17);
            personAge=SharedKernel.Custom.Utils.CalculateAge(dob);
            Assert.False(Utils.CheckDateGreaterThanLimit(dob,1,5));
            Console.WriteLine($"{personAge.ToFullAgeString()} under 1.5" );
        }

        [Test]
        public void should_Check_IsUnderAge_1980()
        {
            //15May1980
            //01May1980

            var dob = new DateTime(1980,5,1);

            var personAge=SharedKernel.Custom.Utils.CalculateAge(dob);
            Assert.False(Utils.CheckDateGreaterThanLimit(dob,1,5));
            Console.WriteLine($"{personAge.ToFullAgeString()} over 1.5" );

            dob = DateTime.Now.AddMonths(-17);
            personAge=SharedKernel.Custom.Utils.CalculateAge(dob);
            Assert.False(Utils.CheckDateGreaterThanLimit(dob,1,5));
            Console.WriteLine($"{personAge.ToFullAgeString()} under 1.5" );
        }
    }
}
