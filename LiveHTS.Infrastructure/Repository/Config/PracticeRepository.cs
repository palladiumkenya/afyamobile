using System;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Infrastructure.Repository.Config
{
    public class PracticeRepository : BaseRepository<Practice,Guid>, IPracticeRepository
    {
        public PracticeRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public Practice GetDefault()
        {
            var practice = GetAll(x => x.IsDefault).FirstOrDefault();

            if (null == practice)
                practice = GetAll().FirstOrDefault();

            return practice;
        }

        public void MakeDefault(Guid practiceId)
        {

            var practice = GetAll(x => x.Id==practiceId).FirstOrDefault();
            if (null != practice)
            {
                _db.Execute($"UPDATE {nameof(Practice)} SET IsDefault=0 WHERE IsDefault=1");
                _db.Execute($"UPDATE {nameof(Practice)} SET IsDefault=1 WHERE Id=?", practiceId.ToString());
            }
        }
    }
}