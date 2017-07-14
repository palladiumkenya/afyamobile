using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Lookup;

namespace LiveHTS.Core.Interfaces.Repository.Lookup
{
    public interface ICategoryRepository:IRepository<Category,Guid>
    {
        IEnumerable<Category> GetAllWithItems(Guid? conceptCategoryId=null);
    }
}