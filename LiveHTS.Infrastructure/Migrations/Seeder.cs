﻿using System;
using System.Collections.Generic;
using System.Reflection;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Interview;
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
using Action = LiveHTS.Core.Model.Config.Action;
using Module = LiveHTS.Core.Model.Survey.Module;

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
            SeedInterview(db);
        }

        private static void SeedConfig(SQLiteConnection db)
        {
            #region Config

            db.CreateTable<IdentifierType>();
            db.CreateTable<PracticeType>();
            db.CreateTable<Device>();
            db.CreateTable<ServerConfig>();
            db.CreateTable<Practice>();
            db.CreateTable<RelationshipType>();
            db.CreateTable<KeyPop>();
            db.CreateTable<MaritalStatus>();
            db.CreateTable<EncounterType>();
            db.CreateTable<ProviderType>();
            db.CreateTable<ConceptType>();
            db.CreateTable<Action>();
            db.CreateTable<Condition>();
            db.CreateTable<Validator>();
            db.CreateTable<ValidatorType>();
            db.CreateTable<Cohort>();
            db.CreateTable<County>();
            db.CreateTable<SubCounty>();
            #endregion

            InsertOnly<County,int>(db, new CountyJson());
            InsertOnly<SubCounty,Guid>(db, new SubCountyJson());

            InsertOnly<ServerConfig,string>(db, new ServerConfigJson());


            InsertOnly<PracticeType,string>(db, new PracticeTypeJson());
            InsertOnly<Practice, Guid>(db, new PracticeJson());
            InsertOnly<RelationshipType,string>(db, new RelationshipTypeJson());
            InsertOnly<IdentifierType,string>(db, new IdentifierTypeJson());
            InsertOnly<KeyPop,string>(db, new KeyPopJson());
            InsertOnly<MaritalStatus,string>(db, new MaritalStatusJson());
            InsertOnly<EncounterType,Guid>(db, new EncounterTypeJson());
            InsertOnly<ProviderType,string>(db, new ProviderTypeJson());
            InsertOnly<ConceptType,string>(db, new ConceptTypeJson());
            InsertOnly<Action,string>(db, new ActionJson());
            InsertOnly<Condition,string>(db, new ConditionJson());
            InsertOnly<Validator,string>(db, new ValidatorJson());
            InsertOnly<ValidatorType,string>(db, new ValidatorTypeJson());
        }

        private static void SeedLookup(SQLiteConnection db)
        {
            #region Lookup

            db.CreateTable<Category>();
            db.CreateTable<Item>();
            db.CreateTable<CategoryItem>();

            #endregion

//            InsertOrUpdate(db, new CategoryJson());
//            InsertOrUpdate(db, new ItemJson());
//            InsertOrUpdate(db, new CategoryItemJson());
        }

        private static void SeedSurvey(SQLiteConnection db)
        {
            #region Survey

            db.CreateTable<Module>();
            db.CreateTable<Form>();
            db.CreateTable<Program>();

            db.CreateTable<Concept>();
            db.CreateTable<Question>();
            db.CreateTable<QuestionBranch>();
            db.CreateTable<QuestionRemoteTransformation>();
            db.CreateTable<QuestionReValidation>();
            db.CreateTable<QuestionTransformation>();
            db.CreateTable<QuestionValidation>();

            #endregion

            /*
            InsertOrUpdate(db, new ModuleJson());
            InsertOrUpdate(db, new FormJson());
            InsertOrUpdate(db, new ProgramJson());

            InsertOrUpdate(db, new ConceptJson());
            InsertOrUpdate(db, new QuestionJson());
            InsertOrUpdate(db, new QuestionBranchJson());
            //InsertOrUpdate(db, new QuestionRemoteTransformationJson());
            //InsertOrUpdate(db, new QuestionReValidationJson());
            //InsertOrUpdate(db, new QuestionTransformationJson());
            InsertOrUpdate(db, new QuestionValidationJson());
            */
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
            db.CreateTable<ClientSummary>();
            #endregion

            InsertOnly<Person,Guid>(db, new PersonUserJson());
            //            InsertOrUpdate(db, new PersonAddressJson());
            //            InsertOrUpdate(db, new PersonContactJson());
            InsertOnly<User,Guid>(db, new UserJson());
            InsertOnly<Provider,Guid>(db, new ProviderJson());
//            InsertOrUpdate(db, new ClientJson());
//            InsertOrUpdate(db, new ClientIdentifierJson());
//            InsertOrUpdate(db, new ClientRelationshipJson());
        }

        private static void SeedInterview(SQLiteConnection db)
        {
            #region Encounter

            db.CreateTable<Encounter>();
            db.CreateTable<Obs>();
            db.CreateTable<ObsTestResult>();
            db.CreateTable<ObsFinalTestResult>();
            db.CreateTable<ObsLinkage>();
            db.CreateTable<ObsTraceResult>();
            db.CreateTable<ObsMemberScreening>();
            db.CreateTable<ObsPartnerScreening>();
            db.CreateTable<ObsFamilyTraceResult>();
            db.CreateTable<ObsPartnerTraceResult>();

            #endregion

            //InsertOrUpdate(db, new EncounterJson());
        }

        private static void InsertOrUpdate<T>(SQLiteConnection db, ISeedJson<T> json)
        {
            
            //            try
            //            {
            foreach (var entity in json.Read())
            {
                var rowsAffected = db.Update(entity);
                if (rowsAffected == 0)
                {
                    db.Insert(entity);
                }
            }
//            }
//            catch (Exception e)
//            {
//                var s = json;
//                var m = e.Message;
//                throw;
//            }

        }

        private static void InsertOnly<T,TId>(SQLiteConnection db, ISeedJson<T> json) where T:Entity<TId>, new()
        {
           
            foreach (var entity in json.Read())
            {
                //var rowsAffected = db.Find<T>(entity.Id);
                var records = db.Table<T>().Count();

                if (records == 0)
                {
                    db.Insert(entity);
                }
            }
        }

    }
}
