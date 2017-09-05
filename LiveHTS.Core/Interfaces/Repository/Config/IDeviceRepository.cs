using System;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Interfaces.Repository.Config
{
    public interface IDeviceRepository:IRepository<Device,Guid>
    {
        Device GetDefault(string serial="");
    }
}