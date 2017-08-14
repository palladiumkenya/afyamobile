using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.Presentation.ViewModel.Template;

namespace LiveHTS.Presentation.ViewModel.Wrapper
{
    public class QuestionTemplateWrap: IQuestionTemplateWrap
    {
        private ClientEncounterViewModel _parent;
        private  QuestionTemplate _questionTemplate;

        public QuestionTemplateWrap(ClientEncounterViewModel parent,QuestionTemplate questionTemplate )
        {
            _parent = parent;
            _questionTemplate = questionTemplate;
        }

        public QuestionTemplate QuestionTemplate
        {
            get { return _questionTemplate; }
        }
    }
}