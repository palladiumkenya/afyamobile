using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.ViewModel;
using LiveHTS.Presentation.ViewModel.Template;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Wrapper
{
    public interface IQuestionTemplateWrap
    {
        IClientEncounterViewModel Parent { get; }
        QuestionTemplate QuestionTemplate { get; }

        void MoveToNextQuestion(QuestionTemplate questionTemplate);
    }
}