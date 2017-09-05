using System.Linq;
using LiveHTS.Core;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Infrastructure.Repository.Interview;
using LiveHTS.SharedKernel.Custom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;


namespace LiveHTS.Infrastructure.Tests.Repository.Survey
{
    [TestClass]
    public class ObsRepositoryTests
    {
        private ILiveSetting _liveSetting;
        private SQLiteConnection _database = TestHelpers.GetDatabase();
        private IObsRepository _obsRepository;
        private Obs _obs;
        private Encounter _encounter;

        [TestInitialize]
        public void SetUp()
        {
            _liveSetting = new LiveSetting(_database.DatabasePath);
            _obsRepository = new ObsRepository(_liveSetting);

            var encounters = TestDataHelpers.Encounters;
            _encounter = encounters.First();
            _obs = _encounter.Obses.First();
        }

        [TestMethod]
        public void should_Save_Or_Update_Obs_New()
        {
            var newObs = _obs;newObs.Id = LiveGuid.NewGuid();
            _obsRepository.SaveOrUpdate(newObs);

            _obsRepository = new ObsRepository(_liveSetting);
            var savedNewObs = _obsRepository.Get(newObs.Id);
            Assert.AreEqual(newObs.Id, savedNewObs.Id);
            Assert.IsNotNull(savedNewObs);
        }
        [TestMethod]
        public void should_Save_Or_Update_Obs_Exisitng()
        {
            var exisingObs = _obs;
            exisingObs.ValueText = "TTTT";
            _obsRepository.SaveOrUpdate(exisingObs);

            _obsRepository = new ObsRepository(_liveSetting);
            var savedNewObs = _obsRepository.Get(exisingObs.Id);
            Assert.IsNotNull(savedNewObs);
            Assert.AreEqual("TTTT", savedNewObs.ValueText);
        }
        [TestMethod]
        public void should_Find_Obs()
        {
            var obses = _obsRepository.Find(_encounter.ClientId, _obs.QuestionId);
            Assert.IsTrue(obses.Count>0);
        }
    }
}