using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Core.Model
{
    public  class EnconterToDeleteDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public EnconterToDeleteDTO(Guid id,EncounterType encounterType)
        {
            Id = id;
            Name = encounterType?.Name;
        }
    }
}
