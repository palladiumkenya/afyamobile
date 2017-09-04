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
        
        private IMvxCommand _deleteTraceCommand;
        private IMvxCommand _editTraceCommand;

        public TraceTemplateWrap(IReferralViewModel parent, ITraceTemplate traceTemplate)
        {
            _parent = parent;
            _traceTemplate = traceTemplate;
        }

        public IReferralViewModel Parent
        {
            get { return _parent; }
        }

        public ITraceTemplate TraceTemplate
        {
            get
            {
                return _traceTemplate;
            }
        }

        public IMvxCommand EditTraceCommand
        {
            get
            {
                _editTraceCommand = _editTraceCommand ?? new MvxCommand(EditTrace);
                return _editTraceCommand;
            }
        }
        public IMvxCommand DeleteTraceCommand
        {
            get
            {
                _deleteTraceCommand = _deleteTraceCommand ?? new MvxCommand(DeleteTrace);
                return _deleteTraceCommand;
            }
        }

        private void EditTrace()
        {
            Parent.EditTrace(TraceTemplate.TraceResult);
        }

        private void DeleteTrace()
        {
            Parent.DeleteTrace(TraceTemplate.TraceResult);
        }


        
    }
}