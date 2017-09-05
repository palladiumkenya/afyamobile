using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Infrastructure.Repository.Config
{
    public class MaritalStatusRepository : BaseRepository<MaritalStatus,string>, IMaritalStatusRepository
    {
        public MaritalStatusRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
    }
}