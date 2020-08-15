using System;
using System.Collections.Generic;

namespace SUBDCORE.Models
{
    public partial class ProductSales
    {
        public int IdProductSales { get; set; }
        public int? FinishedProduct { get; set; }
        public double? Quantity { get; set; }
        public decimal? Summ { get; set; }
        public DateTime? Date { get; set; }
        public short? Employees { get; set; }

        public virtual Employees EmployeesNavigation { get; set; }
        public virtual FinishedProducts FinishedProductNavigation { get; set; }
    }
}
