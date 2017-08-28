using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Wrapper
{
    public interface IEncounterTemplateWrap
    {
        IEncounterViewModel EncounterViewModel { get; }

        EncounterTemplate EncounterTemplate { get; }
        IMvxCommand ResumeEncounterCommand { get; }
        IMvxCommand ReviewEncounterCommand { get; }
        IMvxCommand DiscardEncounterCommand { get; }

        bool ShowResume { get; set; }
        bool ShowReview { get; set; }
        bool ShowDiscard { get; set; }
    }
}