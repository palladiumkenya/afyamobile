using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model;
using LiveHTS.Presentation.Interfaces;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class ActivateViewModel : MvxViewModel, IActivateViewModel
    {
        private readonly IPracticeTypeRepository _practiceTypeRepository;
        private List<PracticeType> _practiceTypes;
        private PracticeType _selectedPracticeType;
        private string _code;
        private string _name;

        public List<PracticeType> PracticeTypes
        {
            get { return _practiceTypes; }
            set
            {
                _practiceTypes = value;
                RaisePropertyChanged(() => PracticeTypes);
            }
        }

        public PracticeType SelectedPracticeType
        {
            get { return _selectedPracticeType; }
            set { _selectedPracticeType = value; }
        }

        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                RaisePropertyChanged(() => Code);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public MvxCommand ActivateCommand { get; set; }

        public ActivateViewModel(IPracticeTypeRepository practiceTypeRepository)
        {
            _practiceTypeRepository = practiceTypeRepository;
        }

        public override void Start()
        {
            base.Start();
            PracticeTypes = _practiceTypeRepository.GetAll().ToList();
        }
    }
}