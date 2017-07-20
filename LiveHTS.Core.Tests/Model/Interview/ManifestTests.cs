using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;

namespace LiveHTS.Core.Tests.Model.Interview
{
    [TestClass]
    public class ManifestTests
    {
        private Form _form;
        private Encounter _encounter, _encounterNoObs;

        [TestInitialize]
        public void SetUp()
        {
            _form = TestHelpers.CreateTestFormWithQuestions(4);
            _encounter = TestHelpers.CreateTestEncountersWithObs(_form);
            _encounterNoObs = TestHelpers.CreateTestEncounters(_form);

        }

        [TestMethod]
        public void should_Create_Manifest()
        {
            var manifest = Manifest.Create(_form, _encounter);
            Assert.IsNotNull(manifest);
            Assert.IsTrue(manifest.HasQuestions());
            Assert.IsTrue(manifest.HasResponses());
            Console.WriteLine(manifest);

        }
        [TestMethod]
        public void should_Create_Manifest_No_Obs()
        {
            var manifest = Manifest.Create(_form, _encounterNoObs);
            Assert.IsNotNull(manifest);
            Assert.IsTrue(manifest.HasQuestions());
            Assert.IsFalse(manifest.HasResponses());
            Console.WriteLine(manifest);

        }

        [TestMethod]
        public void should_Update_Manifest_Encounters()
        {
            var manifest = Manifest.Create(_form, _encounterNoObs);
            Console.WriteLine(manifest);
            Assert.IsFalse(manifest.HasResponses());
            var updatedManifest = manifest;
            updatedManifest.UpdateEncounter(_encounter);
            Assert.IsTrue(updatedManifest.HasResponses());
            Console.WriteLine(new string('-',30));
            Console.WriteLine(updatedManifest);
        }
        [TestMethod]
        public void should_Get_FirstQuestion()
        {
            var manifest = Manifest.Create(_form, _encounter);
            var q = manifest.GetFirstQuestion();
            Assert.AreEqual(1,q.Rank);
            Console.WriteLine(q);
        }
        [TestMethod]
        public void should_Last_Question()
        {
            var manifest = Manifest.Create(_form, _encounter);
            var q = manifest.GetLastResponse();
            Assert.AreEqual(4, q.Question.Rank);
            Console.WriteLine(q.Question);
        }
    }
}