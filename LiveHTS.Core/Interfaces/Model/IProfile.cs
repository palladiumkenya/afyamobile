using System;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Interfaces.Model
{
    public interface IProfile
    {
        string MaritalStatus { get; set; }
        string KeyPop { get; set; }
        string OtherKeyPop { get; set; }
        Guid? Education { get; set; }
        Guid? Completion { get; set; }
        Guid? Occupation { get; set; }
    }
}