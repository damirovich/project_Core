using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SUBDCORE.Models;

namespace SUBDCORE.Repository
{
    public class YearsRepository
    {
        SQLSpAdapter sqladapter;
        List<Years> month = new List<Years>();
        public List<Years> getYears()
        {
            sqladapter = new SQLSpAdapter("GetYears");
            sqladapter.ExecReader();
            foreach (var row in sqladapter.baggage)
            {
                month.Add(new Years()
                {
                    IdYears = Convert.ToByte(row[0]),
                    YearsName = Convert.ToInt32(row[1])
                });
            }
            return month;
        }
    }
}
