using System;

namespace LiveHTS.Presentation.DTO
{
    public class ExpiryDateDTO
    {
        public Guid Id { get;  }
        public DateTime EventDate { get;  }

        public ExpiryDateDTO(Guid id, DateTime eventDate)
        {
            Id = id;
            EventDate = eventDate;
        }
    }
}