using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SUBDCORE.Models;

namespace SUBDCORE.Repository
{
    public class Manufacturerepository : ICrudable<Manufacture>
    {
        SQLSpAdapter spAdapter;
        List<Manufacture> productions = new List<Manufacture>();
        public void Create(Manufacture _object)
        {
            spAdapter = new SQLSpAdapter("AddManufacture");
            spAdapter.AddSqlParameter("@Production", _object.Production);
            spAdapter.AddSqlParameter("@Quantity", _object.Quantity);
            spAdapter.AddSqlParameter("@Date", _object.Date);
            spAdapter.AddSqlParameter("@Employees", _object.Employees);
            spAdapter.ExecNonQuery();
        }

        public void Delete(int id)
        {
            spAdapter = new SQLSpAdapter("DelManufacture");
            spAdapter.AddSqlParameter("@idManufacture", id);
            spAdapter.ExecNonQuery();
        }

        public IEnumerable<Manufacture> GetList()
        {
            Manufacture pr;
            spAdapter = new SQLSpAdapter("GetManufacture");
            spAdapter.ExecReader();
            foreach (var row in spAdapter.baggage)
            {
                pr = new Manufacture();
                pr.ProductionNavigation = new FinishedProducts();
                pr.EmployeesNavigation = new Employees();
                pr.IdManufacture = Convert.ToInt16(row[0]);
                pr.Production = Convert.ToInt32(row[1]);
                pr.Quantity = Convert.ToDouble(row[2]);
                pr.Date = Convert.ToDateTime(row[3]);
                pr.Employees = Convert.ToInt16(row[4]);   
                pr.ProductionNavigation.IdFinishedProducts = Convert.ToInt32(row[5]);
                pr.ProductionNavigation.Names = row[6].ToString();
                pr.EmployeesNavigation.IdEmployees = Convert.ToInt16(row[7]);
                pr.EmployeesNavigation.FullName = row[8].ToString();
                productions.Add(pr);
            }
            return productions;
        }

        public void Update(Manufacture _object)
        {
            spAdapter = new SQLSpAdapter("UpManufacture");
            spAdapter.AddSqlParameter("@idManufacture", _object.IdManufacture);
            spAdapter.AddSqlParameter("@Production", _object.Production);
            spAdapter.AddSqlParameter("@Quantity", _object.Quantity);
            spAdapter.AddSqlParameter("@Date", _object.Date);
            spAdapter.AddSqlParameter("@Employees", _object.Employees);
            spAdapter.ExecNonQuery();
        }
    }
}
