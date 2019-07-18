using System;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Custom;
using NUnit.Framework;

namespace LiveHTS.Core.Tests.Model.Subject
{
    public class PersonTests
    {
        [Test]
        public void should_Check_IsOverAge()
        {
            //15May1980
            //01May1980

            var person = new Person();
            person.BirthDate= new DateTime(1980,5,1);
            Assert.True(person.IsOverAge);
            Console.WriteLine($"{person.AgeInfo} over 1.5" );

            person.BirthDate= new DateTime(2017,6,15);
            Assert.True(person.IsOverAge);
            Console.WriteLine($"{person.AgeInfo} over 1.5" );

            person.BirthDate=DateTime.Now.AddMonths(-17);
            Assert.False(person.IsOverAge);
            Console.WriteLine($"{person.AgeInfo} under 1.5" );
        }
    }
}
