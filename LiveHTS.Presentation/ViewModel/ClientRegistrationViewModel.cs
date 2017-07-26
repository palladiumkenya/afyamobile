using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.WeakSubscription;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientRegistrationViewModel:MvxViewModel,IClientRegistrationViewModel
    {
        private readonly List<IStepViewModel> _viewModels;

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
            _viewModels = new List<IStepViewModel>
            {
                new ClientDemographicViewModel {Parent = this},
                new ClientContactViewModel {Parent = this},
                new ClientProfileViewModel {Parent = this},
                new ClientEnrollmentViewModel {Parent = this}
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