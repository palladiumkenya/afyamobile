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
            var manifest = Manifest.Create(_form, _encounter);
            Assert.IsNotNull(manifest);
            Assert.IsFalse(manifest.HasQuestions());
            Assert.IsFalse(manifest.HasResponses());
            Console.WriteLine(manifest);

        }
       
        [TestMethod]
        public void should_Update_Manifest_Encounters()
        {
            var manifest = Manifest.Create(_form, _encounter);
            Assert.IsNotNull(manifest);
            Assert.IsFalse(manifest.HasQuestions());
            Assert.IsFalse(manifest.HasResponses());
            Console.WriteLine(manifest);
        }
    }
}