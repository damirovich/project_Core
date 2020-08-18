using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SUBDCORE.Models
{
    public partial class Budget
    {
        public int IdBudget { get; set; }
        [Required(ErrorMessage ="Не указано Сумма бюджета")]
        public double? BudgetSum { get; set; }
        [Required(ErrorMessage ="Не указано процент продажи продукта")]
        public byte? Prosent { get; set; }
        [Required(ErrorMessage ="Не указано процент надбавки зарплаты для сотрудников")]
        public byte? ProsentSalary { get; set; }
    }
}
