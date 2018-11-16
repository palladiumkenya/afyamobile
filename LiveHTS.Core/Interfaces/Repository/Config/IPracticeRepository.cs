using System;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Interfaces.Repository.Config
{
    public interface IPracticeRepository:IRepository<Practice,Guid>
    {
        Practice GetDefault();
        void MakeDefault(Guid practiceId);
    }
}