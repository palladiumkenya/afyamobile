using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Service.Config
{
    public class CohortService: ICohortService
    {
        private readonly ICohortRepository _cohortRepository;

        public CohortService(ICohortRepository cohortRepository)
        {
            _cohortRepository = cohortRepository;
        }

        public IEnumerable<Cohort> GetAllCohorts(string search)
        {
            return _cohortRepository
                .GetAll()
                .Where(x=>x.Display.ToLower().Contains(search.ToLower())|| x.Name.ToLower().Contains(search.ToLower()))
                .ToList();
        }

        public IEnumerable<Cohort> GetAllCohorts()
        {
            return _cohortRepository.GetAll().ToList();
        }
    }
}