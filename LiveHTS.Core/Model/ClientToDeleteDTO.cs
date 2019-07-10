﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Config;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Model
{
    public  class ClientToDeleteDTO
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public List<EnconterToDeleteDTO> EnconterToDeleteDtos { get; set; }=new List<EnconterToDeleteDTO>();
        public List<Guid> Relations { get; set; }=new List<Guid>();

        public bool NotYet { get; set; }

        public ClientToDeleteDTO()
        {
        }

        public ClientToDeleteDTO(Guid id, Guid personId)
        {
            Id = id;
            PersonId = personId;
        }

        public void AddEnounter(EnconterToDeleteDTO enconterToDeleteDto)
        {
            EnconterToDeleteDtos.Add(enconterToDeleteDto);
        }


        public void AddRelations(Guid id)
        {
            Relations.Add(id);
        }

        public bool NotSent
        {
            get { return Id.IsNullOrEmpty() && PersonId.IsNullOrEmpty(); }
        }
    }
}
