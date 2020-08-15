using System;
using System.Collections.Generic;

namespace SUBDCORE.Models
{
    public partial class Years
    {
        public Years()
        {
            Payroll = new HashSet<Payroll>();
        }

        public short IdYears { get; set; }
        public int? YearsName { get; set; }

        public virtual ICollection<Payroll> Payroll { get; set; }
    }
}
