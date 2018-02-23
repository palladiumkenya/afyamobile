using System;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.Repository.Survey;
using LiveHTS.SharedKernel.Custom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SQLite;
using Assert = NUnit.Framework.Assert;


namespace LiveHTS.Core.Tests.Model.Survey
{
    [TestFixture]
    public class QuestionValidationTests
    {       
        private ILiveSetting _liveSetting;
        private bool setNunit = TestHelpers.UseNunit = true;
        private SQLiteConnection _database = TestHelpers.GetDatabase();

        private Form _form;
        private Guid _formId;
        private Encounter _encounter;
        private Response _responseRequired, _responseMinOnly, _responseMaxOnly, _responseMinMax;
        private QuestionValidation _questionValidation;

        [SetUp]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            var formRepository = new FormRepository(_liveSetting,
                new QuestionRepository(_liveSetting,
                    new ConceptRepository(_liveSetting, new CategoryRepository(_liveSetting))));
            _formId = TestDataHelpers._formId;
            _form = formRepository.GetWithQuestions(_formId,true);
            _encounter = TestHelpers.CreateTestEncountersWithObs(_form);
            
            _questionValidation=new QuestionValidation();

            //[ValidatorId] Required | Range 
            //[ValidatorTypeId] None | Numeric | Count
            //Revision=0
        }

        [Test]
        public void should_Evaluate_Required()
        {
            var q1 = _form.Questions.OrderBy(x => x.Rank).First();
            var obs = _encounter.Obses.First(x => x.QuestionId == q1.Id);
            _questionValidation = q1.Validations.First(x => x.ValidatorId.ToLower() == "Required".ToLower() && x.Revision == 0);

            obs.ValueCoded = Guid.NewGuid();
            _responseRequired = new Response(obs.EncounterId, _encounter.ClientId, q1, obs);

            var isvalid= _questionValidation.Evaluate(_responseRequired);
            Assert.IsTrue(isvalid);
            Console.WriteLine($"{q1}={obs.ValueCoded}  | {_questionValidation}");

            _responseRequired.Obs.ValueCoded = null;
            Assert.Throws<ArgumentException>(() => _questionValidation.Evaluate(_responseRequired));
        }

        [Test]
        public void should_Evaluate_Numeric_Min_Only()
        {
            //  >= 1

            var q3 = _form.Questions.First(x => x.Rank == 3);
            var obs = _encounter.Obses.First(x => x.QuestionId == q3.Id);
            _questionValidation = q3.Validations.First(x => x.ValidatorId.ToLower() == "Range".ToLower() && x.ValidatorTypeId.ToLower() == "Numeric".ToLower() && x.Revision == 0);

            _questionValidation.MinLimit = "1";
            _questionValidation.MaxLimit = string.Empty;
            obs.ValueNumeric = 1;
            _responseMinOnly = new Response(obs.EncounterId, _encounter.ClientId,q3, obs);
            
            var isvalid = _questionValidation.Evaluate(_responseMinOnly);
            Assert.IsTrue(isvalid);
            Console.WriteLine($"{q3}={obs.ValueNumeric}  | {_questionValidation}");

            _responseMinOnly.Obs.ValueNumeric = null;
            Assert.Throws<ArgumentException>(() => _questionValidation.Evaluate(_responseMinOnly));

            _responseMinOnly.Obs.ValueNumeric = 0;
            Assert.Throws<ArgumentException>(() => _questionValidation.Evaluate(_responseMinOnly));
        }

        [Test]
        public void should_Evaluate_Numeric_Max_Only()
        {
            //  <= 4

            var q3 = _form.Questions.First(x => x.Rank == 3);
            var obs = _encounter.Obses.First(x => x.QuestionId == q3.Id);
            _questionValidation = q3.Validations.First(x => x.ValidatorId.ToLower() == "Range".ToLower() && x.ValidatorTypeId.ToLower() == "Numeric".ToLower() && x.Revision == 0);

            _questionValidation.MinLimit = string.Empty;
            _questionValidation.MaxLimit = "4";
            obs.ValueNumeric = 4;
            _responseMaxOnly = new Response(obs.EncounterId, _encounter.ClientId,q3, obs);

            var isvalid = _questionValidation.Evaluate(_responseMaxOnly);
            Assert.IsTrue(isvalid);
            Console.WriteLine($"{q3}={obs.ValueNumeric}  | {_questionValidation}");

            _responseMaxOnly.Obs.ValueNumeric = null;
            Assert.Throws<ArgumentException>(() => _questionValidation.Evaluate(_responseMaxOnly));

            _responseMaxOnly.Obs.ValueNumeric =5;
            Assert.Throws<ArgumentException>(() => _questionValidation.Evaluate(_responseMaxOnly));
        }

        [Test]
        public void should_Evaluate_Numeric_Range()
        {
            //  1 to 4

            var q3 = _form.Questions.First(x => x.Rank == 3);
            var obs = _encounter.Obses.First(x => x.QuestionId == q3.Id);
            _questionValidation = q3.Validations.First(x => x.ValidatorId.ToLower() == "Range".ToLower() && x.ValidatorTypeId.ToLower() == "Numeric".ToLower() && x.Revision == 0);

            _questionValidation.MinLimit = "1";
            _questionValidation.MaxLimit = "4";
            obs.ValueNumeric = 4;
            _responseMaxOnly = new Response(obs.EncounterId, _encounter.ClientId, q3, obs);

            var isvalid = _questionValidation.Evaluate(_responseMaxOnly);
            Assert.IsTrue(isvalid);
            Console.WriteLine($"{q3}={obs.ValueNumeric}  | {_questionValidation}");

            _responseMaxOnly.Obs.ValueNumeric = null;
            Assert.Throws<ArgumentException>(() => _questionValidation.Evaluate(_responseMaxOnly));

            _responseMaxOnly.Obs.ValueNumeric = 5;
            Assert.Throws<ArgumentException>(() => _questionValidation.Evaluate(_responseMaxOnly));
        }

