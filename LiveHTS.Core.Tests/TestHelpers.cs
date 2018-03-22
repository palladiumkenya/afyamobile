using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using FizzWare.NBuilder;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.SmartCard;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using NUnit.Framework;
using SQLite;

namespace LiveHTS.Core.Tests
{

    public class TestHelpers
    {
        public static bool UseNunit;
        public static SQLiteConnection GetDatabase(string database= "livehts.db", bool withData = true)
        {

            var db = new SQLiteConnection(database,false);

            #region Module
            db.CreateTable<Module>();
            db.CreateTable<Form>();
            #endregion

            #region Lookups
            db.CreateTable<Category>();
            db.CreateTable<Item>();
            db.CreateTable<CategoryItem>();
            #endregion

            #region Configs
            db.CreateTable<Action>();
            db.CreateTable<Condition>();
            db.CreateTable<ConceptType>();
            db.CreateTable<Validator>();
            db.CreateTable<ValidatorType>();
            db.CreateTable<ClientAttribute>();

            db.CreateTable<County>();
            db.CreateTable<SubCounty>();
            db.CreateTable<EncounterType>();
            db.CreateTable<IdentifierType>();
            db.CreateTable<PracticeType>();
            db.CreateTable<ProviderType>();
            db.CreateTable<RelationshipType>();
            db.CreateTable<Device>();
            db.CreateTable<Practice>();

            #endregion

            #region Question
            db.CreateTable<Concept>();
            db.CreateTable<Question>();
            #endregion

            #region Question-Meta
            db.CreateTable<QuestionBranch>();
            db.CreateTable<QuestionRemoteTransformation>();
            db.CreateTable<QuestionReValidation>();
            db.CreateTable<QuestionTransformation>();
            db.CreateTable<QuestionValidation>();
            #endregion

            #region Data
            db.CreateTable<Person>();
            db.CreateTable<PersonAddress>();
            db.CreateTable<PersonContact>();
            db.CreateTable<Client>();
            db.CreateTable<ClientIdentifier>();
            db.CreateTable<ClientRelationship>();
            db.CreateTable<User>();
            db.CreateTable<Provider>();
            db.CreateTable<Encounter>();
            db.CreateTable<Obs>();
            #endregion

            #region Delete Data
            db.DeleteAll<Obs>();
            db.DeleteAll<Encounter>();
            db.DeleteAll<ClientIdentifier>();
            db.DeleteAll<PersonAddress>();
            db.DeleteAll<PersonContact>();
            db.DeleteAll<Client>();
            db.DeleteAll<User>();
            db.DeleteAll<Provider>();
            db.DeleteAll<Person>();
            #endregion

            #region Delete Question-Meta
            db.DeleteAll<QuestionBranch>();
            db.DeleteAll<QuestionRemoteTransformation>();
            db.DeleteAll<QuestionReValidation>();
            db.DeleteAll<QuestionTransformation>();
            db.DeleteAll<QuestionValidation>();
            #endregion

            #region Delete Question
            db.DeleteAll<Question>();
            db.DeleteAll<Concept>();
            #endregion


            #region Delete Configs
            db.DeleteAll<Action>();
            db.DeleteAll<Condition>();
            db.DeleteAll<ConceptType>();
            db.DeleteAll<Validator>();
            db.DeleteAll<ValidatorType>();
            db.DeleteAll<ClientAttribute>();
            db.DeleteAll<County>();
            db.DeleteAll<SubCounty>();
            db.DeleteAll<EncounterType>();
            db.DeleteAll<IdentifierType>();
            db.DeleteAll<PracticeType>();
            db.DeleteAll<ProviderType>();
            db.DeleteAll<RelationshipType>();
            db.DeleteAll<Device>();
            db.DeleteAll<Practice>();
            #endregion

            #region Delete Lookups
            db.DeleteAll<CategoryItem>();
            db.DeleteAll<Category>();
            db.DeleteAll<Item>();
            #endregion

            #region Delete Module
            db.DeleteAll<Form>();
            db.DeleteAll<Module>();
            #endregion


            if (withData)
            {
                #region Module
                db.InsertAll(ReadCsv<Module>());
                db.InsertAll(ReadCsv<Form>());
                #endregion

                #region Lookups
                db.InsertAll(ReadCsv<Category>());
                db.InsertAll(ReadCsv<Item>());
                db.InsertAll(ReadCsv<CategoryItem>());
                #endregion

                #region Configs
                db.InsertAll(ReadCsv<Action>());
                db.InsertAll(ReadCsv<Condition>());
                db.InsertAll(ReadCsv<ConceptType>());
                db.InsertAll(ReadCsv<Validator>());
                db.InsertAll(ReadCsv<ValidatorType>());
                db.InsertAll(ReadCsv<ClientAttribute>());
                db.InsertAll(ReadCsv<County>());
                db.InsertAll(ReadCsv<SubCounty>());
                db.InsertAll(ReadCsv<EncounterType>());
                db.InsertAll(ReadCsv<IdentifierType>());
                db.InsertAll(ReadCsv<PracticeType>());
                db.InsertAll(ReadCsv<ProviderType>());
                db.InsertAll(ReadCsv<RelationshipType>());
                db.InsertAll(ReadCsv<Device>());
                db.InsertAll(ReadCsv<Practice>());
                #endregion

                #region Question
                db.InsertAll(ReadCsv<Concept>());
                db.InsertAll(ReadCsv<Question>());
                #endregion

                #region Question-Meta
                db.InsertAll(ReadCsv<QuestionBranch>());
                db.InsertAll(ReadCsv<QuestionRemoteTransformation>());
                db.InsertAll(ReadCsv<QuestionReValidation>());
                db.InsertAll(ReadCsv<QuestionTransformation>());
                db.InsertAll(ReadCsv<QuestionValidation>());
                #endregion

                var clients = TestDataHelpers.GetTestClients(2);
                var users = TestDataHelpers.GetTestUsers(1);
                var providers = TestDataHelpers.GetTestProviders(1);

                #region Data
                db.InsertAll(clients.Select(x => x.Person));
                db.InsertAll(users.Select(x => x.Person));
                db.InsertAll(providers.Select(x => x.Person));

                db.InsertAll(clients);

                var ids = clients.SelectMany(x => x.Identifiers);
                var rels = clients.SelectMany(x => x.Relationships);

                db.InsertAll(ids);
                db.InsertAll(rels);

                db.InsertAll(users);
                db.InsertAll(providers);
                var encounters = TestDataHelpers.GetTestEncounters(2, clients, users, providers);
                var obs = encounters.SelectMany(x => x.Obses).ToList();
                db.InsertAll(encounters);
                db.InsertAll(obs);
                #endregion
            }
            return db;
        }

