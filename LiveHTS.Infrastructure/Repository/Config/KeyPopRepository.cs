using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Infrastructure.Repository.Config
{
    public class KeyPopRepository : BaseRepository<KeyPop, string>, IKeyPopRepository
    {
        public KeyPopRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
    }
}