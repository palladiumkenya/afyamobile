﻿using System;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Config
{
    public class EncounterType : Entity<Guid>
    {
        public string Name { get; set; }
    }
}