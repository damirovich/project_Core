using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SUBDCORE.Models;

namespace SUBDCORE.Repository
{
    public class UnitOfMeasurerepository : ICrudable<UnitOfMeasure>
    {
        SQLSpAdapter spAdapter;
        List<UnitOfMeasure> units = new List<UnitOfMeasure>();
        public void Create(UnitOfMeasure _object)
        {
            spAdapter = new SQLSpAdapter("AddUnitOfMeasure");
            spAdapter.AddSqlParameter("@Names", _object.Names);
            spAdapter.ExecNonQuery();
        }
        public void Delete(int id)
        {
            spAdapter = new SQLSpAdapter("DelUnitOfMeasure");
            spAdapter.AddSqlParameter("@idUnitOfMeasure", id);
            spAdapter.ExecNonQuery();
        }

        public IEnumerable<UnitOfMeasure> GetList()
        {
            spAdapter = new SQLSpAdapter("GetUnitOfMeasure");
            spAdapter.ExecReader();
            foreach (var row in spAdapter.baggage)
            {
                units.Add(new UnitOfMeasure()
                {
                    IdUnitOfmeasure = Convert.ToInt32(row[0]),
                    Names = row[1].ToString()
                });
            }
            return units;
        }

        public void Update(UnitOfMeasure _object)
        {
            spAdapter = new SQLSpAdapter("UpUnitOfMeasure");
            spAdapter.AddSqlParameter("@idUnitOfMeasure", _object.IdUnitOfmeasure);
            spAdapter.AddSqlParameter("@Names", _object.Names);
            spAdapter.ExecNonQuery();
        }
    }
}
