using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Infrastructure.Repository.Subject
{
    public class ClientRepository:BaseRepository<Client,Guid>,IClientRepository
    {
        public ClientRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
            
        }

        public override Client Get(Guid id, bool voided = false)
        {
            var client =base.Get(id);
            if (null != client)
            {
             client.Person= _db.Table<Person>().FirstOrDefault(x => x.Id == client.PersonId);
            }
            return client;
        }
    }
}