        public static List<T> ReadCsv<T>() where T : class
        {
            var name = typeof(T).Name;
            var folder = UseNunit
                ? TestContext.CurrentContext.TestDirectory
                : Directory.GetCurrentDirectory();

            folder = folder.EndsWith(@"\") ? folder : $@"{folder}\";

            List<T> records;
            using (TextReader reader = File.OpenText($@"{folder}Seed\{name}.csv"))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.Delimiter = "|";
                csv.Configuration.TrimFields = true;
                csv.Configuration.TrimHeaders = true;
                csv.Configuration.WillThrowOnMissingField = false;
                records = csv.GetRecords<T>().ToList();
            }
            return records;
        }

       

        public static Form CreateTestFormWithQuestions(int questionCount)
        {
            var form = Builder<Form>.CreateNew().Build();
            var qs = Builder<Question>.CreateListOfSize(questionCount).All().With(x => x.FormId == form.Id).Build().ToList();
            int count = 0;
            foreach (var q in qs)
            {
                count++;
                q.Ordinal = $"{count}";
                q.Rank = count;
            }
            form.Questions = qs;
            
            return form;
        }
        public static Encounter CreateTestEncountersWithObs(Form form)
        {
            var encounter = Builder<Encounter>.CreateNew()
                .With(x=>x.FormId=form.Id)
                .With(x => x.IsComplete = true)
                .With(x => x.Voided = false)
                .Build();

            var obs = Builder<Obs>.CreateListOfSize(form.Questions.Count).All().With(x => x.EncounterId = encounter.Id)
                .Build().ToList();
            var questions = form.Questions;
            for (int i = 0; i < questions.Count; i++)
            {
                obs[i].QuestionId = questions[i].Id;
            }
            encounter.Obses = obs;
            return encounter;
        }

        public static Encounter CreateTestEncounters(Form form)
        {
            var encounter = Builder<Encounter>.CreateNew()
                .With(x => x.FormId = form.Id)
                .With(x => x.IsComplete = false)
                .With(x => x.Voided = false)
                .Build();
            return encounter;
        }
    }
}