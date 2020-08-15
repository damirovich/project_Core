using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SUBDCORE.Models;

namespace SUBDCORE.Repository
{
    public class RawMaterialsrepository : ICrudable<RawMaterials>
    {
        SQLSpAdapter spAdapter;
        List<RawMaterials> rawMaterials = new List<RawMaterials>();
        public void Create(RawMaterials _object)
        {
            spAdapter = new SQLSpAdapter("AddRawMaterials");
            spAdapter.AddSqlParameter("@Names", _object.Names);
            spAdapter.AddSqlParameter("@UnitOfMeasure", _object.UnitOfMeasure);
            spAdapter.AddSqlParameter("@Summ", _object.Summ);
            spAdapter.AddSqlParameter("@Quantity", _object.Quantity);
            spAdapter.ExecNonQuery();
        }

        public void Delete(int id)
        {
            spAdapter = new SQLSpAdapter("DelRawMaterials");
            spAdapter.AddSqlParameter("@idRawMaterials", id);
            spAdapter.ExecNonQuery();
        }

        public IEnumerable<RawMaterials> GetList()
        {
            RawMaterials rw;
            spAdapter = new SQLSpAdapter("GetRawMaterials");
            spAdapter.ExecReader();
            foreach (var row in spAdapter.baggage)
            {
                rw = new RawMaterials();
                rw.UnitOfMeasureNavigation = new UnitOfMeasure();
                rw.IdRawMaterials = Convert.ToByte(row[0]);
                rw.Names = row[1].ToString();
                rw.UnitOfMeasure = Convert.ToByte(row[2]);
                rw.Quantity = Convert.ToDouble(row[3]);
                rw.Summ = Convert.ToDecimal(row[4]);
                rw.UnitOfMeasureNavigation.IdUnitOfmeasure = Convert.ToByte(row[5]);
                rw.UnitOfMeasureNavigation.Names = row[6].ToString();
                rawMaterials.Add(rw);
            }
            return rawMaterials;
        }

        public void Update(RawMaterials _object)
        {
            spAdapter = new SQLSpAdapter("UpRawMaterials");
            spAdapter.AddSqlParameter("@idRawMaterials", _object.IdRawMaterials);
            spAdapter.AddSqlParameter("@Names", _object.Names);
            spAdapter.AddSqlParameter("@UnitOfMeasure", _object.UnitOfMeasure);
            spAdapter.AddSqlParameter("@Summ", _object.Summ);
            spAdapter.AddSqlParameter("@Quantity", _object.Quantity);
            spAdapter.ExecNonQuery();
        }
    }
}
