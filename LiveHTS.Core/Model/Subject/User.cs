﻿using System;
using System.Collections.Generic;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Subject
{
    public class User:Entity<Guid>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        [Indexed]
        public Guid? PracticeId { get; set; }
        [Indexed]
        public Guid PersonId { get; set; }
        public int? UserId { get; set; }
        [Ignore]
        public Person Person { get; set; }
        [Ignore]
        public Provider Provider { get; set; }
        [Ignore]
        public ICollection<UserSummary> UserSummaries { get; set; }=new List<UserSummary>();
        public User()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{Person}";
        }
    }
}