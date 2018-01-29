using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Infrastructure.Repository.Subject
{
    public class ProviderRepository :BaseRepository<Provider, Guid>, IProviderRepository
    {
        public ProviderRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public Provider GetDefaultProvider()
        {
            var provider= GetAll().FirstOrDefault();

            if(null!=provider)
                provider.Person= _db.Table<Person>().FirstOrDefault(x => x.Id == provider.PersonId);

            return provider;
        }

        public void Sync(List<Provider> providers)
        {
                    
        }
    }
}