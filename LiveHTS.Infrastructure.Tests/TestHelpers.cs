using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using LiveHTS.Core.Model.Survey;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;

namespace LiveHTS.Infrastructure.Tests
{
    public class TestHelpers
    {
        

        public static SQLiteConnection GetTestDatabase()
        {
            var db=new SQLiteConnection("livehts");
            db.CreateTable<Module>();
            db.InsertAll(ReadCsv<Module>());
            return db;
        }

        public static List<T> ReadCsv<T>() where T:class
        {
            
            var name = typeof(T).Name;
            var folder = Directory.GetCurrentDirectory();

            folder = folder.EndsWith(@"\") ? folder : $@"{folder}\";


            List<T> records;
            using (TextReader reader =  File.OpenText($@"{folder}Seed\{name}.csv"))
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
    }

  
}