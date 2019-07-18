﻿using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Events;
using LiveHTS.SharedKernel.Model;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IClientDemographicViewModel:IStepViewModel
    {
        //Client IndexClient { get; set; }
        ClientDemographicDTO Demographic { get; set; }

        List<CustomItem> GenderOptions { get; set; }
        List<CustomItem> AgeUnitOptions { get; set; }
        IndexClientDTO IndexClientDTO { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string NickName { get; set; }
        CustomItem SelectedGender { get; set; }
        decimal Age { get; set; }
        bool Downloaded { get; set; }
        CustomItem SelectedAgeUnit { get; set; }

        IMvxCommand ShowDateDialogCommand { get; }
        IMvxCommand ShowAgeDialogCommand { get; }
        event EventHandler<ChangedDateEvent> ChangedDate;
        TraceDateDTO SelectedDate { get; set; }
        void ShowDatePicker(Guid refId, DateTime refDate);

        DateTime BirthDate { get; set; }
        string PersonId { get; set; }

        void CalculateBirthDate();
        void CalculateAge();
    }
}
