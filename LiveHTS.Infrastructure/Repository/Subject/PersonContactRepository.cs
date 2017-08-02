using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Infrastructure.Repository.Subject
{
    public class PersonContactRepository : BaseRepository<PersonContact,Guid>, IPersonContactRepository
    {
        public PersonContactRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
    }
}