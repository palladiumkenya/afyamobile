using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core;
using LiveHTS.Core.Interfaces;
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
        private ILiveSetting _liveSetting;
        private SQLiteConnection _database;
        private TestCarRepository _carRepository;
        private TestModelRepository _modelRepository;

        [TestInitialize]
        public void SetUp()
        {
            _database = TestHelpers.GetTestDatabase();
            _liveSetting =new LiveSetting(_database.DatabasePath);
            _carRepository = new TestCarRepository(_liveSetting);
            _modelRepository = new TestModelRepository(_liveSetting);
        }
        [TestMethod]
        public void should_Get_By_Id()
        {
            var modelId=_modelRepository.GetAll().First().Id;

            var model = _modelRepository.Get(modelId);
            Assert.IsNotNull(model);
            Assert.IsFalse(model.Voided);
            Console.WriteLine(model);
        }
        [TestMethod]
        public void should_Get_By_Id_Voided()
        {
            var models = _modelRepository.GetAll(true);

            var model = models.First();
            Assert.IsNotNull(model);
            Assert.IsTrue(model.Voided);
            Console.WriteLine(model);
        }
        [TestMethod]
        public void should_Get_All()
        {
            var cars = _carRepository.GetAll().ToList();
            Assert.IsTrue(cars.Count>0);
            foreach (var testCar in cars)
            {
                Assert.IsFalse(testCar.Voided);
                Console.WriteLine(testCar);
                Assert.IsTrue(testCar.TestModels.Count>0);
                
                foreach (var model in testCar.TestModels)
                {
                    Assert.IsFalse(model.Voided);
                    Assert.AreEqual(testCar.Id, model.CarId);
                    Console.WriteLine($"  {model}");
                }
            }
        }
        [TestMethod]
        public void should_Get_All_Voided()
        {
            var models = _modelRepository.GetAll(true).ToList();
            Assert.IsTrue(models.Count>0);
            var model = models.First();
            Assert.IsNotNull(model);
            Assert.IsTrue(model.Voided);
            Console.WriteLine(model);            
        }
        [TestMethod]
        public void should_Save_New()
        {
            var car=new TestCar("TestCar");

            _carRepository.Save(car);

            var savedCar = _carRepository.Get(car.Id);
            Assert.IsNotNull(savedCar);
            Assert.IsFalse(savedCar.Voided);
            Console.WriteLine(savedCar);
        }

        [TestMethod]
        public void should_Update_Exisiting()
        {
            var car = _carRepository.GetAll().First();
            car.Name = "UpdatedCar";

            _carRepository.Update(car);

            var savedCar = _carRepository.Get(car.Id);
            Assert.IsNotNull(savedCar);
            Assert.AreEqual("UpdatedCar", savedCar.Name);
            Assert.IsFalse(savedCar.Voided);
            Console.WriteLine(savedCar);
        }

        [TestMethod]
        public void should_Delete_Exisiting()
        {
            var car = _carRepository.GetAll().First();

            _carRepository.Delete(car.Id);

            var savedCar = _carRepository.Get(car.Id);
            Assert.IsNull(savedCar);
        }

        [TestMethod]
        public void should_Void_Exisiting()
        {
            var car = _carRepository.GetAll().First();
            _carRepository.Void(car.Id);

            var savedCar = _carRepository.Get(car.Id);
            Assert.IsNull(savedCar);
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

        public TestCar(string name):this()
        {
            Name = name;
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
        public TestCarRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }


        public override IEnumerable<TestCar> GetAll(bool voided = false)
        {
            var cars = base.GetAll().ToList();

            foreach (var testCar in cars)
            {
                try
                {
                    var models = _db.Table<TestModel>()
                        .Where(x => x.CarId == testCar.Id &&
                                    x.Voided == voided)
                        .ToList();
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
        public TestModelRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
    }
}