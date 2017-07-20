using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiveHTS.Core.Tests.Model.Interview
{
    [TestClass]
    public class ResponseTests
    {
        private Response _response;

        [TestInitialize]
        public void SetUp()
        {
            
            
            var concept = Builder<Concept>.CreateNew().Build();
            var question = Builder<Question>.CreateNew()
                .With(x => x.Concept = concept)
                .With(x=>x.ConceptId=concept.Id)
                .Build();
            var obs = Builder<Obs>.CreateNew()
                .With(x => x.QuestionId = question.Id)
                .Build();


            _response = Builder<Response>.CreateNew()
                .With(x => x.Question = question)
                .With(x => x.QuestionId = question.Id)
                .With(x => x.Obs = obs)
                .With(x => x.ObsId = obs.Id)
                .Build();
            
        }

        
        [TestMethod]
        public void should_Get_Obs_Value_Single()
        {
            //  Single | Numeric | Multi | DateTime | Text

            _response.Question.Concept.ConceptTypeId = "Single";
            _response.Obs.ValueCoded = LiveGuid.NewGuid();
            var obsVal = _response.GetValue();
            Assert.IsNotNull(obsVal);
            Assert.IsInstanceOfType(obsVal.Value, typeof(Guid?));

            _response.Obs.ValueCoded = null;
            var obsValEmpty = _response.GetValue();
            Assert.IsInstanceOfType(obsValEmpty.Value, typeof(Guid?));

            Console.WriteLine(obsVal);
            Console.WriteLine(new string('-', 30));
            Console.WriteLine(obsValEmpty);
        }
        [TestMethod]
        public void should_Get_Obs_Value_Numeric()
        {
            //  Single | Numeric | Multi | DateTime | Text

            _response.Question.Concept.ConceptTypeId = "Numeric";
            _response.Obs.ValueNumeric = 4.7m;
            var obsVal = _response.GetValue();
            Assert.IsNotNull(obsVal);
            Assert.IsInstanceOfType(obsVal.Value, typeof(decimal?));

            _response.Obs.ValueNumeric = null;
            var obsValEmpty = _response.GetValue();
            Assert.IsInstanceOfType(obsValEmpty.Value, typeof(decimal?));

            Console.WriteLine(obsVal);
            Console.WriteLine(new string('-', 30));
            Console.WriteLine(obsValEmpty);
        }
        [TestMethod]
        public void should_Get_Obs_Value_Multi()
        {
            //  Single | Numeric | Multi | DateTime | Text
            var mulitResponse= new List<Guid> { LiveGuid.NewGuid(), LiveGuid.NewGuid() };

            _response.Question.Concept.ConceptTypeId = "Multi";
            _response.Obs.ValueMultiCoded = string.Join(",", mulitResponse);
            var obsVal = _response.GetValue();
            Assert.IsNotNull(obsVal);
            Assert.IsInstanceOfType(obsVal.Value, typeof(string));

            _response.Obs.ValueMultiCoded = null;
            var obsValEmpty = _response.GetValue();
            Assert.IsInstanceOfType(obsValEmpty.Value, typeof(string));

            Console.WriteLine(obsVal);
            Console.WriteLine(new string('-', 30));
            Console.WriteLine(obsValEmpty);
        }
        [TestMethod]
        public void should_Get_Obs_Value_DateTime()
        {
            //  Single | Numeric | Multi | DateTime | Text

            _response.Question.Concept.ConceptTypeId = "DateTime";
            var obsVal = _response.GetValue();
            Assert.IsNotNull(obsVal);
            Assert.IsInstanceOfType(obsVal.Value, typeof(DateTime?));

            _response.Obs.ValueDateTime = null;
            var obsValEmpty = _response.GetValue();
            Assert.IsInstanceOfType(obsValEmpty.Value, typeof(DateTime?));

            Console.WriteLine(obsVal);
            Console.WriteLine(new string('-', 30));
            Console.WriteLine(obsValEmpty);
        }

        [TestMethod]
        public void should_Get_Obs_Value_Text()
        {
            //  Single | Numeric | Multi | DateTime | Text

            _response.Question.Concept.ConceptTypeId = "Text";
            var obsVal = _response.GetValue();
            Assert.IsNotNull(obsVal);
            Assert.IsInstanceOfType(obsVal.Value,typeof(string));

            _response.Obs.ValueText = null;
            var obsValEmpty = _response.GetValue();
            Assert.IsInstanceOfType(obsValEmpty.Value, typeof(string));

            Console.WriteLine(obsVal);
            Console.WriteLine(new string('-',30));
            Console.WriteLine(obsValEmpty);
        }

        [TestMethod]
        public void should_Convert()
        {
            var guid=new Guid(string.Empty);

            Assert.IsNotNull(guid);

        }

        
    }
}