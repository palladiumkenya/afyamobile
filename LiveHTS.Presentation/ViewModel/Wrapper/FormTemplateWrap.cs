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

        public FormTemplate FormTemplate
        {
            get { return _formTemplate; }
        }

        public IMvxCommand StartEncounterCommand
        {
            get
            {
                _startEncounterCommand = _startEncounterCommand ?? new MvxCommand(StartEncounter);
                return _startEncounterCommand;
            }
        }

        private void StartEncounter()
        {
            _parent.StartEncounter(_formTemplate);
        }
    }
}