using System;
using System.IO;
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
    }
}