using System;
using LiveHTS.Core;
using LiveHTS.Core.Interfaces;
using LiveHTS.Infrastructure.Migrations;
using LiveHTS.Infrastructure.Repository.Survey;
using NUnit.Framework;
using SQLite;


namespace LiveHTS.Infrastructure.Tests.Repository.Survey
{
    [TestFixture]
    public class FormRepositoryTests
    {
        private SQLiteConnection _connection;
        private FormRepository _formRepository;
        private ILiveSetting _liveSetting;
        private readonly Guid _labFormId = new Guid("62040dcc-6260-11e7-907b-a6006ad3dba0");    //  HTS Lab


        [SetUp]
        public void SetUp()
        {
            _connection = new SQLiteConnection("testlivehts.db");
            Seeder.Seed(_connection);
            _liveSetting = new LiveSetting(_connection.DatabasePath);
            _formRepository = new FormRepository(_liveSetting,
                new QuestionRepository(_liveSetting,
                    new ConceptRepository(_liveSetting, new CategoryRepository(_liveSetting))));            
        }

        [Test]
        public void should_Get_Form_With_Questions()
        {
            var form = _formRepository.GetWithQuestions(_labFormId,true);
            Assert.IsNotNull(form);            
            Assert.IsTrue(form.Questions.Count>0);
            Console.WriteLine($"{form}");
            foreach (var question in form.Questions)
            {
                Assert.IsNotNull(question.Concept);
                if (question.HasValidations)
                    Assert.IsTrue(question.Validations.Count > 0);
                Console.WriteLine($"   {question} [{question.Concept}] validations[{question.Validations.Count}]");
            }
        }
    }
}