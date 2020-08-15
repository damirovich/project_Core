using System;
using System.Collections.Generic;

namespace SUBDCORE.Models
{
    public partial class Month
    {
        public Month()
        {
            LoanRepayment = new HashSet<LoanRepayment>();
            Payroll = new HashSet<Payroll>();
        }

        public byte IdMonth { get; set; }
        public string MonthName { get; set; }

        public virtual ICollection<LoanRepayment> LoanRepayment { get; set; }
        public virtual ICollection<Payroll> Payroll { get; set; }
    }
}
