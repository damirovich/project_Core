using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SUBDCORE.Models
{
    public partial class UnitOfMeasure
    {
        public UnitOfMeasure()
        {
            FinishedProducts = new HashSet<FinishedProducts>();
            RawMaterials = new HashSet<RawMaterials>();
        }

        public int IdUnitOfmeasure { get; set; }
        [Required(ErrorMessage = "Введите наименование единицы")]
        public string Names { get; set; }

        public virtual ICollection<FinishedProducts> FinishedProducts { get; set; }
        public virtual ICollection<RawMaterials> RawMaterials { get; set; }
    }
}
