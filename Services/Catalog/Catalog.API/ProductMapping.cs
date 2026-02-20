using AutoMapper;
using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.DeleteProducts;
using Catalog.API.Products.GetProducts;
using Catalog.API.Products.GetProductsByCategory;
using Catalog.API.Products.GetProductsByID;
using Catalog.API.Products.UpdateProducts;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;
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

        CreateMap<GetProdcutsByIDResult, GetProdcutsByIdResponse>()
         .ConstructUsing(src =>
               new GetProdcutsByIdResponse(
                   src._products
              ));

        CreateMap<GetProductByCategoryResult, GetProductByCategoryResponse>()
        .ConstructUsing(src =>
              new GetProductByCategoryResponse(
                  src.Products
             ));

        CreateMap<UpdateProductResults, UpdateProductResponse>()
     .ConstructUsing(src =>
           new UpdateProductResponse(
               src.success
          ));

        CreateMap<UpdateProductRequest, UpdateProductCommand>()
  .ConstructUsing(src =>
        new UpdateProductCommand(
            src.Id, src.Name, src.Description, src.Price, src.Category, src.ImageFile
       ));

        CreateMap<DeleteProductRequest, DeleteProductCommand>()
.ConstructUsing(src =>
new DeleteProductCommand(
    src.Id
));
    }
    }
