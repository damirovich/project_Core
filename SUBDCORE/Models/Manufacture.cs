using System;
using System.Collections.Generic;

namespace SUBDCORE.Models
{
    public partial class Manufacture
    {
        public short IdManufacture { get; set; }
        public int? Production { get; set; }
        public double? Quantity { get; set; }
        public DateTime? Date { get; set; }
        public short? Employees { get; set; }

        public virtual Employees EmployeesNavigation { get; set; }
        public virtual FinishedProducts ProductionNavigation { get; set; }
    }
}
