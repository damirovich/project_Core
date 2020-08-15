using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SUBDCORE.Models;

namespace SUBDCORE.Repository
{
    public class MonthRepository
    {
        SQLSpAdapter sqladapter;
        List<Month> month = new List<Month>();

        public List<Month> getMonths() {
            sqladapter = new SQLSpAdapter("getMonth");
            sqladapter.ExecReader();
            foreach (var row in sqladapter.baggage) {
                month.Add(new Month()
                {
                    IdMonth = Convert.ToByte(row[0]),
                    MonthName = row[1].ToString()
                });
            }
            return month;
        }
    }
}
