using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Infrastructure.Repository.Config
{
    public class ServerConfigRepository : BaseRepository<ServerConfig, string>, IServerConfigRepository
    {
        public ServerConfigRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
    }
}