using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SUBDCORE.Models;

namespace SUBDCORE.Repository
{
    public class Ingredientsrepository : ICrudable<Ingredients>
    {
        SQLSpAdapter spAdapter;
        List<Ingredients> ingredients = new List<Ingredients>();
        public void Create(Ingredients _object)
        {
            spAdapter = new SQLSpAdapter("AddIngredients");
            spAdapter.AddSqlParameter("@Manufacturing", _object.Manufacturing);
            spAdapter.AddSqlParameter("@RawMaterials", _object.RawMaterials);
            spAdapter.AddSqlParameter("@Quantity", _object.Quantity);
            spAdapter.ExecNonQuery();
        }

        public void Delete(int id)
        {
            spAdapter = new SQLSpAdapter("DelIngredients");
            spAdapter.AddSqlParameter("@idIngredients", id);
            spAdapter.ExecNonQuery();
        }

        public IEnumerable<Ingredients> GetList()
        {
            Ingredients ing;
            spAdapter = new SQLSpAdapter("GetIngredients");
            spAdapter.ExecReader();
            foreach (var row in spAdapter.baggage)
            {
                ing = new Ingredients();
                ing.ManufacturingNavigation = new FinishedProducts();
                ing.RawMaterialsNavigation = new RawMaterials();
                ing.IdIngredients= Convert.ToInt16(row[0]);
                ing.Manufacturing = Convert.ToInt32(row[1]);
                ing.RawMaterials = Convert.ToInt16(row[2]);
                ing.Quantity = Convert.ToDouble(row[3]);
                ing.ManufacturingNavigation.IdFinishedProducts = Convert.ToByte(row[4]);
                ing.ManufacturingNavigation.Names = row[5].ToString();
                ing.RawMaterialsNavigation.IdRawMaterials = Convert.ToByte(row[6]);
                ing.RawMaterialsNavigation.Names = row[7].ToString();
                ingredients.Add(ing);
            }
            return ingredients;
        }

        public void Update(Ingredients _object)
        {
            spAdapter = new SQLSpAdapter ("UpIngredients");
            spAdapter.AddSqlParameter("@idIngredients", _object.IdIngredients);
            spAdapter.AddSqlParameter("@Manufacturing", _object.Manufacturing);
            spAdapter.AddSqlParameter("@RawMaterials", _object.RawMaterials);
            spAdapter.AddSqlParameter("@Quantity", _object.Quantity);
            spAdapter.ExecNonQuery();
        }
    }
}
