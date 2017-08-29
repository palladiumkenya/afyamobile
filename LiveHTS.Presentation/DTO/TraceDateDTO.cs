using System;

namespace LiveHTS.Presentation.DTO
{
    public class TraceDateDTO
    {
        public Guid Id { get;  }
        public DateTime EventDate { get;  }

        public TraceDateDTO(Guid id, DateTime eventDate)
        {
            Id = id;
            EventDate = eventDate;
        }
    }
}