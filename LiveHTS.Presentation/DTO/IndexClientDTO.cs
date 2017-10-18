using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Presentation.DTO
{
    public class IndexClientDTO
    {
        public Guid Id { get; set; }
        public  string RelType { get; set; }

        public IndexClientDTO(Guid id, string relType)
        {
            Id = id;
            RelType = relType;
        }
    }
}