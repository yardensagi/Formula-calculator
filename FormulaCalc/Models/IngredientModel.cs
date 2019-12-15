using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaCalc.Models
{

    public class CreateIngredientModel
    {
        public int? IngredientId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Mass { get; set; }
        public double Quantity { get; set; }
        public double? MassPercentage { get; set; }
        public string Type { get; set; }
        public double? SpecificGravity { get; set; }
        public int? ProductID { get; set; }
    }
}
