using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;
using LiveHTS.Presentation.Interfaces.ViewModel.Wrapper;
using LiveHTS.Presentation.Validations;
using LiveHTS.SharedKernel.Custom;
using MvvmCross.Core.ViewModels;
using MvvmValidation;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class FamilyTraceTemplate : MvxNotifyPropertyChanged, IFamilyTraceTemplate
    {

        public ObsFamilyTraceResult TraceResult { get; set; }
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid Mode { get; set; }
        public string ModeDisplay { get; set; }
        public Guid Outcome { get; set; }
        public string OutcomeDisplay { get; set; }
        public Guid? Consent { get; set; }
        public string ConsentDisplay { get; set; }
        public DateTime? Reminder { get; set; }
        public DateTime? BookingDate { get; set; }
        public Guid EncounterId { get; set; }

        public FamilyTraceTemplate(ObsFamilyTraceResult testResult, List<CategoryItem> modes, List<CategoryItem> outcomes, List<CategoryItem> consents)
        {
            TraceResult = testResult;

            if (null != modes && modes.Count > 0)
            {
                var kit = modes.FirstOrDefault(x => x.ItemId == testResult.Mode);
                if (null != kit)
                {
                    ModeDisplay = kit.Display;
                }
            }

            if (null != outcomes && outcomes.Count > 0)
            {
                var result = outcomes.FirstOrDefault(x => x.ItemId == testResult.Outcome);
                if (null != result)
                {
                    OutcomeDisplay = result.Display;

                }
            }

            if (null != consents && consents.Count > 0)
            {
                var result = consents.FirstOrDefault(x => x.ItemId == testResult.Consent);
                if (null != result)
                {
                    ConsentDisplay = result.Display;

                }
            }

            Id = testResult.Id;
            Date = testResult.Date;
            Mode = testResult.Mode;
            Outcome = testResult.Outcome;
            EncounterId = testResult.EncounterId;
            Consent = testResult.Consent;
            Reminder = testResult.Reminder;
            BookingDate = testResult.BookingDate;

        }

       
    }
}