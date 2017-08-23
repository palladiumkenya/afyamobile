using System;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Interview
{
    public class ObsFinalTestResult : Entity<Guid>
    {       
        [Indexed]
        public Guid Result { get; set; }
        [Indexed]
        public Guid EncounterId { get; set; }
    }
}