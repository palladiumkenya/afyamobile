using LiveHTS.Core.Model.Survey;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.Presentation.ViewModel.Template;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;

namespace LiveHTS.Presentation.ViewModel.Wrapper
{
    public class QuestionTemplateWrap: IQuestionTemplateWrap
    {
        private IClientEncounterViewModel _parent;
        private  QuestionTemplate _questionTemplate;
        

        public QuestionTemplateWrap(IClientEncounterViewModel parent,QuestionTemplate questionTemplate )
        {
            _parent = parent;
            _questionTemplate = questionTemplate;
            _questionTemplate.QuestionTemplateWrap = this;
        }

        public QuestionTemplate QuestionTemplate
        {
            get { return _questionTemplate; }
        }

        public void MoveToNextQuestion(QuestionTemplate questionTemplate)
        {
            _parent.AllowNext(questionTemplate);
        }
    }
}