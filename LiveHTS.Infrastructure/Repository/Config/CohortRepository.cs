using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Infrastructure.Repository.Config
{
    public class CohortRepository : BaseRepository<Cohort, Guid>, ICohortRepository
    {
        public CohortRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
    }
}