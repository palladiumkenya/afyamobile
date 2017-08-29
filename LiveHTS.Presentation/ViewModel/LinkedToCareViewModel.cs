using System;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class LinkedToCareViewModel:MvxViewModel, ILinkedToCareViewModel
    {
        private string _title = "LINKAGE";
        private string _facilityHandedTo;
        private string _handedTo;
        private string _workerCarde;
        private DateTime? _dateEnrolled;
        private string _enrollmentId;
        private string _remarks;
        private ILinkageViewModel _parentViewModel;

        public ILinkageViewModel ParentViewModel
        {
            get { return _parentViewModel; }
            set { _parentViewModel = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }


        public string FacilityHandedTo
        {
            get { return _facilityHandedTo; }
            set { _facilityHandedTo = value; RaisePropertyChanged(() => FacilityHandedTo); }
        }

        public string HandedTo
        {
            get { return _handedTo; }
            set { _handedTo = value; RaisePropertyChanged(() => HandedTo); }
        }

        public string WorkerCarde
        {
            get { return _workerCarde; }
            set { _workerCarde = value; RaisePropertyChanged(() => WorkerCarde); }
        }

        public DateTime? DateEnrolled
        {
            get { return _dateEnrolled; }
            set { _dateEnrolled = value; RaisePropertyChanged(() => DateEnrolled); }
        }

        public string EnrollmentId
        {
            get { return _enrollmentId; }
            set { _enrollmentId = value; RaisePropertyChanged(() => EnrollmentId); }
        }

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; RaisePropertyChanged(() => Remarks); }
        }

    }
}