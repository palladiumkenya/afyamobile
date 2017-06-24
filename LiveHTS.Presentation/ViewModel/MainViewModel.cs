using System.Linq;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Interfaces.Services;
using LiveHTS.Presentation.Interfaces;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class MainViewModel : MvxViewModel, IMainViewModel
    {
        private readonly IActivationService _activationService;
        private readonly IPracticeTypeRepository _practiceTypeRepository;
        private string _activationCode;
        private int _parcticesTypeCount;


        public MainViewModel(IActivationService activationService, IPracticeTypeRepository practiceTypeRepository)
        {
            _activationService = activationService;
            _practiceTypeRepository = practiceTypeRepository;
        }

        public string ActivationCode
        {
            get { return _activationCode; }
            set
            {
                _activationCode = value; 
                RaisePropertyChanged(()=> ActivationCode);
            }
        }

        public int ParcticesTypeCount
        {
            get { return _parcticesTypeCount; }
            set
            {
                _parcticesTypeCount = value;
                RaisePropertyChanged(()=> ParcticesTypeCount);
            }
        }

        public override void Start()
        {
            ActivationCode = _activationService.IsActive() ? "XYZ" : "Not Activated!";
            ParcticesTypeCount = _practiceTypeRepository.GetAll().ToList().Count;
            base.Start();
        }
    }
}