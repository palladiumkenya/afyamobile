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
        }
    }
}