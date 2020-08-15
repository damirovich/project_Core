using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SUBDCORE.Models;

namespace SUBDCORE.Repository
{
    public class PurchaseOfCheeserepository : ICrudable<PurchaseOfCheese>
    {
        SQLSpAdapter spAdapter;
        List<PurchaseOfCheese>  purchaseOfCheeses = new List<PurchaseOfCheese>();
        public void Create(PurchaseOfCheese _object)
        {
            spAdapter = new SQLSpAdapter("AddPurchaseOfCheese");
            spAdapter.AddSqlParameter("@Rawmaterials", _object.RawMaterials);
            spAdapter.AddSqlParameter("@Quantity", _object.Quantity);
            spAdapter.AddSqlParameter("@Summ", _object.Summ);
            spAdapter.AddSqlParameter("@Date", _object.Date);
            spAdapter.AddSqlParameter("@Employees", _object.Employees);
            spAdapter.ExecNonQuery();
        }

        public void Delete(int id)
        {
            spAdapter = new SQLSpAdapter("DelPurchaseOfCheese");
            spAdapter.AddSqlParameter("@idPurchaseOfCheese", id);
            spAdapter.ExecNonQuery();
        }

        public IEnumerable<PurchaseOfCheese> GetList()
        {
            PurchaseOfCheese pm;
            spAdapter = new SQLSpAdapter("GetPurchaseOfCheese");
            spAdapter.ExecReader();
            foreach (var row in spAdapter.baggage)
            {
                pm = new PurchaseOfCheese();
                pm.RawMaterialsNavigation = new RawMaterials();
                pm.EmployeesNavigation = new Employees();
                pm.IdPurchaseOfcheese = Convert.ToInt32(row[0]);
                pm.RawMaterials = Convert.ToInt16(row[1]);
                pm.Quantity = Convert.ToDouble(row[2]);
                pm.Summ = Convert.ToDecimal(row[3]);
                pm.Date = Convert.ToDateTime(row[4]);
                pm.Employees = Convert.ToInt16(row[5]);
                pm.RawMaterialsNavigation.IdRawMaterials = Convert.ToInt16(row[6]);
                pm.RawMaterialsNavigation.Names = row[7].ToString();
                pm.EmployeesNavigation.IdEmployees = Convert.ToInt16(row[8]);
                pm.EmployeesNavigation.FullName = row[9].ToString();
                purchaseOfCheeses.Add(pm);
            }
            return purchaseOfCheeses;
        }

        public void Update(PurchaseOfCheese _object)
        {
            spAdapter = new SQLSpAdapter("UpPurchaseOfCheese");
            spAdapter.AddSqlParameter("@idPurchaseOfCheese", _object.IdPurchaseOfcheese);
            spAdapter.AddSqlParameter("@Rawmaterials", _object.RawMaterials);
            spAdapter.AddSqlParameter("@Quantity", _object.Quantity);
            spAdapter.AddSqlParameter("@Summ", _object.Summ);
            spAdapter.AddSqlParameter("@Date", _object.Date);
            spAdapter.AddSqlParameter("@Employees", _object.Employees);
            spAdapter.ExecNonQuery();
        }
    }
}
