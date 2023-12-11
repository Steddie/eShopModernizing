using System.Collections.Generic;

using eShopTelerikAdminBlazorServer.Models.Employee;
using eShopTelerikAdminBlazorServer.Models.Sales;

namespace eShopTelerikAdminBlazorServer.Data
{
    public interface IDataService
    {
        List<Employee> GetEmployees();
        IEnumerable<Sale> GetSales();
        List<Team> GetTeams();
    }
}
