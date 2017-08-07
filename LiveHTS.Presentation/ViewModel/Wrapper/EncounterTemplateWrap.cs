using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.Presentation.ViewModel.Template;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel.Wrapper
{
    public class EncounterTemplateWrap : IEncounterTemplateWrap
    {
        private ClientDashboardViewModel _parent;
        private EncounterTemplate _encounterTemplate;
        
        private IMvxCommand _resumeEncounterCommand;
        private IMvxCommand _reviewEncounterCommand;
        private IMvxCommand _discardEncounterCommand;

        public EncounterTemplateWrap(ClientDashboardViewModel parent, EncounterTemplate encounterTemplate)
        {
            _parent = parent;
            _encounterTemplate = encounterTemplate;
        }

        public EncounterTemplate EncounterTemplate
        {
            get { return _encounterTemplate; }
        }    

        public IMvxCommand ResumeEncounterCommand
        {
            get
            {
                _resumeEncounterCommand = _resumeEncounterCommand ??
                                          new MvxCommand(ResumeEncounter, CanResumeEncounter);
                return _resumeEncounterCommand;
            }
        }

        public IMvxCommand ReviewEncounterCommand
        {
            get
            {
                _reviewEncounterCommand = _reviewEncounterCommand ??
                                          new MvxCommand(ReviewEncounter, CanReviewEncounter);
                return _reviewEncounterCommand;
            }
        }

        public IMvxCommand DiscardEncounterCommand
        {
            get
            {
                _discardEncounterCommand = _discardEncounterCommand ??
                                           new MvxCommand(DiscardEncounter, CanDiscardEncounter);
                return _discardEncounterCommand;
            }
        }

        private bool CanResumeEncounter()
        {
            throw new System.NotImplementedException();
        }

        private void ResumeEncounter()
        {
            _parent.ResumeEncounter(EncounterTemplate);
        }

        private bool CanReviewEncounter()
        {
            throw new System.NotImplementedException();
        }

        private void ReviewEncounter()
        {
            _parent.ReviewEncounter(EncounterTemplate);
        }

        private bool CanDiscardEncounter()
        {
            throw new System.NotImplementedException();
        }

        private void DiscardEncounter()
        {
            _parent.DiscardEncounter(EncounterTemplate);
        }
    }
}