        [Test]
        public void should_Evaluate_Count_Min_Only()
        {
            //  >= 2

            var q4 = _form.Questions.First(x => x.Rank == 4);
            var obs = _encounter.Obses.First(x => x.QuestionId == q4.Id);
            _questionValidation = q4.Validations.First(x => x.ValidatorId.ToLower() == "Range".ToLower() && x.ValidatorTypeId.ToLower() == "Count".ToLower() && x.Revision == 0);

            _questionValidation.MinLimit = "2";
            _questionValidation.MaxLimit = string.Empty;
            obs.ValueMultiCoded = $"{LiveGuid.NewGuid()},{LiveGuid.NewGuid()}";
            _responseMinOnly = new Response(obs.EncounterId, _encounter.ClientId,q4, obs);

            var isvalid = _questionValidation.Evaluate(_responseMinOnly);
            Assert.IsTrue(isvalid);
            Console.WriteLine($"{q4}={obs.ValueMultiCoded}  | {_questionValidation}");

            _responseMinOnly.Obs.ValueMultiCoded = String.Empty;
            Assert.Throws<ArgumentException>(() => _questionValidation.Evaluate(_responseMinOnly));

            _responseMinOnly.Obs.ValueMultiCoded = null;
            Assert.Throws<ArgumentException>(() => _questionValidation.Evaluate(_responseMinOnly));

            _responseMinOnly.Obs.ValueMultiCoded = $"{LiveGuid.NewGuid()}";
            Assert.Throws<ArgumentException>(() => _questionValidation.Evaluate(_responseMinOnly));
        }


        [Test]
        public void should_Evaluate_Count_Max_Only()
        {
            //  <= 4

            var q4 = _form.Questions.First(x => x.Rank == 4);
            var obs = _encounter.Obses.First(x => x.QuestionId == q4.Id);
            _questionValidation = q4.Validations.First(x => x.ValidatorId.ToLower() == "Range".ToLower() && x.ValidatorTypeId.ToLower() == "Count".ToLower() && x.Revision == 0);
            
            _questionValidation.MinLimit = string.Empty;
            _questionValidation.MaxLimit = "4";
            obs.ValueMultiCoded = $"{LiveGuid.NewGuid()},{LiveGuid.NewGuid()}";
            _responseMaxOnly = new Response(obs.EncounterId, _encounter.ClientId, q4, obs);

            var isvalid = _questionValidation.Evaluate(_responseMaxOnly);
            Assert.IsTrue(isvalid);
            Console.WriteLine($"{q4}={obs.ValueMultiCoded}  | {_questionValidation}");

            _responseMaxOnly.Obs.ValueMultiCoded = String.Empty;
            Assert.Throws<ArgumentException>(() => _questionValidation.Evaluate(_responseMaxOnly));

            _responseMaxOnly.Obs.ValueMultiCoded = null;
            Assert.Throws<ArgumentException>(() => _questionValidation.Evaluate(_responseMaxOnly));

            _responseMaxOnly.Obs.ValueMultiCoded = $"{LiveGuid.NewGuid()},{LiveGuid.NewGuid()},{LiveGuid.NewGuid()},{LiveGuid.NewGuid()},{LiveGuid.NewGuid()}";
            Assert.Throws<ArgumentException>(() => _questionValidation.Evaluate(_responseMaxOnly));
        }

        [Test]
        public void should_Evaluate_Count_Range()
        {
            //  1 to 4

            var q4 = _form.Questions.First(x => x.Rank == 4);
            var obs = _encounter.Obses.First(x => x.QuestionId == q4.Id);
            _questionValidation = q4.Validations.First(x => x.ValidatorId.ToLower() == "Range".ToLower() && x.ValidatorTypeId.ToLower() == "Count".ToLower() && x.Revision == 0);

            _questionValidation.MinLimit = "1";
            _questionValidation.MaxLimit = "4";
            obs.ValueMultiCoded = $"{LiveGuid.NewGuid()},{LiveGuid.NewGuid()}";
            _responseMinMax = new Response(obs.EncounterId, _encounter.ClientId,q4, obs);

            var isvalid = _questionValidation.Evaluate(_responseMinMax);
            Assert.IsTrue(isvalid);
            Console.WriteLine($"{q4}={obs.ValueMultiCoded}  | {_questionValidation}");

            _responseMinMax.Obs.ValueMultiCoded = String.Empty;
            Assert.Throws<ArgumentException>(() => _questionValidation.Evaluate(_responseMinMax));

            _responseMinMax.Obs.ValueMultiCoded = null;
            Assert.Throws<ArgumentException>(() => _questionValidation.Evaluate(_responseMinMax));

            _responseMinMax.Obs.ValueMultiCoded = $"{LiveGuid.NewGuid()},{LiveGuid.NewGuid()},{LiveGuid.NewGuid()},{LiveGuid.NewGuid()},{LiveGuid.NewGuid()}";
        }
    }
}