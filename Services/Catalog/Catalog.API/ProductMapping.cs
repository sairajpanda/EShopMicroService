using AutoMapper;
using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.GetProducts;
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


          CreateMap<GetProductsResult, GetProductsResponse>()
          .ConstructUsing(src =>
              new GetProductsResponse(
                  src.Products
              ));
    }
    }
