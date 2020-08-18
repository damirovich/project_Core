using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SUBDCORE.Models
{
    public partial class Ingredients
    {
        public short IdIngredients { get; set; }
        [Required(ErrorMessage = "Выберите продукт")]
        public int? Manufacturing { get; set; }
        [Required(ErrorMessage = "Выберите сырье")]
        public short? RawMaterials { get; set; }
        [Required(ErrorMessage = "Введите колличество")]
        public double? Quantity { get; set; }

        public virtual FinishedProducts ManufacturingNavigation { get; set; }
        public virtual RawMaterials RawMaterialsNavigation { get; set; }
    }
}
