using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Lookup;

namespace LiveHTS.Core.Interfaces.Repository.Lookup
{
    public interface ILookupCategoryRepository:IRepository<Category,Guid>
    {
        IEnumerable<Category> GetAllWithItems();
    }
}