using System;
using System.Collections.Generic;
using System.Reflection;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model
{
    public class PersonContact : Entity<Guid>
    {
        public int Phone { get; set; }
        public bool Preferred { get; set; }
        [Indexed]
        public Guid PersonId { get; set; }
    } 
}