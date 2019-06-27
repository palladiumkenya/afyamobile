using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Subject;
using NUnit.Framework;

namespace LiveHTS.Core.Tests.Model
{
    [TestFixture]
    public class SyncClientRelationsDTOTests
    {
        private List<ClientRelationship> _relations;

        private Guid _indexA=Guid.NewGuid();
        private Guid _indexB = Guid.NewGuid();

        [SetUp]
        public void SetUp()
        {
            _relations = Builder<ClientRelationship>
                .CreateListOfSize(3)
                .All()
                .With(x=>x.RelatedClientId=Guid.NewGuid())
                .Build()
                .ToList();

            _relations[0].ClientId = _indexA;
            _relations[1].ClientId = _indexA;
            _relations[2].ClientId = _indexB;
        }

        [Test]
        public void should_Create()
        {
            var dto = SyncClientPriorityDTO.Create(_relations);
            Assert.True(dto.Any());
            foreach (var d in dto)
            {
                Console.WriteLine(d.IndexId);
                foreach (var secondaryId in d.SecondaryIds)
                {
                    Console.WriteLine($" {secondaryId}");
                }
            }
           Assert.AreEqual(5,dto.Count+dto.SelectMany(x=>x.SecondaryIds).Count());


           var dtoo = SyncClientPriorityDTO.Create(new List<ClientRelationship>());
           Assert.False(dtoo.Any());
           Assert.AreEqual(0,dtoo.Count+dtoo.SelectMany(x=>x.SecondaryIds).Count());

        }

        [Test]
        public void should_GetId_Not_In()
        {
            var allIds=new Guid[3];
           allIds[0]=Guid.NewGuid();
           allIds[1]=Guid.NewGuid();
           allIds[2]=Guid.NewGuid();


            var excludeIds = allIds.Take(2).ToList();

            var remaingIds = allIds.Where(x => !excludeIds.Contains(x)).ToList();
            Assert.True(remaingIds.Count==1);
        }
    }
}
