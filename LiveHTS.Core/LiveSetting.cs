using LiveHTS.Core.Interfaces;

namespace LiveHTS.Core
{
    public class LiveSetting : ILiveSetting
    {

        public string DatasePath { get; set; }

        public LiveSetting(string datasePath)
        {
            DatasePath = datasePath;
        }
    }
}