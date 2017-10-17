using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.Presentation.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

namespace LiveHTS.Presentation.ViewModel
{
    public class FamilyMemberViewModel : MvxViewModel, IFamilyMemberViewModel
    {

        private readonly IDialogService _dialogService;
        private readonly IDashboardService _dashboardService;

        private List<FamilyMemberTemplateWrap> _familyMembers = new List<FamilyMemberTemplateWrap>();

        private Client _client;
        private IMvxCommand _addFamilyMemberCommand;
        public string Title { get; set; }

        public Client Client
        {
            get { return _client; }
            set
            {
                _client = value; RaisePropertyChanged(() => Client);
                FamilyMembers = ConvertToFamilyMemberWrapperClass(Client, this);
            }
        }
        public List<FamilyMemberTemplateWrap> FamilyMembers
        {
            get { return _familyMembers; }
            set { _familyMembers = value; RaisePropertyChanged(() => FamilyMembers); }
        }

        public IMvxCommand AddFamilyMemberCommand
        {
            get
            {
                _addFamilyMemberCommand = _addFamilyMemberCommand ?? new MvxCommand(AddRelationShip);
                return _addFamilyMemberCommand;
            }
        }

        public FamilyMemberViewModel()
        {
            Title = "FAMILY";
            _dialogService = Mvx.Resolve<IDialogService>();
            _dashboardService = Mvx.Resolve<IDashboardService>();
        }
        private void AddRelationShip()
        {
            ShowViewModel<ClientRelationshipsViewModel>(new { id = Client.Id, reltype = "Family" });
        }
        public async void RemoveFamilyMember(FamilyMemberTemplate template)
        {
            try
            {
                var result = await _dialogService.ConfirmAction("Are you sure ?", "Remove Family Member");
                if (result)
                {
                    _dashboardService.RemoveRelationShip(template.Id);
                    Client = _dashboardService.LoadClient(Client.Id);
                }
            }
            catch (Exception e)
            {
                MvxTrace.Error(e.Message);
                _dialogService.Alert(e.Message, "Remove Family Member");
            }
        }
        private static List<FamilyMemberTemplateWrap> ConvertToFamilyMemberWrapperClass(Client client, IFamilyMemberViewModel familyMemberViewModel)
        {
            var clientRelationships = client.Relationships.Where(x => x.RelationshipTypeId.ToLower() != "Partner".ToLower()).ToList();

            List<FamilyMemberTemplateWrap> list = new List<FamilyMemberTemplateWrap>();
            foreach (var r in clientRelationships)
            {
                list.Add(new FamilyMemberTemplateWrap(new FamilyMemberTemplate(r), familyMemberViewModel));
            }

            return list;
        }
    }
}