using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Infrastructure.Repository.Subject
{
    public class PersonRepository:BaseRepository<Person,Guid>, IPersonRepository
    {
        public PersonRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
    }
}