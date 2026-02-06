using AutoMapper;
using Catalog.API.Products.CreateProduct;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductRequest, CreateProductCommand>()
                .ConstructUsing(src =>
                    new CreateProductCommand(
                        src.Name,
                        src.Description,
                        src.Price,
                        src.Category,
                        src.ImageFile
                    ));

           CreateMap<CreateProductResult, CreateProductResponse>()
           .ConstructUsing(src =>
               new CreateProductResponse(
                   src.Id
               ));
    }
    }
