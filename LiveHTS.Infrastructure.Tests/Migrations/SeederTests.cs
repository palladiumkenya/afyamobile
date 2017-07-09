using System;
using System.Linq;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.Migrations;
using LiveHTS.Infrastructure.Seed;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;

namespace LiveHTS.Infrastructure.Tests.Migrations
{
    [TestClass]
    public class SeederTests
    {
        [TestMethod]
        public void should_Read_Module_Json()
        {
            var modules = ModuleJson.Read();
            Assert.IsTrue(modules.Count>0);

            foreach (var module in modules)
            {
                Console.WriteLine(module);
            }
        }
        [TestMethod]
        public void should_Read_Form_Json()
        {
            var forms = FormJson.Read();
            Assert.IsTrue(forms.Count > 0);

            foreach (var form in forms)
            {
                Console.WriteLine(form);
            }
        }
        [TestMethod]
        public void should_Seed()
        {
            var db=new SQLiteConnection("testlivehts.db");
            Seeder.Seed(db);
            var modules = db.Table<Module>().ToList();
            var forms = db.Table<Form>().ToList();
            Assert.IsTrue(modules.Count>0);
            Assert.IsTrue(forms.Count>0);
        }
    }
}