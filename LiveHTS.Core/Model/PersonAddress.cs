using System;
using System.Collections.Generic;
using System.Reflection;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model
{
    public class PersonAddress : Entity<Guid>
    {
        public string Landmark { get; set; }
        [Indexed]
        public Guid? CountyId { get; set; }
        public bool Preferred { get; set; }
        [Indexed]
        public Guid PersonId { get; set; }
    }
}