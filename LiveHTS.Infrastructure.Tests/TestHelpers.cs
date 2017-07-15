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

            db.CreateTable<Module>();
            db.CreateTable<Form>();
            db.CreateTable<Category>();
            db.CreateTable<Item>();
            db.CreateTable<CategoryItem>();
            db.CreateTable<ConceptType>();
            db.CreateTable<Concept>();
            db.CreateTable<Question>();
            db.CreateTable<Validator>();
            db.CreateTable<ValidatorType>();

            db.CreateTable<Action>();
            db.CreateTable<Condition>();
            db.CreateTable<SubjectAttribute>();

            db.CreateTable<QuestionBranch>();
            db.CreateTable<QuestionRemoteTransformation>();
            db.CreateTable<QuestionReValidation>();
            db.CreateTable<QuestionTransformation>();
            db.CreateTable<QuestionValidation>();
            
            db.DeleteAll<Form>();
            db.DeleteAll<Module>();

            db.DeleteAll<CategoryItem>();
            db.DeleteAll<Item>();
            db.DeleteAll<Category>();

            db.DeleteAll<Concept>();
            db.DeleteAll<ConceptType>();

            db.DeleteAll<QuestionBranch>();
            db.DeleteAll<QuestionRemoteTransformation>();
            db.DeleteAll<QuestionReValidation>();
            db.DeleteAll<QuestionTransformation>();
            db.DeleteAll<QuestionValidation>();

            db.DeleteAll<Validator>();
            db.DeleteAll<ValidatorType>();
            db.DeleteAll<Question>();
            db.DeleteAll<Action>();
            db.DeleteAll<Condition>();
            db.DeleteAll<SubjectAttribute>();

            if (withData)
            {
                db.InsertAll(ReadCsv<Module>());
                db.InsertAll(ReadCsv<Form>());
                db.InsertAll(ReadCsv<Category>());
                db.InsertAll(ReadCsv<Item>());
                db.InsertAll(ReadCsv<CategoryItem>());
                db.InsertAll(ReadCsv<ConceptType>());
                db.InsertAll(ReadCsv<Concept>());
                db.InsertAll(ReadCsv<Action>());
                db.InsertAll(ReadCsv<Condition>());
                db.InsertAll(ReadCsv<SubjectAttribute>());
                db.InsertAll(ReadCsv<Question>());
                db.InsertAll(ReadCsv<Validator>());
                db.InsertAll(ReadCsv<ValidatorType>());
                db.InsertAll(ReadCsv<QuestionBranch>());
                db.InsertAll(ReadCsv<QuestionRemoteTransformation>());
                db.InsertAll(ReadCsv<QuestionReValidation>());
                db.InsertAll(ReadCsv<QuestionTransformation>());
                db.InsertAll(ReadCsv<QuestionValidation>());
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