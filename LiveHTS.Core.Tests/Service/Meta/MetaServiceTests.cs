using System;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Meta;
using LiveHTS.Core.Interfaces.Services.Meta;
using LiveHTS.Core.Service.Meta;
using LiveHTS.Infrastructure.Repository.Meta;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;

namespace LiveHTS.Core.Tests.Service.Meta
{
    [TestClass]
    public class MetaServiceTests
    {
        private ILiveSetting _liveSetting;
        private IRegionRepository _regionRepository;
        private IMetaService _metaService;
        private SQLiteConnection _connection;

        [TestInitialize]
        public void SetUp()
        {
            _connection = new SQLiteConnection("livehtsmeta.db");
            _regionRepository = new RegionRepository(new LiveSetting(_connection.DatabasePath));
            _metaService = new MetaService(_regionRepository);
        }


        [TestMethod]
        public void should_Load_Counties()
        {
            var regions = _metaService.GetCounties().ToList();
            Assert.IsTrue(regions.Any(x=>x.CountyId>0));
            Assert.IsTrue(regions.Any(x=>x.CountyName== "Select County"));
            foreach (var region in regions)
                Console.WriteLine(region.County());
        }

        [TestMethod]
        public void should_Load_SubCounties()
        {
            var regions = _metaService.GetSubCounties(47).ToList();
            Assert.IsTrue(regions.Any(x => x.SubCountyId > 0));
            Assert.IsTrue(regions.Any(x => x.SubCountyName == "Select SubCounty"));
            foreach (var region in regions)
                Console.WriteLine(region.SubCounty());
        }

        [TestMethod]
        public void should_Load_Wards()
        {
            var regions = _metaService.GetWards(11).ToList();
            Assert.IsTrue(regions.Any(x => x.WardId > 0));
            Assert.IsTrue(regions.Any(x => x.WardName == "Select Ward"));
            foreach (var region in regions)
                Console.WriteLine(region.Ward());
        }
    }
}