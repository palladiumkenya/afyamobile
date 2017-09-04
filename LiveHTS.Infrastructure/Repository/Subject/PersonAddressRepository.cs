using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Infrastructure.Repository.Subject
{
    public class PersonAddressRepository : BaseRepository<PersonAddress,Guid>, IPersonAddressRepository
    {
        public PersonAddressRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
    }
}