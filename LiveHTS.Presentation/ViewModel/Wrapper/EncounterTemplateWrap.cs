using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.Presentation.ViewModel.Template;
using LiveHTS.SharedKernel.Custom;
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
        private readonly IEncounterViewModel _encounterViewModel;
        private bool _showResume;
        private bool _showReview;
        private bool _showDiscard;

        public EncounterTemplateWrap(ClientDashboardViewModel parent, EncounterTemplate encounterTemplate)
        {
            _parent = parent;
            _encounterTemplate = encounterTemplate;
        }

        public EncounterTemplateWrap(IEncounterViewModel encounterViewModel, EncounterTemplate encounterTemplate)
        {
            _encounterTemplate = encounterTemplate;
            _encounterViewModel = encounterViewModel;
        }

        public IEncounterViewModel EncounterViewModel
        {
            get { return _encounterViewModel; }
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

        public bool ShowResume
        {
            get { return _showResume; }
            set { _showResume = value; }
        }

        public bool ShowReview
        {
            get { return _showReview; }
            set { _showReview = value; }
        }

        public bool ShowDiscard
        {
            get { return _showDiscard; }
            set { _showDiscard = value; }
        }

        private bool CanResumeEncounter()
        {
            ShowResume= null != EncounterTemplate && EncounterTemplate.Status == "Started";
            return ShowResume;
        }

        private void ResumeEncounter()
        {
            _parent.ResumeEncounter(EncounterTemplate);
        }

        private bool CanReviewEncounter()
        {
            ShowReview= null != EncounterTemplate && EncounterTemplate.Status == "Completed";
            return ShowReview;
        }

        private void ReviewEncounter()
        {
            _parent.ReviewEncounter(EncounterTemplate);
        }

        private bool CanDiscardEncounter()
        {
            ShowDiscard =null != EncounterTemplate;
            return ShowDiscard;
        }

        private void DiscardEncounter()
        {
            _parent.DiscardEncounter(EncounterTemplate);
        }
    }
}