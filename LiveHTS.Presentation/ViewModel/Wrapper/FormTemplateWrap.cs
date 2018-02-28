using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.Presentation.ViewModel.Template;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel.Wrapper
{
    public class FormTemplateWrap:IFormTemplateWrap
    {
        private ClientDashboardViewModel _parent;
        private  FormTemplate _formTemplate;
        private  IMvxCommand _startEncounterCommand;
        private readonly IEncounterViewModel _encounterViewModel;
        

        public FormTemplateWrap(ClientDashboardViewModel parent, FormTemplate formTemplate)
        {
            _parent = parent;
            _formTemplate = formTemplate;
        }
        public FormTemplateWrap(IEncounterViewModel encounterViewModel, FormTemplate formTemplate)
        {
            _formTemplate = formTemplate;
            _encounterViewModel = encounterViewModel;
        }

        public IEncounterViewModel EncounterViewModel
        {
            get { return _encounterViewModel; }
        }

        public FormTemplate FormTemplate
        {
            get { return _formTemplate; }
        }

        public IMvxCommand StartEncounterCommand
        {
            get
            {
                _startEncounterCommand = _startEncounterCommand ?? new MvxCommand(StartEncounter, CanStartEncounter);
                return _startEncounterCommand;
            }
        }

        public bool ShowStart { get; set; }


        private bool CanStartEncounter()
        {
           // return !_formTemplate.HasEncounters&&_formTemplate.HasConsent;

//            if (_formTemplate.Display.Contains("Lab") && _formTemplate.Block)
//                return false;

            if (_formTemplate.ConsentRequired)
            {
                ShowStart = _formTemplate.HasConsent && !_formTemplate.HasEncounters;
            }
            else
            {
                ShowStart = !_formTemplate.HasEncounters;
            }
            return ShowStart;
        }
        

        private void StartEncounter()
        {

            if (null != _parent)
            {
                _parent.StartEncounter(_formTemplate);
            }
            if (null != _encounterViewModel)
            {
                _encounterViewModel.StartEncounter(_formTemplate);
            }
            
        }
    }
}