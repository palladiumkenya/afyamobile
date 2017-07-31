using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Infrastructure.Repository.Config
{
    public class IdentifierTypeRepository : BaseRepository<IdentifierType, string>, IIdentifierTypeRepository
    {
        public IdentifierTypeRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
    }
}