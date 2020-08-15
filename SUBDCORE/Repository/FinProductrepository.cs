using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SUBDCORE.Models;
namespace SUBDCORE.Repository
{
    public class FinProductrepository : ICrudable<FinishedProducts>
    {
        SQLSpAdapter spAdapter;
        List<FinishedProducts> finishedProducts = new List<FinishedProducts>();
        public object UnitNavigation { get; private set; }

        public void Create(FinishedProducts _object)
        {
            spAdapter = new SQLSpAdapter("AddFinishedProducts");
            spAdapter.AddSqlParameter("@Names", _object.Names);
            spAdapter.AddSqlParameter("@UnitOfMeasure", _object.UnitOfMeasure);
            spAdapter.AddSqlParameter("@Summ", _object.Summ);
            spAdapter.AddSqlParameter("@Quantity", _object.Quantity);
            spAdapter.ExecNonQuery();
        }

        public void Delete(int id)
        {
            spAdapter = new SQLSpAdapter("DelFinishedProducts");
            spAdapter.AddSqlParameter("@idfinishedProducts", id);
            spAdapter.ExecNonQuery();
        }

        public IEnumerable<FinishedProducts> GetList()
        {
            FinishedProducts fp;
            spAdapter = new SQLSpAdapter("GetFinishedProducts");
            spAdapter.ExecReader();
            foreach (var row in spAdapter.baggage)
            {
                fp = new FinishedProducts();
                fp.UnitOfMeasureNavigation = new UnitOfMeasure();
                fp.IdFinishedProducts = Convert.ToInt32(row[0]);
                fp.Names = row[1].ToString();
                fp.Quantity = Convert.ToDouble(row[2]);
                fp.Summ = Convert.ToDecimal(row[3]);
                fp.UnitOfMeasure = Convert.ToByte(row[4]);
                fp.UnitOfMeasureNavigation.IdUnitOfmeasure = Convert.ToByte(row[5]);
                fp.UnitOfMeasureNavigation.Names = row[6].ToString();
                finishedProducts.Add(fp);
            }
            return finishedProducts;
        }

        public void Update(FinishedProducts _object)
        {
            spAdapter = new SQLSpAdapter("UpFinishedProducts");
            spAdapter.AddSqlParameter("@idfinishedProducts", _object.IdFinishedProducts);
            spAdapter.AddSqlParameter("@Names", _object.Names);
            spAdapter.AddSqlParameter("@UnitOfMeasure", _object.UnitOfMeasure);
            spAdapter.AddSqlParameter("@Summ", _object.Summ);
            spAdapter.AddSqlParameter("@Quantity", _object.Quantity);
            spAdapter.ExecNonQuery();
        }
    }
}
