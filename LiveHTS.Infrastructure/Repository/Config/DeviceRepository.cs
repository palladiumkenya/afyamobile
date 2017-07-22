using System;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Infrastructure.Repository.Config
{
    public class DeviceRepository:BaseRepository<Device,Guid>,IDeviceRepository
    {
        public DeviceRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public Device GetDefault(string serial = "")
        {
            if (string.IsNullOrWhiteSpace(serial))

                return GetAll().FirstOrDefault();

            return GetAll(x => x.Serial.ToLower() == serial.ToLower())
                .FirstOrDefault();
        }
    }
}