using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.Tests.Repository;
using SQLite;


namespace LiveHTS.Infrastructure.Tests
{
    public class TestHelpers
    {
        public static SQLiteConnection GetDatabase(bool withData = true)
        {
            var db = new SQLiteConnection("livehts.db");

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
            }
            return db;
        }

        public static List<T> ReadCsv<T>() where T : class
        {
            var name = typeof(T).Name;
            var folder = Directory.GetCurrentDirectory();

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
        public static SQLiteConnection GetTestDatabase(bool withData = true)
        {
            var db = new SQLiteConnection("test.db");
            db.CreateTable<TestCar>();
            db.CreateTable<TestModel>();
            db.DeleteAll<TestModel>();
            db.DeleteAll<TestCar>();
            if (withData)
            {
                db.InsertAll(ReadCsv<TestCar>());
                db.InsertAll(ReadCsv<TestModel>());
            }
            return db;
        }
    }
}