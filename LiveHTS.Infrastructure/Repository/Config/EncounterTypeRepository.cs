using System;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Infrastructure.Repository.Config
{
    public class EncounterTypeRepository : BaseRepository<EncounterType, Guid>, IEncounterTypeRepository
    {
        public EncounterTypeRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public EncounterType GetDefault()
        {
            return GetAll(x => x.Name.ToLower()
                    .Contains("Initial".ToLower()))
                .FirstOrDefault();
        }
    }
}