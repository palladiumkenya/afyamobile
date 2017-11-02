using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.Presentation.ViewModel.Template;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel.Wrapper
{
    public class ModuleTemplateWrap : IModuleTemplateWrap
    {
        private ModuleTemplate _moduleTemplate;
        private readonly IEncounterViewModel _encounterViewModel;

        public ModuleTemplateWrap(IEncounterViewModel encounterViewModel,ModuleTemplate moduleTemplate )
        {
            _moduleTemplate = moduleTemplate;
            _encounterViewModel = encounterViewModel;
        }
        public IEncounterViewModel EncounterViewModel
        {
            get { return _encounterViewModel; }
        }

        public ModuleTemplate ModuleTemplate
        {
            get { return _moduleTemplate; }
        }
    }
}