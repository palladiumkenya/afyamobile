using System;
using System.Linq;
using LiveHTS.Core.Engine;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Engine;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.Repository.Survey;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SQLite;

namespace LiveHTS.Core.Tests.Engine
{
    [TestFixture]
    public class ValidationEngineTests
    {
        private ILiveSetting _liveSetting;
        private bool setNunit = TestHelpers.UseNunit = true;
        private SQLiteConnection _database = TestHelpers.GetDatabase();

        private Form _form;
        private Guid _formId;
        private Encounter _encounter;
        private Response _responseRequired;
        private IValidationEngine _validationEngine;

        [SetUp]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            var formRepository = new FormRepository(_liveSetting,
                new QuestionRepository(_liveSetting,
                    new ConceptRepository(_liveSetting, new CategoryRepository(_liveSetting))));
            _formId = TestDataHelpers._formId;
            _form = formRepository.GetWithQuestions(_formId, true);
            _encounter = TestHelpers.CreateTestEncountersWithObs(_form);

            
            _validationEngine=new ValidationEngine();
            //[ValidatorId] Required | Range 
            //[ValidatorTypeId] None | Numeric | Count
            //Revision=0
        }

        [Test]
        public void should_Validate()
        {
            var q1 = _form.Questions.OrderBy(x => x.Rank).First();
            var obs = _encounter.Obses.First(x => x.QuestionId == q1.Id);

            obs.ValueCoded = Guid.NewGuid();
            _responseRequired = new Response(obs.EncounterId,_encounter.ClientId, q1, obs);

            var isvalid = _validationEngine.Validate(_responseRequired);
            NUnit.Framework.Assert.IsTrue(isvalid);
            Console.WriteLine($"{q1}={obs.ValueCoded}  | {isvalid}");

            _responseRequired.Obs.ValueCoded = null;
            NUnit.Framework.Assert.Throws<ArgumentException>(() => _validationEngine.Validate(_responseRequired));
        }

        [Test]
        public void should_Validate_With_Multiple()
        {
            //Q3.No of Kits,  Required & between 1-5

            var q3 = _form.Questions.First(x => x.Rank == 3);
            var obs = _encounter.Obses.First(x => x.QuestionId == q3.Id);

            obs.ValueNumeric = 2;
            _responseRequired = new Response(obs.EncounterId,_encounter.ClientId, q3, obs);

            var isvalid = _validationEngine.Validate(_responseRequired);
            NUnit.Framework.Assert.IsTrue(isvalid);
            Console.WriteLine($"{q3}={obs.ValueNumeric}  | {isvalid}");

            _responseRequired.Obs.ValueNumeric = null;
            NUnit.Framework.Assert.Throws<ArgumentException>(() => _validationEngine.Validate(_responseRequired));

            _responseRequired.Obs.ValueNumeric = 6;
            NUnit.Framework.Assert.Throws<ArgumentException>(() => _validationEngine.Validate(_responseRequired));
        }
    }
}