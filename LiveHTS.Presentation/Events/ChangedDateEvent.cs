using System;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Presentation.Events
{
    public class ChangedDateEvent:EventArgs
    {
        public Guid Id { get; set; }
        public  DateTime Date { get; set; }

        public ChangedDateEvent(Guid id, DateTime date)
        {
            Id = id;
            Date = date;
        }
    }

    public class PositionChangedEvent : EventArgs
    {
        public int Position { get; set; }


        public PositionChangedEvent(int position)
        {
            Position = position;
        }
    }
}