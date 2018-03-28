using System;
using System.Collections.Generic;
using System.IO;
using FizzWare.NBuilder;
using LiveHTS.Core.Annotations;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.SmartCard;
using LiveHTS.SharedKernel.Custom;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LiveHTS.Core.Tests.Model.SmartCard
{
    [TestFixture]
    public class SHRTests
    {
        private string _shrMessageWitHtsId;
        private string _shrMessageWitNoHtsId;

        private Guid _pos = new Guid("b25efd8a-852f-11e7-bb31-be2e44b06b34");
        private Guid _neg = new Guid("b25efb78-852f-11e7-bb31-be2e44b06b34");
        private Guid _inc = new Guid("b25f017c-852f-11e7-bb31-be2e44b06b34");   

        [SetUp]
        public void SetUp()
        {
            var folder = TestContext.CurrentContext.TestDirectory.HasToEndWith(@"\");
            _shrMessageWitHtsId = File.ReadAllText($@"{folder}\Json\shrHts.json");
            _shrMessageWitNoHtsId = File.ReadAllText($@"{folder}\Json\shrNoHts.json");
        }
        [Test]
        public void should_Create_Blank()
        {
            var shr = SHR.CreateBlank();
            Assert.NotNull(shr);
            var json = JsonConvert.SerializeObject(shr);
            Console.WriteLine(json);

        }
        [Test]
        public void should_AssignHTS()
        {
            Assert.False(string.IsNullOrWhiteSpace(_shrMessageWitNoHtsId));
            var shr = JsonConvert.DeserializeObject<SHR>(_shrMessageWitNoHtsId);
            Assert.NotNull(shr);
            Assert.False(shr.HasHtsNumber());
            shr.AssignHtsNumber("10001");
            Assert.True(shr.HasHtsNumber());
            foreach (var htsInternalpatientid in shr.GetHtsInternalpatientids())
            {
                Console.WriteLine(htsInternalpatientid);
            }
             
        }
        [Test]
        public void should_Not_AssignHTS()
        {
            Assert.False(string.IsNullOrWhiteSpace(_shrMessageWitHtsId));
            var shr = JsonConvert.DeserializeObject<SHR>(_shrMessageWitHtsId);
            Assert.NotNull(shr);
            Assert.True(shr.HasHtsNumber());
            foreach (var htsInternalpatientid in shr.GetHtsInternalpatientids())
            {
                Console.WriteLine(htsInternalpatientid);
            }
        }

        [Test]
        public void should_Append_Unique_Tests()
        {
            var initialTestResult = new ObsFinalTestResult {FinalResult = _pos};
            var initalTestDate = DateTime.Today.Date;
            var initialFacility = "10000";

            var shr = SHR.CreateBlank();
            shr.HIV_TEST.Add(HIVTEST.Create(initalTestDate, initialTestResult, initialFacility));
            Assert.True(shr.HIV_TEST.Count == 1);

            var newTestResult = new ObsFinalTestResult { FinalResult = _pos };
            var newTestDate = DateTime.Today.Date;
            var newinitialFacility = "10000";

            shr.UpdateTesting(newTestDate, newTestResult, newinitialFacility);

            
            Assert.True(shr.HIV_TEST.Count==1);
            foreach (var hivtest in shr.HIV_TEST)
            {
                Console.WriteLine(hivtest);
            }
        }

        [Test]
        public void should_Append_New_Tests()
        {
            var initialTestResult = new ObsFinalTestResult { FinalResult = _pos };
            var initalTestDate = DateTime.Today.Date;
            var initialFacility = "10000";

            var shr = SHR.CreateBlank();
            shr.HIV_TEST.Add(HIVTEST.Create(initalTestDate, initialTestResult, initialFacility));
            Assert.True(shr.HIV_TEST.Count == 1);

            var newTestResult = new ObsFinalTestResult { FinalResult = _pos };
            var newTestDate = DateTime.Today.Date;
            var newinitialFacility = "10001";

            shr.UpdateTesting(newTestDate, newTestResult, newinitialFacility);


            Assert.True(shr.HIV_TEST.Count == 2);
            foreach (var hivtest in shr.HIV_TEST)
            {
                Console.WriteLine(hivtest);
            }
        }
    }
}