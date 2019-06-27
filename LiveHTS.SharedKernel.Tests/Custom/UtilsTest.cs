using System;
using LiveHTS.SharedKernel.Custom;
using NUnit.Framework;

namespace LiveHTS.SharedKernel.Tests.Custom
{
    [TestFixture]
    public class UtilsTest
    {
        [Test]
        public void should_GenId()
        {
            var nos = Utils.GenId();
            var nos2 = Utils.GenId();
            Assert.AreNotEqual(nos,nos2);
            Console.WriteLine(nos);
            Console.WriteLine(nos2);

        }
        [Test]
        public void should_CheckDateGreaterThanLimit()
        {
            //15May1980
            //01May1980

            var dob = new DateTime(1980,5,1);
            Assert.True(Utils.CheckDateGreaterThanLimit(dob,1,5));
            var personAge=SharedKernel.Custom.Utils.CalculateAge(dob);
            Console.WriteLine($"{personAge.ToFullAgeString()} over 1.5" );

            var dob2 = new DateTime(2017,6,15);
            Assert.True(Utils.CheckDateGreaterThanLimit(dob2,1,5));
            personAge=SharedKernel.Custom.Utils.CalculateAge(dob2);
            Console.WriteLine($"{personAge.ToFullAgeString()} over 1.5" );

            dob = DateTime.Now.AddMonths(-17);
            personAge=SharedKernel.Custom.Utils.CalculateAge(dob);
            Assert.False(Utils.CheckDateGreaterThanLimit(dob,1,5));
            Console.WriteLine($"{personAge.ToFullAgeString()} under 1.5" );
        }
    }
}
