using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel.Wrapper
{
    public class TraceTemplateWrap: ITraceTemplateWrap
    {
        private IReferralViewModel _parent;
        private ITraceTemplate _traceTemplate;
        private IMvxCommand _saveTraceCommand;
        private IMvxCommand _deleteTraceCommand;
        private IMvxCommand _showDateDialogCommand;

        public IReferralViewModel Parent
        {
            get { return _parent; }
        }

        public ITraceTemplate TraceTemplate
        {
            get { return _traceTemplate; }
        }

        public IMvxCommand SaveTraceCommand
        {
            get
            {
                _saveTraceCommand = _saveTraceCommand ?? new MvxCommand(SaveTrace, CanSaveTrace);
                return _saveTraceCommand;
            }
        }

        private bool CanSaveTrace()
        {
            return TraceTemplate.CanSave();
        }

        private void SaveTrace()
        {
            if (TraceTemplate.Validate())
            {
                
                 Parent.SaveTrace(TraceTemplate.TraceResult);
            }

        }

        public IMvxCommand DeleteTraceCommand
        {
            get
            {
                _deleteTraceCommand = _deleteTraceCommand ?? new MvxCommand(DeleteTrace, CanDeleteTrace);
                return _deleteTraceCommand;
            }
        }

        private bool CanDeleteTrace()
        {
            return TraceTemplate.CanDelete();
        }

        private void DeleteTrace()
        {
            //Parent.DeleteTrace(TraceTemplate.TraceResult);
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

            //Parent.ShowDatePicker(TraceTemplate.Id,TraceTemplate.Date);
        }

        public TraceTemplateWrap(IReferralViewModel parent, ITraceTemplate traceTemplate)
        {
            _parent = parent;
            _traceTemplate = traceTemplate;
            _traceTemplate.TraceTemplateWrap = this;
        }
    }
}