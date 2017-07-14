using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Lookup
{
    public class Category : Entity<Guid>
    {
        public string Code { get; set; }
        [Ignore]
        public List<CategoryItem> Items { get; set; } = new List<CategoryItem>();

        public Category()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return Code;
        }
    }
}