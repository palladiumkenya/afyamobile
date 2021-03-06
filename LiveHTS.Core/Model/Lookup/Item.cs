﻿using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Lookup
{
    public class Item : Entity<Guid>
    {
        public string Code { get; set; }
        public string Display { get; set; }

        public Item()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}