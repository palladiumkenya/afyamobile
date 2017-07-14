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
    public class LookupCategoryRepositoryTests
    {
        private ILiveSetting _liveSetting;
        private SQLiteConnection _database = TestHelpers.GetDatabase();
        private ICategoryRepository _categoryRepository;

        [TestInitialize]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _categoryRepository =new CategoryRepository(_liveSetting);
        }

        [TestMethod]
        public void should_Get_Category_with_Items()
        {
            var categories = _categoryRepository.GetAllWithItems().ToList();
            Assert.IsTrue(categories.Count>0);
            foreach (var category in categories)
            {
                Console.WriteLine(category);

                Assert.IsTrue(category.Items.Count > 0);
                foreach (var categoryItem in category.Items)
                {
                    Console.WriteLine($" >. {categoryItem}");
                }
            }
        }
    }
}