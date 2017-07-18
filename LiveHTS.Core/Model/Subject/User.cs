using System;
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

        public User()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}