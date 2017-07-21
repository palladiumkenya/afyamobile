using System;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void should_Update_Encounter()
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
        public void should_Get_Question()
        {
            var manifest = Manifest.Create(_form, _encounter);
            var q = manifest.GetFirstQuestion();

            var question = manifest.GetQuestion(q.Id);

            Assert.IsNotNull(question);
            Console.WriteLine(question);
        }

        [TestMethod]
        public void should_Get_FirstQuestion()
        {
            var manifest = Manifest.Create(_form, _encounter);

            var q = manifest.GetFirstQuestion();

            Assert.AreEqual(1, q.Rank);
            Console.WriteLine(q);
        }

        [TestMethod]
        public void should_Get_NextRank_Question()
        {
            var manifest = Manifest.Create(_form, _encounter);
            var q = manifest.QuestionStore[0];

            var question= manifest.GetNextRankQuestionAfter(q.Id);

            Assert.AreEqual(2, question.Rank);
            Assert.IsNotNull(question);
            Console.WriteLine(question);
        }

        [TestMethod]
        public void should_Get_PreviousRank_Question()
        {
            var manifest = Manifest.Create(_form, _encounter);
            var q = manifest.QuestionStore[3];

            var question = manifest.GetPreviousRankQuestionBefore(q.Id);

            Assert.AreEqual(3, question.Rank);
            Assert.IsNotNull(question);
            Console.WriteLine(question);
        }

        [TestMethod]
        public void should_Get_Last_Response()
        {
            var manifest = Manifest.Create(_form, _encounter);
            var q = manifest.GetLastResponse();
            Assert.AreEqual(4, q.Question.Rank);
            Console.WriteLine(q.Question);
        }
    }
}