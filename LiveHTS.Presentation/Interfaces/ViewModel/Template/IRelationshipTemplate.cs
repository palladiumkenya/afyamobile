﻿using System;

namespace LiveHTS.Presentation.Interfaces.ViewModel.Template
{
    public interface IRelationshipTemplate
    {
        Guid Id { get; set; }
        string FullName { get; set; }
        string Gender { get; set; }
        DateTime BirthDate { get; set; }
    }
}