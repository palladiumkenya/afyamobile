using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.Presentation.ViewModel.Template;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel.Wrapper
{
    public class 
        TestTemplateWrap : ITestTemplateWrap
    {
        private readonly ITestEpisodeViewModel _episodeViewModel;
        private readonly ITestTemplate _testTemplate;
        private IMvxCommand _editTestCommand;
        private IMvxCommand _removeTestCommand;

        public TestTemplateWrap(ITestEpisodeViewModel episodeViewModel, ITestTemplate testTemplate)
        {
            _episodeViewModel = episodeViewModel;
            _testTemplate = testTemplate;
        }

        public ITestEpisodeViewModel EpisodeViewModel
        {
            get { return _episodeViewModel; }
        }

        public ITestTemplate TestTemplate
        {
            get { return _testTemplate; }
        }

        public IMvxCommand EditTestCommand
        {
            get
            {
                _editTestCommand = _editTestCommand ?? new MvxCommand(EditTest);
                return _editTestCommand;
            }
        }
        public IMvxCommand RemoveTestCommand
        {
            get
            {
                _removeTestCommand = _removeTestCommand ?? new MvxCommand(RemoveTest);
                return _removeTestCommand;
            }
        }

        private void RemoveTest()
        {
            EpisodeViewModel.DeleteTest(TestTemplate.TestResult);
        }

        private void EditTest()
        {
            EpisodeViewModel.EditTest(TestTemplate.TestResult);
        }
    }
}