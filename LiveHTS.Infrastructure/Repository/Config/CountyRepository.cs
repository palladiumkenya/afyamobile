using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Infrastructure.Repository.Config
{
    public class CountyRepository : BaseRepository<County,int>, ICountyRepository
    {
        public CountyRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
    }
}