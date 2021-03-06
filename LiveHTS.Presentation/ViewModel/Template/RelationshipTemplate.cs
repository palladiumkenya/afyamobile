﻿using System;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces.ViewModel.Template;

namespace LiveHTS.Presentation.ViewModel.Template
{
    public class RelationshipTemplate : IRelationshipTemplate
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }

        public RelationshipTemplate(ClientRelationship relationship)
        {
            Id = relationship.Id;
            FullName = relationship.Person.FullName;
            Gender = relationship.Person.Gender;
            BirthDate = relationship.Person.BirthDate;
        }
    }
}