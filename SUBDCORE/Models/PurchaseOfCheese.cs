using System;
using System.Collections.Generic;

namespace SUBDCORE.Models
{
    public partial class PurchaseOfCheese
    {
        public int IdPurchaseOfcheese { get; set; }
        public short? RawMaterials { get; set; }
        public double? Quantity { get; set; }
        public decimal? Summ { get; set; }
        public DateTime? Date { get; set; }
        public short? Employees { get; set; }

        public virtual Employees EmployeesNavigation { get; set; }
        public virtual RawMaterials RawMaterialsNavigation { get; set; }
    }
}
