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

        public FormTemplateWrap(ClientDashboardViewModel parent, FormTemplate formTemplate)
        {
            _parent = parent;
            _formTemplate = formTemplate;
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

        private bool CanStartEncounter()
        {
            return !_formTemplate.HasEncounters;
        }

        private void StartEncounter()
        {
            _parent.StartEncounter(_formTemplate);
        }
    }
}