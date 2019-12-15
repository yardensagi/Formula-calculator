using AutoMapper;
using Data_lib;
using FormulaCalc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaCalc.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();
            CreateMap<CreateIngredientModel, Ingredient>();
            CreateMap<Ingredient, CreateIngredientModel>();

        }
    }
}
