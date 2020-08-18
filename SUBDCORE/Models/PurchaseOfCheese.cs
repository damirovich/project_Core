using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SUBDCORE.Models
{
    public partial class PurchaseOfCheese
    {
        public int IdPurchaseOfcheese { get; set; }
        [Required(ErrorMessage = "Выберите сырье")]
        public short? RawMaterials { get; set; }
        [Required(ErrorMessage = "Введите колличество")]
        public double? Quantity { get; set; }
        [Required(ErrorMessage = "Введите цену за сырье")]
        public decimal? Summ { get; set; }
        [Required(ErrorMessage = "Укажите дату покупки")]
        public DateTime? Date { get; set; }
        [Required(ErrorMessage = "Выберите сотрудника")]
        public short? Employees { get; set; }

        public virtual Employees EmployeesNavigation { get; set; }
        public virtual RawMaterials RawMaterialsNavigation { get; set; }
    }
}
