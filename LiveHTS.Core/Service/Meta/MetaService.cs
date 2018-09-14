using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Meta;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Interfaces.Services.Meta;
using LiveHTS.Core.Model.Meta;

namespace LiveHTS.Core.Service.Meta
{
    public class MetaService : IMetaService
    {
        private readonly IRegionRepository _regionRepository;

        public MetaService(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public IEnumerable<RegionItem> GetCounties(bool addSelectOption=true)
        {
            var regions = new List<RegionItem>();

            if (addSelectOption)
            {
                var initialSelected = RegionItem.CreateCountyInitial();
                regions.Add(initialSelected);
            }

            var counties = _regionRepository.GetCounties().ToList();
            if (counties.Any())
            {
                    regions.AddRange(counties);
            }
            return regions;
        }

        public IEnumerable<RegionItem> GetSubCounties(int countyId, bool addSelectOption = true)
        {
            var regions = new List<RegionItem>();

            if (addSelectOption)
            {
                var initialSelected = RegionItem.CreateSubCountyInitial();
                regions.Add(initialSelected);
            }

            var subCounties = _regionRepository.GetSubCounties(countyId).ToList();
            if (subCounties.Any())
            {
                regions.AddRange(subCounties);
            }
            return regions;
        }

        public IEnumerable<RegionItem> GetWards(int subCountyId, bool addSelectOption = true)
        {
            var regions = new List<RegionItem>();

            if (addSelectOption)
            {
                var initialSelected = RegionItem.CreateWardInitial();
                regions.Add(initialSelected);
            }

            var wards = _regionRepository.GetWards(subCountyId).ToList();
            if (wards.Any())
            {
                regions.AddRange(wards);
            }
            return regions;
        }
    }
}