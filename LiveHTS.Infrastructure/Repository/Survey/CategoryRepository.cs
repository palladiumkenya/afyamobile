using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Lookup;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.SharedKernel.Custom;
using SQLite;

namespace LiveHTS.Infrastructure.Repository.Survey
{
    public class CategoryRepository : BaseRepository<Category,Guid>, ICategoryRepository
    {
        private readonly ILiveSetting _liveSetting;

        public CategoryRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
            _db.CreateTable<Item>();
            _db.CreateTable<CategoryItem>();
        }

        public IEnumerable<Category> GetAllWithItems(Guid? conceptCategoryId=null)
        {
            //cat

            var categories=new List<Category>();

            if (conceptCategoryId.IsNullOrEmpty())
            {
                categories = _db.Table<Category>().ToList();
            }
            else
            {
                categories = new List<Category> {Get(conceptCategoryId.Value)};
            }


            foreach (var category in categories)
            {
                try
                {
                    var categoryItems = _db.Table<CategoryItem>().Where(x => x.CategoryId == category.Id).ToList();
                    foreach (var categoryItem in categoryItems)
                    {
                        categoryItem.Item = _db.Find<Item>(categoryItem.ItemId);
                    }

                    if (categoryItems.Count > 0)
                        category.Items = categoryItems;
                }
                catch
                {
                    // ignored
                }
            }
            return categories;
        }
    }
}