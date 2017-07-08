using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Survey;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model.Lookup
{
    public class Category : Entity<Guid>
    {
        public string Code { get; set; }
        public List<CategoryItem> Items { get; set; } = new List<CategoryItem>();
    }
}