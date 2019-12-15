using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data_lib;
using FormulaCalc.Models;
using FormulaCalc.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaCalc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductServices _ProductService;
        public IMapper _Mapper;



        public ProductController(
          IProductServices ProductService,
          IMapper mapper
)        {
            _ProductService = ProductService;
            _Mapper = mapper;

        }


      [HttpPost("getAll")]
      public IActionResult GetAll()
      {
           var products = _ProductService.GetAll();     
           return Ok(products);
      }
  
  
      [HttpPost("getByName")]
      public IActionResult GetByName([FromBody]GetByNameModel model)
      {
          var product = _ProductService.GetByName(model.name);
          var productModel = _Mapper.Map<ProductModel>(product);
  
          return Ok(productModel);
      }

        [HttpPost("create")]
        public IActionResult Create([FromBody]ProductModel model)
        {
            var ingredients = _Mapper.Map<List<Ingredient>>(model.Ingredients);
            Product prod = new Product()
            {
                Name = model.Name,
                Ingredients = new List<Ingredient>(ingredients),
                Mass = model.Mass,
                Price = model.Price,
                Instructions = model.Instructions
            };
            

            try
            {
               var x = _ProductService.Create(prod);
                return Ok(x);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

       [HttpPost("update")]
       public IActionResult Update([FromBody] ProductModel model)
       {
           var product = _Mapper.Map<Product>(model);
   
           try
           {
               var prod = _ProductService.Update(product);
               return Ok(prod);
           }
           catch (Exception ex)
           {
   
               return BadRequest(new { message = ex.Message });
           }
   
       }


        [HttpPost("delete")]
        public IActionResult Delete([FromBody] GetByNameModel model)
        {
            int.TryParse(model.name, out int id);
            try
            {
                var check =  _ProductService.Delete(id);
                if (check)
                {
                    var products = _ProductService.GetAll();
                    return Ok(products);
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }

        }
    }
}