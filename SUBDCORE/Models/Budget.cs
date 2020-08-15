using System;
using System.Collections.Generic;

namespace SUBDCORE.Models
{
    public partial class Budget
    {
        public int IdBudget { get; set; }
        public double? BudgetSum { get; set; }
        public byte? Prosent { get; set; }
        public byte? ProsentSalary { get; set; }
    }
}
