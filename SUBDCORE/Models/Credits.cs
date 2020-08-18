using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SUBDCORE.Models
{
    public partial class Credits
    {
        public Credits()
        {
            LoanRepayment = new HashSet<LoanRepayment>();
        }

        public short IdCredits { get; set; }
        [Required(ErrorMessage = "Введите наименование Банка")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите сумму кредита")]
        public decimal? CreaditAmount { get; set; }
        [Required(ErrorMessage = "Введите годовую процентную ставку")]
        public byte? InterestRate { get; set; }
        [Required(ErrorMessage = "Введите срок кредита")]
        public short? CreditTerm { get; set; }
        [Required(ErrorMessage = "Укажите дата получение кредита")]
        public DateTime? DateOfIssue { get; set; }
        public decimal? MonthlyPayment { get; set; }
        public decimal? Fine { get; set; }

        public virtual ICollection<LoanRepayment> LoanRepayment { get; set; }
    }
}
