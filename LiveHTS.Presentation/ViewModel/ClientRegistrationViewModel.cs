using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientRegistrationViewModel:MvxViewModel,IClientRegistrationViewModel
    {
        private readonly List<IStepViewModel> _viewModels;
        private  IDialogService _dialogService = null;
        private ILookupService _lookupService = null;


        public IEnumerable<IStepViewModel> ViewModels
        {
            get { return _viewModels.OrderBy(x => x.Step).ToList(); }
        }

        public void ShowStep(IStepViewModel viewModel)
        {
            ShowViewModel(viewModel.GetType());
        }

        public ClientRegistrationViewModel()
        {
            Mvx.TryResolve(out _dialogService);
            Mvx.TryResolve(out _lookupService);

            _viewModels = new List<IStepViewModel>
            {
                new ClientDemographicViewModel(_dialogService) {Parent = this},
                new ClientContactViewModel (_dialogService){Parent = this},
                new ClientProfileViewModel(_dialogService,_lookupService) {Parent = this},
                new ClientEnrollmentViewModel(_dialogService,_lookupService) {Parent = this}
            };
            ShowStep(1);
        }
        public void ShowStep(int step)
        {
            var vm = ViewModels.FirstOrDefault(x => x.Step == step);

            
            if (null != vm)
            {
                switch (vm.Step)
                {
                    case 1:                        
                        ShowViewModel<ClientDemographicViewModel>();
                        break;
                    case 2:
                        ShowViewModel<ClientContactViewModel>();
                        break;
                    case 3:
                        ShowViewModel<ClientProfileViewModel>();
                        break;
                    case 4:
                        ShowViewModel<ClientEnrollmentViewModel>();
                        break;
                }
            }
        }
    }
}