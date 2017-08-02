using System;
using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.Seed;
using LiveHTS.Infrastructure.Seed.Config;
using LiveHTS.Infrastructure.Seed.Subject;
using LiveHTS.Infrastructure.Seed.Survey;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Infrastructure.Migrations
{
    public class Seeder
    {
        public static void Seed(SQLiteConnection db)
        {
            SeedConfig(db);
            SeedSurvey(db);
            SeedSubject(db);
        }

        private static void SeedConfig(SQLiteConnection db)
        {
            #region Config

            db.CreateTable<IdentifierType>();
            db.CreateTable<PracticeType>();

            db.CreateTable<Practice>();
            db.CreateTable<KeyPop>();
            db.CreateTable<MaritalStatus>();

            #endregion

            InsertOrUpdate(db, new IdentifierTypeJson());
            InsertOrUpdate(db, new PracticeTypeJson());
            InsertOrUpdate(db, new PracticeJson());

            InsertOrUpdate(db, new KeyPopJson());
            InsertOrUpdate(db, new MaritalStatusJson());

        }

        private static void SeedSurvey(SQLiteConnection db)
        {
            #region Survey

            db.CreateTable<Module>();
            db.CreateTable<Form>();

            #endregion

            InsertOrUpdate(db, new ModuleJson());
            InsertOrUpdate(db, new FormJson());

        }

        private static void SeedSubject(SQLiteConnection db)
        {
            #region Subject

            db.CreateTable<Person>();
            db.CreateTable<PersonContact>();
            db.CreateTable<PersonAddress>();
            db.CreateTable<User>();
            db.CreateTable<Client>();
            db.CreateTable<ClientIdentifier>();
            db.CreateTable<ClientRelationship>();

            #endregion

            InsertOrUpdate(db, new PersonJson());
            InsertOrUpdate(db, new PersonAddressJson());
            InsertOrUpdate(db, new PersonContactJson());
            InsertOrUpdate(db, new UserJson());
            InsertOrUpdate(db, new ClientJson());
            InsertOrUpdate(db, new ClientIdentifierJson());
            InsertOrUpdate(db, new ClientRelationshipJson());
        }

        private static void InsertOrUpdate<T>(SQLiteConnection db, ISeedJson<T> json) 
        {
            foreach (var entity in json.Read())
            {
                var rowsAffected = db.Update(entity);
                if (rowsAffected == 0)
                {
                    db.Insert(entity);
                }
            }
        }
    }
}