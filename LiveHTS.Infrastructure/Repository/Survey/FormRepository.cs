using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Infrastructure.DummyData;

namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class FormRepository:BaseRepository<Form>,IFormRepository
    {
        private readonly Module _module;

        public FormRepository()
        {
            foreach(var m in LiveDatabase.Read())
            {
                _entities.AddRange(m.Forms);
            }             
        }
    }
}