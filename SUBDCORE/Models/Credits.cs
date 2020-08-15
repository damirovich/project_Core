using System;
using System.Collections.Generic;

namespace SUBDCORE.Models
{
    public partial class Credits
    {
        public Credits()
        {
            LoanRepayment = new HashSet<LoanRepayment>();
        }

        public short IdCredits { get; set; }
        public string Name { get; set; }
        public decimal? CreaditAmount { get; set; }
        public byte? InterestRate { get; set; }
        public short? CreditTerm { get; set; }
        public DateTime? DateOfIssue { get; set; }
        public decimal? MonthlyPayment { get; set; }
        public decimal? Fine { get; set; }

        public virtual ICollection<LoanRepayment> LoanRepayment { get; set; }
    }
}
