using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SUBDCORE.Models
{
    public partial class FinishedProducts
    {
        public FinishedProducts()
        {
            Ingredients = new HashSet<Ingredients>();
            Manufacture = new HashSet<Manufacture>();
            ProductSales = new HashSet<ProductSales>();
        }

        public int IdFinishedProducts { get; set; }
        [Required(ErrorMessage ="Введите наименование продукта")]
        public string Names { get; set; }
        [Required(ErrorMessage = "Выберите единицы измерения")]
        public int? UnitOfMeasure { get; set; }
        public decimal? Summ { get; set; }
        public double? Quantity { get; set; }

        public virtual UnitOfMeasure UnitOfMeasureNavigation { get; set; }
        public virtual ICollection<Ingredients> Ingredients { get; set; }
        public virtual ICollection<Manufacture> Manufacture { get; set; }
        public virtual ICollection<ProductSales> ProductSales { get; set; }
    }
}
