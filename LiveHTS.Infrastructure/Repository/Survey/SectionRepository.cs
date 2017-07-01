using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.DummyData;
using ILiveDatabase = LiveHTS.Core.Interfaces.Repository.ILiveDatabase;

namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class SectionRepository : BaseRepository<Section>, ISectionRepository
    {
        public SectionRepository(ILiveDatabase database) : base(database)
        {
            var forms = database.Read().ToList().SelectMany(x => x.Forms).ToList();

            foreach (var form in forms)
            {
                _entities.AddRange(form.Sections);
            }
        }
    }
}