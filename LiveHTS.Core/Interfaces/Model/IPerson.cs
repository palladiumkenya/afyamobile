using System;

namespace LiveHTS.Core.Interfaces.Model
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string Gender { get; set; }
        DateTime BirthDate { get; set; }
        bool? BirthDateEstimated { get; set; }
        
    }
}