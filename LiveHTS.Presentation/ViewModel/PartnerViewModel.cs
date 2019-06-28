using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using LiveHTS.SharedKernel.Model;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

namespace LiveHTS.Presentation.ViewModel
{
    public class PartnerViewModel :MvxViewModel, IPartnerViewModel
    {

        private readonly IDialogService _dialogService;
        private readonly IDashboardService _dashboardService;

        private List<PartnerTemplateWrap> _partners=new List<PartnerTemplateWrap>();

        private Client _client;
        private IMvxCommand _addPartnerCommand;
        private bool _showAddPartner;
        public IDashboardViewModel Parent { get; set; }
        public string Title { get; set; }

        public Client Client
        {
            get { return _client; }
            set
            {
                _client = value; RaisePropertyChanged(() => Client);
                ShowAddPartner = Client.IsInState(LiveState.HtsPnsAcceptedYes,LiveState.HtsEnrolled);
                Partners = ConvertToPartnerWrapperClass(Client, this);
            }
        }
        public List<PartnerTemplateWrap> Partners
        {
            get { return _partners; }
            set { _partners = value; RaisePropertyChanged(() => Partners); }
        }

        public bool ShowAddPartner
        {
            get { return _showAddPartner; }
            set
            {
                _showAddPartner = value;
                RaisePropertyChanged(() => ShowAddPartner);
            }
        }

        public IMvxCommand AddPartnerCommand
        {
            get
            {
                _addPartnerCommand = _addPartnerCommand ?? new MvxCommand(AddRelationShip);
                return _addPartnerCommand;
            }
        }



        public PartnerViewModel()
        {
            Title = "PARTNERS";

            _dialogService = Mvx.Resolve<IDialogService>();
            _dashboardService = Mvx.Resolve<IDashboardService>();

        }
        private void AddRelationShip()
        {
            ShowViewModel<ClientRelationshipsViewModel>(new { id = Client.Id ,reltype= "Partner" });
        }
        public async void RemoveRelationship(PartnerTemplate template)
        {
            try
            {
                var result = await _dialogService.ConfirmAction("Are you sure ?", "Remove Partner");
                if (result)
                {
                    _dashboardService.RemoveRelationShip(template.Id);
                    Client = _dashboardService.LoadClient(Client.Id);
                }
            }
            catch (Exception e)
            {
                MvxTrace.Error(e.Message);
                _dialogService.Alert(e.Message, "Remove Relationship");
            }
        }

        public void ShowDashboard(PartnerTemplate template)
        {
//            Close(this);
//            Parent.ShowDashboard(template.RelatedClientId.ToString(), template.ClientId.ToString(), "pns");
            ShowViewModel<StandByViewModel>(new { id = template.RelatedClientId.ToString(), callerId = template.ClientId.ToString(), mode = "pns" });
        }

        private static List<PartnerTemplateWrap> ConvertToPartnerWrapperClass(Client client, IPartnerViewModel partnerViewModel)
        {
            var clientRelationships = client.Relationships.ToList().Where(x => x.IsPatner()).ToList();

            List<PartnerTemplateWrap> list = new List<PartnerTemplateWrap>();
            foreach (var r in clientRelationships)
            {
                list.Add(new PartnerTemplateWrap(new PartnerTemplate(r), partnerViewModel));
            }

            return list;
        }
    }
}
