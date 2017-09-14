using System;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Interfaces.Services.Config
{
    public interface IPracticeSetupService
    {
        Practice GetDefault();
        void Save(Practice practice);
    }
}