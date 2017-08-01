using System;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Subject
{
    public class ClientIdentifier : Entity<Guid>,IEnrollment
    {
        [Indexed]
        public string IdentifierTypeId { get; set; }
        public string Identifier { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Preferred { get; set; }
        [Indexed]
        public Guid ClientId { get; set; }

        public ClientIdentifier()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{IdentifierTypeId}|{Identifier}";
        }
    }
}