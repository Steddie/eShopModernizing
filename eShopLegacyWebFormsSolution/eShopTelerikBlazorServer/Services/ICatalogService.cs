using System.Collections.Generic;
using eShopLegacyWebForms.Models;
using System;
using eShopLegacyWebForms.ViewModel;
using Telerik.DataSource;

namespace eShopLegacyWebForms.Services
{
    public interface ICatalogService : IDisposable
    {
        CatalogItem FindCatalogItem(int id);
        IEnumerable<CatalogBrand> GetCatalogBrands();
        Task<DataSourceResult> GetCatalogItemsPaginated(DataSourceRequest request);
        IEnumerable<CatalogType> GetCatalogTypes();
        void CreateCatalogItem(CatalogItem catalogItem);
        void UpdateCatalogItem(CatalogItem catalogItem);
        void RemoveCatalogItem(CatalogItem catalogItem);
    }
}