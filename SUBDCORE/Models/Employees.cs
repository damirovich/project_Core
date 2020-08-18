using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage ="Введите ФИО сотрудника")]
        public string FullName { get; set; }
        [Required(ErrorMessage ="Укажите должность")]
        public byte? Position { get; set; }
        [Required(ErrorMessage ="Введите зарплату")]
        public decimal? Salary { get; set; }
        [Required(ErrorMessage = "Не укзан адрес проживания")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Укажите номер телефона")]
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
