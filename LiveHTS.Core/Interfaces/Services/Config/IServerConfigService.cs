using System;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Interfaces.Services.Config
{
    public interface IServerConfigService
    {
        void SaveCentral(ServerConfig config);
        void SaveLocal(ServerConfig config);
    }
}