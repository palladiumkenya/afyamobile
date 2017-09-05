using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Model.Survey;


namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class ProgramRepository : BaseRepository<Program, Guid>, IProgramRepository
    {
        public ProgramRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
    }
}