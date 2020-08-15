using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SUBDCORE.Models;
namespace SUBDCORE.Repository
{
    public class ProductSalesrepository : ICrudable<ProductSales>
    {
        SQLSpAdapter spAdapter;
        List<ProductSales> productSales = new List<ProductSales>();
        public void Create(ProductSales _object)
        {
            spAdapter = new SQLSpAdapter("AddProductSales");
            spAdapter.AddSqlParameter("@FinishedProduct", _object.FinishedProduct);
            spAdapter.AddSqlParameter("@Quantity", _object.Quantity);
            spAdapter.AddSqlParameter("@Date", _object.Date);
            spAdapter.AddSqlParameter("@Employees", _object.Employees);
            spAdapter.ExecNonQuery();
        }

        public void Delete(int id)
        {
            spAdapter = new SQLSpAdapter("DelProductSales");
            spAdapter.AddSqlParameter("@idProductSales", id);
            spAdapter.ExecNonQuery();
        }

        public IEnumerable<ProductSales> GetList()
        {
            ProductSales ps;
            spAdapter = new SQLSpAdapter("GetProductSales");
            spAdapter.ExecReader();
            foreach (var row in spAdapter.baggage)
            {
                ps = new ProductSales();
                ps.FinishedProductNavigation = new FinishedProducts();
                ps.EmployeesNavigation = new Employees();
                ps.IdProductSales = Convert.ToInt32(row[0]);
                ps.FinishedProduct = Convert.ToByte(row[1]);
                ps.Quantity = Convert.ToDouble(row[2]);
                ps.Summ = Convert.ToDecimal(row[3]);
                ps.Date = Convert.ToDateTime(row[4]);
                ps.Employees = Convert.ToInt16(row[5]);
                ps.FinishedProductNavigation.IdFinishedProducts = Convert.ToByte(row[6]);
                ps.FinishedProductNavigation.Names = row[7].ToString();
                ps.EmployeesNavigation.IdEmployees = Convert.ToInt16(row[8]);
                ps.EmployeesNavigation.FullName = row[9].ToString();
                productSales.Add(ps);
            }
            return productSales;
        }

        public void Update(ProductSales _object)
        {
            spAdapter = new SQLSpAdapter("UpProductSales");
            spAdapter.AddSqlParameter("@idProductSales", _object.IdProductSales);
            spAdapter.AddSqlParameter("@FinishedProduct", _object.FinishedProduct);
            spAdapter.AddSqlParameter("@Quantity", _object.Quantity);
            spAdapter.AddSqlParameter("@Summ", _object.Summ);
            spAdapter.AddSqlParameter("@Date", _object.Date);
            spAdapter.AddSqlParameter("@Employees", _object.Employees);
            spAdapter.ExecNonQuery();
        }
    }
}
