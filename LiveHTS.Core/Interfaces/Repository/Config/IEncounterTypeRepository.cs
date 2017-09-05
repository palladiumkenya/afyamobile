using System;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Interfaces.Repository.Config
{
    public interface IEncounterTypeRepository : IRepository<EncounterType, Guid>
    {
        EncounterType GetDefault();
    }
}