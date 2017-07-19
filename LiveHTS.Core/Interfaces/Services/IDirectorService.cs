using System;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Interfaces.Services
{
    public interface IDirectorService
    {
        Manifest Manifest { get; }
        void Initialize();
        void UpdateManifest();
        Question GetLiveQuestion();
    }
}