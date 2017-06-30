﻿using System;
using System.Collections.Generic;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Survey
{
    public class Form:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Section> Sections { get; set; }
        public Guid ModuleId { get; set; }
    }
}