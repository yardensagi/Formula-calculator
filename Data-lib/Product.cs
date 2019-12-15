using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data_lib
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        public string Name { get; set; }
        public double Price { get; set; }
        public double Mass { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Instructions { get; set; }
    }
}
