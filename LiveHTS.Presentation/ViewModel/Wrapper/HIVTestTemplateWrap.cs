using System;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.Presentation.ViewModel.Template;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel.Wrapper
{
    public class HIVTestTemplateWrap : IHIVTestTemplateWrap
    {
        private IClientHIVTestViewModel _parent;
        private IMvxCommand _saveTestCommand;
        private IMvxCommand _deleteTestCommand;
        private readonly IHIVTestTemplate _hivTestTemplate;
        private IMvxCommand _showDateDialogCommand;

        public HIVTestTemplateWrap(IClientHIVTestViewModel parent, IHIVTestTemplate formTemplate)
        {
            _parent = parent;
            _hivTestTemplate = formTemplate;
            _hivTestTemplate.HIVTestTemplateWrap = this;
        }

        public IClientHIVTestViewModel Parent
        {
            get { return _parent; }
        }
        public IHIVTestTemplate HIVTestTemplate
        {
            get { return _hivTestTemplate; }
        }
        public IMvxCommand SaveTestCommand
        {
            get
            {
                _saveTestCommand = _saveTestCommand ?? new MvxCommand(SaveTest, CanSaveTest);
                return _saveTestCommand;
            }
        }
        public IMvxCommand DeleteTestCommand
        {
            get
            {
                _deleteTestCommand = _deleteTestCommand ?? new MvxCommand(DeleteTest, CanDeleteTest);
                return _deleteTestCommand;
            }
        }
        public IMvxCommand ShowDateDialogCommand
        {
            get
            {
                _showDateDialogCommand = _showDateDialogCommand ?? new MvxCommand(ShowDateDialog);         
                return _showDateDialogCommand;
            }
        }
        private void ShowDateDialog()
        {

            Parent.ShowDatePicker(HIVTestTemplate.Id, HIVTestTemplate.Expiry);
        }
        private void Parent_DateSelected(object sender, System.EventArgs e)
        {
            HIVTestTemplate.LotNumber = $"{Parent.SelectedDate:F}";
            
        }
        private bool CanSaveTest()
        {
            return HIVTestTemplate.CanSave();
        }
        private void SaveTest()
        {
            if (HIVTestTemplate.Validate())
                Parent.SaveTest(HIVTestTemplate.TestResult);
        }
        private bool CanDeleteTest()
        {
            return HIVTestTemplate.CanDelete();
        }
        private void DeleteTest()
        {
            Parent.DeleteTest(HIVTestTemplate.TestResult);
        }
    }
}