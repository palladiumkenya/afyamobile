using System;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Interfaces.Model
{
    public interface IEnrollment
    {
        string IdentifierTypeId { get; set; }
        string Identifier { get; set; }
        DateTime RegistrationDate { get; set; }
    }
}