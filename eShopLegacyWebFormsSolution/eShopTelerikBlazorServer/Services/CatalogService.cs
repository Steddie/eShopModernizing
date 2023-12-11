using System;
using eShopLegacyWebForms.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using eShopLegacyWebForms.ViewModel;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;

namespace eShopLegacyWebForms.Services
{
    public class CatalogService : ICatalogService
    {
        private CatalogDBContext db;
        private CatalogItemHiLoGenerator indexGenerator;

        public CatalogService(CatalogDBContext db, CatalogItemHiLoGenerator indexGenerator)
        {
            this.db = db;
            this.indexGenerator = indexGenerator;
        }

        public Task<DataSourceResult> GetCatalogItemsPaginated(DataSourceRequest request)
        {
            return db.CatalogItems
                .Include(c => c.CatalogBrand)
                .Include(c => c.CatalogType)
                .ToDataSourceResultAsync(request);
        }


        public CatalogItem FindCatalogItem(int id)
        {
            return db.CatalogItems.Include(c => c.CatalogBrand).Include(c => c.CatalogType).FirstOrDefault(ci => ci.Id == id);
        }
        public IEnumerable<CatalogType> GetCatalogTypes()
        {
            return db.CatalogTypes.ToList();
        }

        public IEnumerable<CatalogBrand> GetCatalogBrands()
        {
            return db.CatalogBrands.ToList();
        }

        public void CreateCatalogItem(CatalogItem catalogItem)
        {
            catalogItem.Id = indexGenerator.GetNextSequenceValue(db);
            db.CatalogItems.Add(catalogItem);
            db.SaveChanges();
        }

        public void UpdateCatalogItem(CatalogItem catalogItem)
        {
            // reattach entity
            var item = db.CatalogItems.Find(catalogItem.Id); 
            // update entity 
            db.Entry(item).CurrentValues.SetValues(catalogItem);
            // Set the item's state to modified 
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveCatalogItem(CatalogItem catalogItem)
        {
            db.CatalogItems.Remove(catalogItem);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}