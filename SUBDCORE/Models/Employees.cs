using System;
using System.Collections.Generic;

namespace SUBDCORE.Models
{
    public partial class Employees
    {
        public Employees()
        {
            Manufacture = new HashSet<Manufacture>();
            Payroll = new HashSet<Payroll>();
            ProductSales = new HashSet<ProductSales>();
            PurchaseOfCheese = new HashSet<PurchaseOfCheese>();
        }

        public short IdEmployees { get; set; }
        public string FullName { get; set; }
        public byte? Position { get; set; }
        public decimal? Salary { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual Position PositionNavigation { get; set; }
        public virtual ICollection<Manufacture> Manufacture { get; set; }
        public virtual ICollection<Payroll> Payroll { get; set; }
        public virtual ICollection<ProductSales> ProductSales { get; set; }
        public virtual ICollection<PurchaseOfCheese> PurchaseOfCheese { get; set; }
    }
}
