using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Infrastructure.Migrations;
using LiveHTS.Infrastructure.Repository.Subject;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SQLite;

namespace LiveHTS.Infrastructure.Tests.Repository.Subject
{
    [TestFixture]
    public class ClientRelationshipRepositoryTests
    {
        private SQLiteConnection _connection;
        private IClientRelationshipRepository _repository;

        private readonly Guid  _clientBob = new Guid("4547b7e0-98c7-4c6f-9d2a-a7b7016df234");    //  Bob Swagger
        private readonly Guid _personBob = new Guid("1fa07f17-d5fe-4daf-9eee-a7b7016df234");    //  Bob Swagger

        private readonly Guid _clientJulie = new Guid("4547b7e0-98c7-4c6f-9d2a-a7b7016df232");   //  Julie Swagger
        private readonly Guid _personJulie = new Guid("82dfdc68-6c3c-4a39-8f1f-a7b7016df22e");   //  Julie Swagger
        
        
        [SetUp]
        public void SetUp()
        {
            _connection = new SQLiteConnection("testlivehts.db");
            Seeder.Seed(_connection);
            _repository=new ClientRelationshipRepository(new LiveSetting(_connection.DatabasePath));
        }

        [Test]
        public void should_GetRelationships()
        {
            var relations = _repository.GetRelationships(_clientBob).ToList();
            Assert.IsTrue(relations.Count>0);
            var reltionship = relations.FirstOrDefault();
            Assert.IsNotNull(reltionship);
            Assert.IsNotNull(reltionship.Person);
            Assert.AreEqual("Julie", reltionship.Person.FirstName);

            Assert.AreEqual(_clientBob, reltionship.ClientId);
            Assert.AreEqual(_clientJulie, reltionship.RelatedClientId);

            foreach (var r in relations)
            {
                Console.WriteLine(r);
            }
        }

        [Test]
        public void should_GetRelationships_Indirect()
        {
            var relations = _repository.GetRelationships(_clientJulie).ToList();
            Assert.IsTrue(relations.Count > 0);
            var reltionship = relations.FirstOrDefault();
            Assert.IsNotNull(reltionship);
            Assert.IsNotNull(reltionship.Person);
            Assert.AreEqual("Bob", reltionship.Person.FirstName);

            Assert.AreEqual(_clientJulie, reltionship.ClientId);
            Assert.AreEqual(_clientBob, reltionship.RelatedClientId);

            foreach (var r in relations)
            {
                Console.WriteLine(r);
            }
        }
    }
}