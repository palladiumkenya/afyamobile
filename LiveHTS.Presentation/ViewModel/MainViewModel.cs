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
    public class MainViewModel : MvxViewModel, IMainViewModel
    {
        private readonly IModuleRepository _moduleRepository;
        private Module _module;
        private string _title;
        private List<Form> _forms;
        private  Form _selectedForm;

        public Module Module
        {
            get { return _module; }
            set
            {
                _module = value;
                RaisePropertyChanged(() => Module);
            }
        }

        public Form SelectedForm
        {
            get { return _selectedForm; }
            set
            {
                _selectedForm = value;
                RaisePropertyChanged(() => SelectedForm);
            }
        }

       
        public ICommand ProceedCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    var formName = SelectedForm?.Name;
                    ShowViewModel<CounsellingViewModel>(new {form = formName});
                });
            }
        }

      
        public MainViewModel(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        public override void Start()
        {
            base.Start();
            Module = _moduleRepository.GetDefaultModule();
        }
    }
}