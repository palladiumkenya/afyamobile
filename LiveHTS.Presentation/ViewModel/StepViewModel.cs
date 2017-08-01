﻿using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Validations;
using MvvmCross.Core.ViewModels;
using MvvmValidation;

namespace LiveHTS.Presentation.ViewModel
{
    public abstract class StepViewModel : MvxViewModel, IStepViewModel
    {
        protected readonly IDialogService _dialogService;
        private string _title;
        private string _description;
        private string _moveNextLabel;
        private string _movePreviousLabel;
        private IMvxCommand _moveNextCommand;
        private IMvxCommand _movePreviousCommand;
        private ObservableDictionary<string, string> _errors;

        public virtual IClientRegistrationViewModel Parent { get; set; }

        public virtual int Step { get; set; }
        public virtual string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(() => Title); }
        }
        public virtual string Description
        {
            get { return _description; }
            set { _description = value; RaisePropertyChanged(() => Description); }
        }

        public virtual string MoveNextLabel
        {
            get { return _moveNextLabel; }
            set { _moveNextLabel = value; RaisePropertyChanged(() => MoveNextLabel); }
        }
        public virtual string MovePreviousLabel
        {
            get { return _movePreviousLabel; }
            set { _movePreviousLabel = value; RaisePropertyChanged(() => MovePreviousLabel); }
        }

        public virtual ValidationHelper Validator { get; }
        public virtual ObservableDictionary<string, string> Errors
        {
            get { return _errors; }
            set { _errors = value; RaisePropertyChanged(() => Errors); }
        }

        public virtual IMvxCommand MoveNextCommand
        {
            get
            {
                _moveNextCommand = _moveNextCommand ?? new MvxCommand(MoveNext, CanMoveNext);
                return _moveNextCommand;
            }
        }
        public virtual IMvxCommand MovePreviousCommand
        {
            get
            {
                _movePreviousCommand = _movePreviousCommand ?? new MvxCommand(MovePrevious, CanMovePrevious);
                return _movePreviousCommand;
            }
        }

        protected StepViewModel():this(null)
        {
        }

        protected StepViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            Validator = new ValidationHelper();
        }

        public virtual bool Validate()
        {
            var result = Validator.ValidateAll();
            Errors = result.AsObservableDictionary();
            return result.IsValid;
        }

        public virtual void MoveNext()
        {
            
        }
        public virtual void MovePrevious()
        {
            
        }
        public virtual bool CanMoveNext()
        {
            return false;
        }
        public virtual bool CanMovePrevious()
        {
            return false;
        }
       
        public virtual void Save()
        {
        }
    }
}