using Data_lib;
using FormulaCalc.Data;
using FormulaCalc.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaCalc.Services
{

    public interface IProductServices
    {
       IEnumerable<Product> GetAll();
       Product GetByName(string id);
       Product Create(Product product);
        Product Update(Product product);
       bool Delete(int name);
    }
    public class ProductServices : IProductServices
    {
        private AppDbContext _context;

        public ProductServices(AppDbContext context)
        {
            _context = context;
        }

        public Product Create(Product product)
        {

            //check the product against the database and if the name is already in the db throw back an exception
            if (_context.Products.Any(dbProd => dbProd.Name == product.Name))
                throw new AppException("Product \"" + product.Name + "\" is already taken");

            _context.Products.Add(product);
            _context.SaveChanges();

            return product;
        }
       
       public bool Delete(int id)
       {
           var product = _context.Products.Include(ing => ing.Ingredients).ToList().FirstOrDefault(x => x.ProductID == id);
           if (product != null)
           {
               DeleteIngredientsFromProduct(product.ProductID);
               _context.Products.Remove(product);
               _context.SaveChanges();
                return true;
           }
            return false;
       }
    
       public IEnumerable<Product> GetAll()
       {
           var products = _context.Products.Include(ing => ing.Ingredients).ToList();
           return products;
       }

      public Product GetByName(string name)
      {
          var product = _context.Products.Find(name);
          //if the product searched is not null then get the ingredients it has and assign them into the product list
          if (product != null)
          {
              List<Ingredient> ingredients = GetProductIngredients(product.ProductID);
              if (ingredients != null)
              {
                  product.Ingredients = ingredients;
              }
              return product;
          }
          throw new Exception("Product not found");
      }

        public Product Update(Product product)
        {
            var myProduct = _context.Products.Find(product.ProductID);


            if (myProduct == null)
                throw new AppException("Product not found");


            // update product properties if provided
            if (!string.IsNullOrWhiteSpace(product.Name))
                myProduct.Name = product.Name;
            if (product.Price != myProduct.Price)
                myProduct.Price = product.Price;
            if (product.Mass != myProduct.Mass)
                myProduct.Mass = product.Mass;
            if (!string.IsNullOrWhiteSpace(product.Instructions))
                myProduct.Instructions = product.Instructions;

            foreach (var ing in product.Ingredients)
            {
                var tempIng = _context.Ingredients.Find(ing.IngredientId);
                if (tempIng != null)
                {
                    if (!string.IsNullOrWhiteSpace(ing.Name))
                        tempIng.Name = ing.Name;
                    if (ing.Price != tempIng.Price)
                        tempIng.Price = ing.Price;
                    if (ing.Mass != tempIng.Mass)
                        tempIng.Mass = ing.Mass;
                    if (ing.MassPercentage != tempIng.MassPercentage)
                        tempIng.MassPercentage = ing.MassPercentage;
                    if (ing.Quantity != tempIng.Quantity)
                        tempIng.Quantity = ing.Quantity;
                    if (ing.Type != tempIng.Type)
                        tempIng.Type = ing.Type;
                    if (ing.SpecificGravity != tempIng.SpecificGravity)
                        tempIng.SpecificGravity = ing.SpecificGravity;

                    _context.Ingredients.Update(tempIng);
                }
                else
                {
                    myProduct.Ingredients.Add(ing);
                }
            }
            _context.Products.Update(myProduct);
            _context.SaveChanges();
            return myProduct;
        }

       private List<Ingredient> GetProductIngredients( int productId)
       {
           var product = _context.Products.Find(productId);
           List<Ingredient> ingredients = 
                _context.Ingredients.Include(ing => ing.Product)
                        .Where(res => res.ProductID == product.ProductID).ToList();

            return ingredients;
       }
     
        private void DeleteIngredientsFromProduct(int productId)
        {
            var product = _context.Products.Find(productId);
            List<Ingredient> ingredients = GetProductIngredients(product.ProductID);
            foreach (var ingredient in ingredients)
            {
                _context.Ingredients.Remove(ingredient);
            }
            _context.SaveChanges();
        }
    }
}
