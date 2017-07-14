using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.Repository.Survey;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;


namespace LiveHTS.Infrastructure.Tests.Repository.Survey
{
    [TestClass]
    public class FormRepositoryTests
    {
        private ILiveSetting _liveSetting;
        private SQLiteConnection _database = TestHelpers.GetDatabase();
        private IFormRepository _formRepository;
        private Module _module;
        private Form _labForm;

        [TestInitialize]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _formRepository = new FormRepository(_liveSetting,
                new QuestionRepository(_liveSetting,
                    new ConceptRepository(_liveSetting, new CategoryRepository(_liveSetting))));
            _module = new ModuleRepository(_liveSetting).GetDefaultModule();
            _labForm = _module.Forms.FirstOrDefault(x => x.Name.ToLower().Equals("HTS Lab Form".ToLower()));
        }

        [TestMethod]
        public void should_Get_Form_With_Questions()
        {
            var form = _formRepository.GetWithQuestions(_module.Id,_labForm.Id);
            Assert.IsNotNull(form);
            Assert.AreEqual(_module.Id,form.ModuleId);
            Assert.IsTrue(form.Questions.Count>0);
            Console.WriteLine($"{form}");
            foreach (var question in form.Questions)
            {
                Console.WriteLine($"   {question}");
            }
        }
    }
}