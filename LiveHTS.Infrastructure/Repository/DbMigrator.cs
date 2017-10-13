using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Infrastructure.Migrations;
using SQLite;

namespace LiveHTS.Infrastructure.Repository
{
    public class DbMigrator:IDbMigrator
    {
        private readonly ILiveSetting _liveSetting;

        public DbMigrator(ILiveSetting liveSetting)
        {
            _liveSetting = liveSetting;
        }

        public void Migrate()
        {
            Seeder.Seed(new SQLiteConnection(_liveSetting.DatasePath));
        }
    }
}