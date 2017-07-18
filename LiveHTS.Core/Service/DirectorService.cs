using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Core.Service
{
    public class DirectorService:IDirectorService
    {
        private readonly Manifest _manifest;

        public Manifest Manifest
        {
            get { return _manifest; }
        }

        public void RefreshManifest()
        {
            throw new System.NotImplementedException();
        }

        public Question GetLiveQuestion()
        {
            throw new System.NotImplementedException();
        }
    }
}