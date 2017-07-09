using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Infrastructure.Repository;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;

namespace LiveHTS.Infrastructure.Tests.Repository
{
    [TestClass]
    public class BaseRepositoryTests
    {
        private SQLiteConnection _database = TestHelpers.GetTestDatabase();
        private TestCarRepository _carRepository;
        private TestModelRepository _modelRepository;

        [TestInitialize]
        public void SetUp()
        {
            _carRepository = new TestCarRepository(_database.DatabasePath);
            _modelRepository = new TestModelRepository(_database.DatabasePath);
        }

        [TestMethod]
        public void should_Get_By_Id()
        {
            var model = _modelRepository.Get(1);
            Assert.IsNotNull(model);
            Console.WriteLine(model);
        }

        [TestMethod]
        public void should_Get_All()
        {
            var cars = _carRepository.GetAll().ToList();
            Assert.IsTrue(cars.Count>0);
            foreach (var testCar in cars)
            {
                Console.WriteLine(testCar);
                Assert.IsTrue(testCar.TestModels.Count>0);
                
                foreach (var model in testCar.TestModels)
                {
                    Assert.AreEqual(testCar.Id, model.CarId);
                    Console.WriteLine($"  {model}");
                }
            }
        }

    }

    class TestCar : Entity<Guid>
    {
        public string Name { get; set; }
        [SQLite.Ignore]
        public virtual List<TestModel> TestModels { get; set; }=new List<TestModel>();

        public TestCar()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{Name} ({Id})";
        }
    }

    class TestModel : Entity<int>
    {
        [AutoIncrement]
        public override int Id { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
        [Indexed]
        public Guid CarId { get; set; }

        public override string ToString()
        {
            return $"{Id}> {Name}-{Year}";
        }
    }

    class TestCarRepository : BaseRepository<TestCar, Guid>
    {
        public TestCarRepository( string databasePath) : base( databasePath)
        {
        }

        public override IEnumerable<TestCar> GetAll()
        {
            var cars=base.GetAll().ToList();

            foreach (var testCar in cars)
            {
                try
                {
                    var models = _db.Table<TestModel>().Where(x => x.CarId == testCar.Id).ToList();
                    if (models.Count > 0)
                        testCar.TestModels = models;
                }
                catch
                {
                    // ignored
                }
            }

            return cars;
        }
    }

    class TestModelRepository : BaseRepository<TestModel, int>
    {
        public TestModelRepository( string databasePath) : base( databasePath)
        {
        }
    }
}