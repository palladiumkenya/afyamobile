using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Infrastructure.Repository.Config
{
    public class PracticeRepository : BaseRepository<Practice,Guid>, IPracticeRepository
    {
        public PracticeRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
    }
}