using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientEnrollmentViewModel : IStepViewModel
    {
        string ClientInfo { get; set; }
        IEnumerable<IdentifierType> IdentifierTypes { get; set; }
        IdentifierType SelectedIdentifierType { get; set; }
        string Identifier { get; set; }
        DateTime RegistrationDate { get; set; }
    }
}