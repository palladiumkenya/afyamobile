using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.Interfaces;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class ObsViewModel : MvxViewModel, IObsViewModel
    {
        private string _form;
        private string _currentAction;
        private bool _canStart;
        private bool _canStop;
        private string _status;

        public string Form
        {
            get { return _form; }
            set
            {
                _form = value;
                RaisePropertyChanged(() => Form);
            }
        }

        public bool CanStart
        {
            get { return _canStart; }
            set
            {
                _canStart = value;
                RaisePropertyChanged(() => CanStart);
            }
        }

        public bool CanStop
        {
            get { return _canStop; }
            set
            {
                _canStop = value;
                RaisePropertyChanged(() => CanStop);
            }
        }

        public ICommand StartCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    CanStart = false;
                    CanStop = true;
                    CurrentAction = "Started... (Disable START)";
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
            set
            {
                _currentAction = value;
                RaisePropertyChanged(() => CurrentAction);
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                RaisePropertyChanged(() => Status);
            }
        }

        public void Init(string form)
        {
            Form = form;
            CanStart = true;
            CanStop = false;
            CurrentAction = "Im Ready!";
            Status = "started";
        }

        public class SavedState
        {
            public bool CanStart { get; set; }
            public bool CanStop { get; set; }
            public string CurrentAction { get; set; }

            public SavedState(bool canStart, bool canStop, string currentAction)
            {
                CanStart = canStart;
                CanStop = canStop;
                CurrentAction = currentAction;
            }
        }

        protected override void SaveStateToBundle(IMvxBundle bundle)
        {
            bundle.Data["obsstate"] = JsonConvert.SerializeObject(SaveState());
            base.SaveStateToBundle(bundle);
        }

        protected override void ReloadFromBundle(IMvxBundle state)
        {
            if (state.Data.ContainsKey("obsstate"))
            {
                var savedState = JsonConvert.DeserializeObject<SavedState>(state.Data["obsstate"]);
                Status = $"Restored | {DateTime.Now:HH:mm:ss tt zz}|";
                _canStart = savedState.CanStart;
                _canStop = savedState.CanStop;
                _currentAction = savedState.CurrentAction;
            }
            base.ReloadFromBundle(state);
        }

        public SavedState SaveState()
        {
            Status = $"Saved | {DateTime.Now:HH:mm:ss tt zz}|";
            return new SavedState(_canStart, _canStop, _currentAction);
        }
    }
}