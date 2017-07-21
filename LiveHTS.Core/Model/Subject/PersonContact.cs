﻿using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Subject
{
    public class PersonContact : Entity<Guid>
    {
        public int Phone { get; set; }
        public bool Preferred { get; set; }
        [Indexed]
        public Guid PersonId { get; set; }

        public PersonContact()
        {
            Id = LiveGuid.NewGuid();
        }
    } 
}