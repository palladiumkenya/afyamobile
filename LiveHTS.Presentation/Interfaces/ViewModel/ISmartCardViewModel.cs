using System;
using System.Collections.Generic;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.SmartCard;
using LiveHTS.Core.Model.Subject;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface ISmartCardViewModel: IMvxViewModel
    {
        ClientShrRecord ClientShrRecord { get; set; }
        Client ClientShr { get; set; }
        Encounter EncounterShr { get; set; }
        string ShrMessage { get; set; }
        List<string> ShrErrors { get; set; }
        Exception ShrException { get; set; }
        SHR Shr { get; set; }
        SmartClientDTO SmartClient { get; set; }
        List<HIVTestHistoryDTO> HivTestHistories { get; set; }
        bool ShowTesting { get; set; }
        bool ShowReadCard { get; set; }
        IMvxCommand ReadCardCommand { get; }
        Action ReadCardAction { get; set; }
        IMvxCommand WriteCardCommand { get; }
        IMvxCommand TestingCommand { get; }
        void ReadCardDone();
    }
}