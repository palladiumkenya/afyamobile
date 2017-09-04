using System;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Interfaces.Repository.Lookup;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Service;
using LiveHTS.Core.Service.Config;
using LiveHTS.Infrastructure.Repository.Config;
using LiveHTS.Infrastructure.Repository.Subject;
using LiveHTS.Infrastructure.Repository.Survey;
using NUnit.Framework;
using SQLite;

namespace LiveHTS.Core.Tests.Service.Config
{
    [TestFixture]
    public class LookupServiceTests
    {
        private bool setNunit = TestHelpers.UseNunit = true;

        private ILiveSetting _liveSetting;

        private ICountyRepository _countyRepository;
        private ISubCountyRepository _subCountyRepository;
        private IPracticeRepository _practiceRepository;
        private IPracticeTypeRepository _practiceTypeRepository;
        private IMaritalStatusRepository _maritalStatusRepository;
        private IKeyPopRepository _keyPopRepository;
        private IIdentifierTypeRepository _identifierTypeRepository;
        private IEncounterTypeRepository _encounterTypeRepository;
        private ICategoryRepository _categoryRepository;
        private ILookupService _lookupService;
        private SQLiteConnection _database = TestHelpers.GetDatabase();
        private IRelationshipTypeRepository _relationshipTypeRepository;


        [SetUp]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);

            _countyRepository =new CountyRepository(_liveSetting);
            _subCountyRepository=new SubCountyRepository(_liveSetting);
            _practiceTypeRepository=new PracticeTypeRepository(_liveSetting);
            _practiceRepository =new PracticeRepository(_liveSetting);
            _maritalStatusRepository=new MaritalStatusRepository(_liveSetting);
            _keyPopRepository=new KeyPopRepository(_liveSetting);
            _identifierTypeRepository=new IdentifierTypeRepository(_liveSetting);
            _relationshipTypeRepository=new RelationshipTypeRepository(_liveSetting);
            _categoryRepository=new CategoryRepository(_liveSetting);



        _lookupService = new LookupService(_countyRepository,_subCountyRepository,_practiceRepository,_practiceTypeRepository,_maritalStatusRepository,_keyPopRepository,_identifierTypeRepository,_relationshipTypeRepository, _encounterTypeRepository,_categoryRepository);

            
        }

        [Test]
        public void should_Load_GetCounties()
        {
            var counties = _lookupService.GetCounties().ToList();
            Assert.IsTrue(counties.Any());
        }
        [Test]
        public void should_Load_GetSubCounties()
        {
            var subCounties = _lookupService.GetSubCounties(new []{47}).ToList();
            Assert.IsTrue(subCounties.Any());
        }
        [Test]
        public void should_Load_GetPracticeTypes()
        {
            var practiceTypes = _lookupService.GetPracticeTypes().ToList();
            Assert.IsTrue(practiceTypes.Any());
        }
        [Test]
        public void should_Load_GetPractices()
        {
            var practices = _lookupService.GetPractices(new []{"Facility"}).ToList();
            Assert.IsTrue(practices.Any());
        }

        [Test]
        public void should_Load_DefaultPractice()
        {
            var practice = _lookupService.GetDefault();
            Assert.IsNotNull(practice);
        }

        [Test]
        public void should_Load_GetRelationshipTypes()
        {
            var counties = _lookupService.GetRelationshipTypes().ToList();
            Assert.IsTrue(counties.Any());
        }
        [Test]
        public void should_Load_CategoryItems()
        {
            var items = _lookupService.GetCategoryItems("YesNo").ToList();
            Assert.IsTrue(items.Any());
            foreach (var categoryItem in items)
            {
                Console.WriteLine(categoryItem);
            }
        }

        [Test]
        public void should_Load_CategoryItems_Withheader()
        {
            var items = _lookupService.GetCategoryItems("YesNo",true,"Select Yes or No").ToList();
            Assert.IsTrue(items.Any(x=>x.Display== "Select Yes or No"));
            foreach (var categoryItem in items)
            {
                Console.WriteLine($"{categoryItem.Rank},{categoryItem}");
            }
            Console.WriteLine(new string('-',20));

            items = _lookupService.GetCategoryItems("YesNo", true).ToList();
            Assert.IsTrue(items.Any(x => x.Display.Contains("[")));
            foreach (var categoryItem in items)
            {
                Console.WriteLine($"{categoryItem.Rank},{categoryItem}");
            }
        }
    }
}