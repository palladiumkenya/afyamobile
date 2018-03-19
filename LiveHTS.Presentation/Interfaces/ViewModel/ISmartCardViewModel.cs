using System;
using System.Collections.Generic;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.SmartCard;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface ISmartCardViewModel: IMvxViewModel
    {
        string ShrMessage { get; set; }
        List<string> ShrErrors { get; set; }
        Exception ShrException { get; set; }
        SHR Shr { get; set; }
        SmartClientDTO SmartClient { get; set; }
        List<HIVTestHistoryDTO> HivTestHistories { get; set; }

        IMvxCommand ReadCardCommand { get; }
        Action ReadCardAction { get; set; }
        IMvxCommand WriteCardCommand { get; }
        IMvxCommand TestingCommand { get; }
        void ReadCardDone();
    }
}