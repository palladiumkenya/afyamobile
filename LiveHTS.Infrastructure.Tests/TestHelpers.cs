using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
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
            db.DeleteAll<Form>();
            db.DeleteAll<Module>();
            if (withData)
            {
                db.InsertAll(ReadCsv<Module>());
                db.InsertAll(ReadCsv<Form>());
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