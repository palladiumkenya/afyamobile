using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.Interfaces;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class ObsViewModel : MvxViewModel, IObsViewModel
    {
        private string _form;
        private string _currentAction;
        private bool _canStart;
        private bool _canStop;

        public string Form
        {
            get { return _form; }
            set
            {
                _form = value; 
                RaisePropertyChanged(()=>Form);
            }
        }

        public bool CanStart
        {
            get { return _canStart; }
            set { _canStart = value;RaisePropertyChanged(() => CanStart); }
        }

        public bool CanStop
        {
            get { return _canStop; }
            set { _canStop = value; RaisePropertyChanged(() => CanStop);}
        }

        public ICommand StartCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    CanStart = false;
                    CanStop = true;
                    CurrentAction = "Started...";
                });
            }
        }

        public ICommand StopCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    CanStart = true;
                   CanStop = false;
                    CurrentAction = "Stopped...";
                });
            }
        }

        public string CurrentAction
        {
            get { return _currentAction; }
            set { _currentAction = value;RaisePropertyChanged(() => CurrentAction); }
        }

        public void Init(string form)
        {
            Form = form;
            CanStart = CanStop = false;
        }


        public override void Start()
        {
            base.Start();
            CurrentAction = "Ready...";
        }

        public override void Appeared()
        {
            base.Appeared();
            CanStart = true;
             CanStop = false;
        }
    }
}