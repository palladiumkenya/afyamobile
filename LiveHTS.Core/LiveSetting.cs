using LiveHTS.Core.Interfaces;
using SQLite;

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