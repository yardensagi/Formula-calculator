using Data_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaCalc.Models
{
    public class ProductModel
    {
        public int? ProductID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Mass { get; set; }
        public List<CreateIngredientModel> Ingredients { get; set; }
        public string Instructions { get; set; }

    }
    public class GetByNameModel
    {
        public string name { get; set; }
    }


}
