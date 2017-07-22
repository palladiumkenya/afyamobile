using System;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Interfaces.Repository
{
    public interface IDeviceRepository:IRepository<Device,Guid>
    {
        Device GetBySerial(string serial);
    }
}