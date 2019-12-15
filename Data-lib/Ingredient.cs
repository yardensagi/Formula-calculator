using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data_lib
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double Mass { get; set; }
        public string Type { get; set; }
        [Column("Mass Percentage")]
        public double? MassPercentage { get; set; }
        [Column("Specific Gravity")]
        public double? SpecificGravity { get; set; }
        public double Price { get; set; }
        //this part will set the foreignkey for productId 
        public int? ProductID { get; set; }
        public Product Product { get; set; }
    }

}
