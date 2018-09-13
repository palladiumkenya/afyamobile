using LiveHTS.Core.Interfaces;

namespace LiveHTS.Core
{
    public class LiveSetting : ILiveSetting
    {

        public string DatasePath { get; set; }
        public string MetaDatabasePath { get; set; }

        public LiveSetting(string datasePath)
        {
            DatasePath = datasePath;
            MetaDatabasePath = datasePath.Replace("livehts.db", "livehtsmeta.db");
        }
    }
}