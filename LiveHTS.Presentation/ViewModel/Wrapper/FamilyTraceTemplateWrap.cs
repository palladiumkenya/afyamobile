﻿using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel.Wrapper
{
    public class FamilyTraceTemplateWrap : IFamilyTraceTemplateWrap
    {
        private IFamilyTraceTemplate _traceTemplate;
        private IMvxCommand _deleteTraceCommand;
        private IMvxCommand _editTraceCommand;
        private IMemberTracingViewModel _parent;

        public FamilyTraceTemplateWrap(IMemberTracingViewModel parent,IFamilyTraceTemplate traceTemplate)
        {
            _traceTemplate = traceTemplate;
            _parent = parent;
        }

        public IMemberTracingViewModel Parent
        {
            get { return _parent; }
        }

        public IFamilyTraceTemplate TraceTemplate
        {
            get { return _traceTemplate; }
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