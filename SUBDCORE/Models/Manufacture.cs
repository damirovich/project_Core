using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SUBDCORE.Models
{
    public partial class Manufacture
    {
        public short IdManufacture { get; set; }
        [Required(ErrorMessage = "Выберите продукт")]
        public int? Production { get; set; }
        [Required(ErrorMessage = "Введите колличество")]
        public double? Quantity { get; set; }
        [Required(ErrorMessage ="Укажите дату производства")]
        public DateTime? Date { get; set; }
        [Required(ErrorMessage = "Выберите сотрудника")]
        public short? Employees { get; set; }

        public virtual Employees EmployeesNavigation { get; set; }
        public virtual FinishedProducts ProductionNavigation { get; set; }
    }
}
