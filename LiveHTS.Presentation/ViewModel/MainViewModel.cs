using System.Linq;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Interfaces.Repository.Survey;
using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Presentation.Interfaces;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class MainViewModel : MvxViewModel, IMainViewModel
    {
        private readonly IFormRepository _formRepository;
        private string _module;
        private string _title;

        public string Module
        {
            get { return _module; }
            set
            {
                _module = value;
                RaisePropertyChanged(() => Module);
            }
        }

        public MainViewModel(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }
        public override void Start()
        {
            var module =_formRepository.GetModule();
            Module = $"{module.Name} ({module.Description})";
            base.Start();
        }
    }
}