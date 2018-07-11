using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Interfaces.Services.Config
{
    public interface IPracticeSetupService
    {
        Practice GetDefault();
        List<Practice> GetAll();
        void Save(Practice practice);
    }
}