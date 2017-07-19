using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiveHTS.Core.Tests.Model.Interview
{
    [TestClass]
    public class ManifestTests
    {
        private Form _form;
        private Encounter _encounter;

        [TestInitialize]
        public void SetUp()
        {
            _form = Builder<Form>.CreateNew().Build();
            _encounter = Builder<Encounter>.CreateNew().Build();
        }

        [TestMethod]
        public void should_Create_Manifest()
        {
            var manifest = Manifest.Create(_form, _encounter,Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() , Guid.NewGuid());
            Assert.IsNotNull(manifest);
            Assert.IsFalse(manifest.HasQuestionStore());
            Assert.IsFalse(manifest.HasAnsweredQuestionStore());
            Console.WriteLine(manifest);

        }
        [TestMethod]
        public void should_Get_Encounter()
        {
            _encounter.Obses = Builder<Obs>.CreateListOfSize(2)
                .All()
                .With(x => x.EncounterId = _encounter.Id)
                .Build()
                .ToList();
            var manifest = Manifest.Create(_form, _encounter, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            Assert.IsNotNull(manifest);
            var encounter = manifest.GetEncounter();
            Assert.IsNotNull(encounter);
            Console.WriteLine($"{manifest} >>> {encounter.Id}");
            
            
        }
        [TestMethod]
        public void should_Create_Manifest_No_Data()
        {
            var manifest = Manifest.Create(null, null, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            Assert.IsNotNull(manifest);
            Assert.IsFalse(manifest.HasQuestionStore());
            Assert.IsFalse(manifest.HasAnsweredQuestionStore());
            Console.WriteLine(manifest);

            var encounter = manifest.GetEncounter();
            Assert.IsNull(encounter);
        }
    }
}