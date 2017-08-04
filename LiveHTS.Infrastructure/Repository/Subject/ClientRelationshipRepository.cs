using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Infrastructure.Repository.Subject
{
    public class ClientRelationshipRepository : BaseRepository<ClientRelationship,Guid>, IClientRelationshipRepository
    {
        public ClientRelationshipRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }

        public IEnumerable<ClientRelationship> GetRelationships(Guid clientId)
        {
            var relationsList = new List<ClientRelationship>();

            //My Relationships  

            var myRelations= _db.Table<ClientRelationship>()
                .Where(x => x.ClientId == clientId)
                .ToList();

            foreach (var relation in myRelations)
            {
                relation.Person = _db
                    .Query<Person>("select * from person where id in (select personid from client where id=?)", relation.RelatedClientId)
                    .FirstOrDefault();

                relationsList.Add(relation);
            }

            var otherRelations = _db.Table<ClientRelationship>()
                .Where(x => x.RelatedClientId == clientId)
                .ToList();

            foreach (var relation in otherRelations)
            {
                relation.Person = _db
                    .Query<Person>("select * from person where id in (select personid from client where id=?)", relation.ClientId)
                    .FirstOrDefault();

                relationsList.Add(relation);
            }

            return relationsList;
        }

        public ClientRelationship Find(string relationshipTypeId, Guid clientId, Guid otherClientId)
        {
            return GetAll(x => x.RelationshipTypeId == relationshipTypeId && x.ClientId == clientId &&
                               x.RelatedClientId == otherClientId).FirstOrDefault();
        }
    }
}