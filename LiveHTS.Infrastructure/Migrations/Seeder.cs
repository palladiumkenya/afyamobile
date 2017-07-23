using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.Seed;
using SQLite;

namespace LiveHTS.Infrastructure.Migrations
{
    public class Seeder
    {
        public static void Seed(SQLiteConnection db)
        {            
            db.CreateTable<Module>();
            db.CreateTable<Form>();
            db.CreateTable<PracticeType>();
            db.CreateTable<Practice>();
            db.CreateTable<Person>();
            db.CreateTable<User>();
            
            //Modules
            foreach (var module in ModuleJson.Read())
            {
                var exisits = db.Find<Module>(module.Id);
                if (null == exisits)
                {
                    db.Insert(module);
                }
            }
            
            //Forms
            foreach (var form in FormJson.Read())
            {
                var exisits = db.Find<Form>(form.Id);
                if (null == exisits)
                {
                    db.Insert(form);
                }
            }

            //PracticeType
            foreach (var practiceType in PracticeTypeJson.Read())
            {
                var exisits = db.Find<PracticeType>(practiceType.Id);
                if (null == exisits)
                {
                    db.Insert(practiceType);
                }
            }

            //Practice
            foreach (var practice in PracticeJson.Read())
            {
                var exisits = db.Find<Practice>(practice.Id);
                if (null == exisits)
                {
                    db.Insert(practice);
                }
            }

            //Person
            foreach (var person in PersonJson.Read())
            {
                var exisits = db.Find<Person>(person.Id);
                if (null == exisits)
                {
                    db.Insert(person);
                }
            }

            //User
            foreach (var user in UserJson.Read())
            {
                var exisits = db.Find<User>(user.Id);
                if (null == exisits)
                {
                    db.Insert(user);
                }
            }
        }

        
    }
}