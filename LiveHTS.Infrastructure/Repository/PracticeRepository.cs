using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model;

namespace LiveHTS.Infrastructure.Repository
{
    public class PracticeRepository:BaseRepository<Practice>,IPracticeRepository
    {
        public PracticeRepository(ILiveDatabase database) : base(database)
        {
        }
    }
}