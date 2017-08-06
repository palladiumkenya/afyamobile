using System;
using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.Seed;
using LiveHTS.Infrastructure.Seed.Config;
using LiveHTS.Infrastructure.Seed.Lookup;
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
            SeedLookup(db);
            SeedSurvey(db);
            SeedSubject(db);
        }

        private static void SeedConfig(SQLiteConnection db)
        {
            #region Config

            db.CreateTable<IdentifierType>();
            db.CreateTable<PracticeType>();
            db.CreateTable<Practice>();
            db.CreateTable<RelationshipType>();
            db.CreateTable<KeyPop>();
            db.CreateTable<MaritalStatus>();
            db.CreateTable<EncounterType>();
            db.CreateTable<ProviderType>();
            #endregion

            InsertOrUpdate(db, new IdentifierTypeJson());
            InsertOrUpdate(db, new PracticeTypeJson());
            InsertOrUpdate(db, new PracticeJson());
            InsertOrUpdate(db, new RelationshipTypeJson());
            InsertOrUpdate(db, new KeyPopJson());
            InsertOrUpdate(db, new MaritalStatusJson());
            InsertOrUpdate(db, new EncounterTypeJson());
            InsertOrUpdate(db, new ProviderTypeJson());
        }

        private static void SeedLookup(SQLiteConnection db)
        {
            #region Lookup

            db.CreateTable<Category>();
            db.CreateTable<Item>();
            db.CreateTable<CategoryItem>();

            #endregion

            InsertOrUpdate(db, new CategoryJson());
            InsertOrUpdate(db, new ItemJson());
            InsertOrUpdate(db, new CategoryItemJson());
        }

        private static void SeedSurvey(SQLiteConnection db)
        {
            #region Survey

            db.CreateTable<Module>();
            db.CreateTable<Form>();

            db.CreateTable<Concept>();
            db.CreateTable<Question>();
            db.CreateTable<QuestionBranch>();
            db.CreateTable<QuestionRemoteTransformation>();
            db.CreateTable<QuestionReValidation>();
            db.CreateTable<QuestionTransformation>();
            db.CreateTable<QuestionValidation>();

            #endregion

            InsertOrUpdate(db, new ModuleJson());
            InsertOrUpdate(db, new FormJson());

            InsertOrUpdate(db, new ConceptJson());
            InsertOrUpdate(db, new QuestionJson());
            InsertOrUpdate(db, new QuestionBranchJson());
            InsertOrUpdate(db, new QuestionRemoteTransformationJson());
            InsertOrUpdate(db, new QuestionReValidationJson());
            InsertOrUpdate(db, new QuestionTransformationJson());
            InsertOrUpdate(db, new QuestionValidationJson());

        }

        private static void SeedSubject(SQLiteConnection db)
        {
            #region Subject

            db.CreateTable<Person>();
            db.CreateTable<PersonContact>();
            db.CreateTable<PersonAddress>();
            db.CreateTable<User>();
            db.CreateTable<Provider>();
            db.CreateTable<Client>();
            db.CreateTable<ClientIdentifier>();
            db.CreateTable<ClientRelationship>();

            #endregion

            InsertOrUpdate(db, new PersonJson());
            InsertOrUpdate(db, new PersonAddressJson());
            InsertOrUpdate(db, new PersonContactJson());
            InsertOrUpdate(db, new UserJson());
            InsertOrUpdate(db, new ProviderTypeJson());
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