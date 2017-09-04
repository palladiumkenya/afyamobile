using System;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Presentation.Events
{
    public class DateChangedEvent:EventArgs
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public DateChangedEvent(Guid id)
        {
            Id = id;
            Date=DateTime.Now;
        }

        public DateChangedEvent(Guid id, DateTime date)
        {
            Id = id;
            Date = date;
        }
    }
}