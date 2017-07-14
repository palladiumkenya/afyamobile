using System;
using System.Linq;
using LiveHTS.Core;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Lookup;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.Repository.Survey;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;


namespace LiveHTS.Infrastructure.Tests.Repository.Survey
{
    [TestClass]
    public class QuestionRepositoryTests
    {
        private ILiveSetting _liveSetting;
        private SQLiteConnection _database = TestHelpers.GetDatabase();
        private IQuestionRepository _questionRepository;


        [TestInitialize]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _questionRepository = new QuestionRepository(_liveSetting,
                new ConceptRepository(_liveSetting, new CategoryRepository(_liveSetting)));
        }

        [TestMethod]
        public void should_Get_Question_with_Concept()
        {
            var questions = _questionRepository.GetWithConcepts().ToList();
            Assert.IsTrue(questions.Count > 0);
            foreach (var question in questions)
            {
                Assert.IsNotNull(question);
                Console.Write(question);
                Assert.IsNotNull(question.Concept);
                Console.Write($" [{question.Concept}]");
                if (question.Concept.CategoryId.HasValue)
                {
                    Assert.IsNotNull(question.Concept.Category);
                    Assert.AreEqual(question.Concept.CategoryId, question.Concept.Category.Id);
                    Console.Write($" ({question.Concept.Category})");
                    Console.WriteLine();
                    foreach (var categoryItem in question.Concept.Category.Items)
                    {
                        Console.WriteLine($"   >. {categoryItem}");
                    }
                }
                else
                {
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        [TestMethod]
        public void should_Get_Question_with_Metadata()
        {
            var questions = _questionRepository.GetWithConcepts().ToList();
            Assert.IsTrue(questions.Count > 0);
            foreach (var question in questions)
            {
                Assert.IsNotNull(question);
                Console.Write(question);
                Assert.IsNotNull(question.Concept);
                Console.Write($" [{question.Concept}]");
                if (question.Concept.CategoryId.HasValue)
                {
                    Assert.IsNotNull(question.Concept.Category);
                    Assert.AreEqual(question.Concept.CategoryId, question.Concept.Category.Id);
                    Console.Write($" ({question.Concept.Category})");
                    Console.WriteLine();
                    foreach (var categoryItem in question.Concept.Category.Items)
                    {
                        Console.WriteLine($"   >. {categoryItem}");
                    }
                }
                else
                {
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}