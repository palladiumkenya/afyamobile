using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Infrastructure.Repository.Config
{
    public class PracticeTypeRepository : BaseRepository<PracticeType,string>, IPracticeTypeRepository
    {
        public PracticeTypeRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
    }
}