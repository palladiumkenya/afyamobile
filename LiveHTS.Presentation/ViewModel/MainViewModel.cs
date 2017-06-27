using System.Collections.Generic;
using System.Linq;
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
        private readonly IFormRepository _formRepository;
        private string _module;
        private string _title;
        private List<Form> _forms;
        private  Form _selectedForm;

        public string Module
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



        public List<Form> Forms
        {
            get { return _forms; }
            set
            {
                _forms = value;
                RaisePropertyChanged(() => Forms);
            }
        }

        public MainViewModel(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public override void Start()
        {
            var module = _formRepository.GetModule();
            Module = $"{module.Name} ({module.Description})";
            Forms = _formRepository.GetAll().Select(x => new Form() {Name = x.Name, Id = x.Id}).ToList();

            base.Start();
        }
    }
}