using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Infrastructure.Repository.Config
{
    public class SubCountyRepository : BaseRepository<SubCounty,Guid>, ISubCountyRepository
    {
        public SubCountyRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
    }
}