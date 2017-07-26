using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace LiveHTS.Presentation.ViewModel
{
    public class ClientRegistrationViewModel:MvxViewModel,IClientRegistrationViewModel
    {
        private readonly IDialogService _dialogService;

        private  IMvxCommand _moveNextCommand;
        private  IMvxCommand _movePreviousCommand;
        private List<IStepViewModel> _viewModels;
        private  int _currentStep;
        private  string _moveNextLabel, _movePreviousLabel;

        public int CurrentStep
        {
            get { return _currentStep;}
            private set
            {
                _currentStep = value;
                MoveNextCommand.RaiseCanExecuteChanged();
                MovePreviousCommand.RaiseCanExecuteChanged();
            }
        }
        public string MovePreviousLabel
        {
            get { return _movePreviousLabel; }
            private set
            {
                _movePreviousLabel = value;
                RaisePropertyChanged(() => MovePreviousLabel);
            }
        }
        public string MoveNextLabel
        {
            get { return _moveNextLabel; }
            private set
            {
                _moveNextLabel = value;
                RaisePropertyChanged(() => MoveNextLabel);
            }
        }

        public  IEnumerable<IStepViewModel> ViewModels
        {
            get { return _viewModels.OrderBy(x=>x.Step); }
        }

        public IMvxCommand MoveNextCommand
        {
            get
            {
                _moveNextCommand = _moveNextCommand ?? new MvxCommand(MoveNext, CanMoveNext);
                return _moveNextCommand;
            }
        }

      
        public IMvxCommand MovePreviousCommand

        {
            get
            {
                _movePreviousCommand = _movePreviousCommand ?? new MvxCommand(MovePrevious, CanMovePrevious);
                return _movePreviousCommand;
            }
        }
     
        public ClientRegistrationViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            _viewModels = new List<IStepViewModel>
            {
                Mvx.Resolve<IClientDemographicViewModel>(),
                Mvx.Resolve<IClientContactViewModel>(),
                Mvx.Resolve<IClientProfileViewModel>(),
                Mvx.Resolve<IClientEnrollmentViewModel>()
            };
            MovePreviousLabel = "PREV";
            MoveNextLabel = "NEXT";
            CurrentStep = 1;
            ShowStep(_currentStep);
        }

        private void MoveNext()
        {
            var vmCurrent = ViewModels.FirstOrDefault(x => x.Step == _currentStep);

            if(!vmCurrent.Validate())
                return;

            if (CurrentStep == _viewModels.Count)
            {
                //Save
                _dialogService.Alert($"Save Successful", "Registration Complete", "OK");
                return;
            }

            CurrentStep++;

            ShowStep(_currentStep);

            
        }
        private void MovePrevious()
        {
            CurrentStep--;

            ShowStep(_currentStep);

            
        }

        private bool CanMoveNext()
        {
            return CurrentStep < _viewModels.Count + 1;
        }
        private bool CanMovePrevious()
        {
            return CurrentStep > 1;
        }

        private void ShowStep(int step)
        {
            MoveNextLabel = CurrentStep == _viewModels.Count ? "SAVE" : "NEXT";
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