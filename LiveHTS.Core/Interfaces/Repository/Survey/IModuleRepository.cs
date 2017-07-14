using System;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Interfaces.Repository.Survey
{
    public interface IModuleRepository:IRepository<Module,Guid>
    {
        Module GetDefaultModule();
    }
}