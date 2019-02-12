using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class SummaryViewModel : MvxViewModel,ISummaryViewModel
    {
        private Client _client;
        private List<ClientSummary> _summaries;

        public string Title { get; set; }
        public Client Client
        {
            get { return _client; }
            set
            {
                _client = value; 
                RaisePropertyChanged(() => Client);
                ShowSummary();
            }
        }

        public List<ClientSummary> Summaries
        {
            get { return _summaries; }
            set { _summaries = value; RaisePropertyChanged(() => Summaries); }
        }

        public SummaryViewModel()
        {
            Title = "SUMMARY";

        }

        private void ShowSummary()
        {
            if (null != Client)
            {
                if (Client.ClientSummaries.Any())
                    Summaries = Client.ClientSummaries.OrderBy(x=>x.Rank).ToList();
            }
        }
    }
}