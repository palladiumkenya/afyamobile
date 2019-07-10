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

//            var otherRelations = _db.Table<ClientRelationship>()
//                .Where(x => x.RelatedClientId == clientId)
//                .ToList();
//
//            foreach (var relation in otherRelations)
//            {
//                //TODO: Save reverse relations
//
//                relation.Person = _db
//                    .Query<Person>("select * from person where id in (select personid from client where id=?)", relation.ClientId)
//                    .FirstOrDefault();
//
//                relation.RelatedClientId = relation.ClientId;
//                relation.ClientId = clientId;
//                relationsList.Add(relation);
//            }

            return relationsList;
        }

        public IEnumerable<ClientRelationship> GetPracticeRelationships(Guid practiceId)
        {
            var relations = _db
                .Query<ClientRelationship>(@"
                        select r.* from ClientRelationship r inner join Client c on r.ClientId=c.Id
                        where r.IsIndex=0 and c.PracticeId=?", practiceId)
                .ToList();

            return relations;
        }

        public ClientRelationship Find(string relationshipTypeId, Guid clientId, Guid otherClientId)
        {
            return GetAll(x => x.RelationshipTypeId == relationshipTypeId && x.ClientId == clientId &&
                               x.RelatedClientId == otherClientId).FirstOrDefault();
        }

        public void Purge(Guid clientId)
        {
            _db.Execute($"DELETE FROM {nameof(ClientRelationship)} WHERE ClientId=?", clientId.ToString());
        }

        public void PurgeRel(Guid guid)
        {
            _db.Execute($"DELETE FROM {nameof(ClientRelationship)} WHERE ClientId=?", guid.ToString());
        }

        public override void Delete(Guid id)
        {
            var relation = Get(id);
            if (null != relation)
            {
                base.Delete(id);
                var ids = GetAll(
                        x => x.RelatedClientId == relation.ClientId &&
                             x.ClientId == relation.RelatedClientId)
                    .Select(x => x.Id)
                    .ToList();

                foreach (var did in ids)
                {
                   base.Delete(did);
                }
            }
        }
    }
}
