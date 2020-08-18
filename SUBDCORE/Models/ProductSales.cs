using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SUBDCORE.Models
{
    public partial class ProductSales
    {
        public int IdProductSales { get; set; }
        [Required(ErrorMessage = "Выберите продукт")]
        public int? FinishedProduct { get; set; }
        [Required(ErrorMessage = "Введите колличество")]
        public double? Quantity { get; set; }
        public decimal? Summ { get; set; }
        [Required(ErrorMessage = "Укажите дату продажи")]
        public DateTime? Date { get; set; }
        [Required(ErrorMessage = "Выберите сотрудника")]
        public short? Employees { get; set; }

        public virtual Employees EmployeesNavigation { get; set; }
        public virtual FinishedProducts FinishedProductNavigation { get; set; }
    }
}
