using System;
using System.Collections.Generic;

namespace SUBDCORE.Models
{
    public partial class Payroll
    {
        public short IdSalary { get; set; }
        public short? IdEmployee { get; set; }
        public short? Years { get; set; }
        public byte? Month { get; set; }
        public int? Purchased { get; set; }
        public int? Sales { get; set; }
        public int? Manufacture { get; set; }
        public int? WorkPerformed { get; set; }
        public decimal? SumSalary { get; set; }
        public DateTime? DataPayroll { get; set; }
        public decimal? ProsentZp { get; set; }
        public decimal? SummaSalary { get; set; }
        public decimal? WagePrem { get; set; }
        public decimal? InitalProsent { get; set; }
        public decimal? InitalSalary { get; set; }

        public virtual Employees IdEmployeeNavigation { get; set; }
        public virtual Month MonthNavigation { get; set; }
        public virtual Years YearsNavigation { get; set; }
    }
}
