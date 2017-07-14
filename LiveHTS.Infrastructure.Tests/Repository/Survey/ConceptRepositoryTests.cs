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
    public class ConceptRepositoryTests
    {
        private ILiveSetting _liveSetting;
        private SQLiteConnection _database = TestHelpers.GetDatabase();
        private IConceptRepository _conceptRepository;

        [TestInitialize]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _conceptRepository =new ConceptRepository(_liveSetting,new CategoryRepository(_liveSetting));
        }

        [TestMethod]
        public void should_Get_Concept_with_Items()
        {


            var concepts = _conceptRepository.GetWithLookups().ToList();
            Assert.IsTrue(concepts.Count>0);
            foreach (var concept in concepts)
            {
                Assert.IsNotNull(concept);
                Console.Write(concept);

                if (concept.CategoryId.HasValue)
                {
                    Assert.IsNotNull(concept.Category);
                    Assert.AreEqual(concept.CategoryId,concept.Category.Id);
                    Console.Write($" ({concept.Category})");
                    Console.WriteLine();
                    foreach (var categoryItem in concept.Category.Items)
                    {
                        Console.WriteLine($" >. {categoryItem}");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}