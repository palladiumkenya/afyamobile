using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core;
using LiveHTS.Core.Interfaces.Repository.Meta;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Meta;
using LiveHTS.Infrastructure.Migrations;
using LiveHTS.Infrastructure.Repository.Meta;
using LiveHTS.Infrastructure.Repository.Survey;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;

namespace LiveHTS.Infrastructure.Tests.Repository.Meta
{
    [TestClass]
    public class RegionRepositoryTests
    {
        private IRegionRepository _regionRepository;
        private SQLiteConnection _connection;
        
        [TestInitialize]
        public void SetUp()
        {
            _connection = new SQLiteConnection("livehtsmeta.db");
            _regionRepository = new RegionRepository(new LiveSetting(_connection.DatabasePath));
        }
        
        [TestMethod]
        public void should_Get_Counties()
        {
            var regions = _regionRepository.GetCounties().ToList();
            Assert.IsTrue(regions.Any());
            foreach (var region in regions)
            {
                Console.WriteLine(region.County());
            }
        }

        [TestMethod]
        public void should_Get_Sub_Counties()
        {
            var regions = _regionRepository.GetSubCounties(47).ToList();
            Assert.IsTrue(regions.Any());
            foreach (var region in regions)
            {
                Console.WriteLine(region.SubCounty());
            }
        }

        [TestMethod]
        public void should_Get_Wards()
        {
            var regions = _regionRepository.GetWards(12).ToList();
            Assert.IsTrue(regions.Any());
            foreach (var region in regions)
            {
                Console.WriteLine(region.Ward());
            }
        }
        
        [TestMethod]
        public void should_Get_County()
        {
            var region = _regionRepository.GetCounty(1);
            Assert.IsNotNull(region);
            Console.WriteLine(region.County());
        }

        [TestMethod]
        public void should_Get_Sub_County()
        {
            var region = _regionRepository.GetSubCounty(1);
            Assert.IsNotNull(region);
            Console.WriteLine(region.SubCounty());
        }

        [TestMethod]
        public void should_Get_Ward()
        {
            var region = _regionRepository.GetWard(1);
            Assert.IsNotNull(region);
            Console.WriteLine(region.Ward());
        }
    }
}