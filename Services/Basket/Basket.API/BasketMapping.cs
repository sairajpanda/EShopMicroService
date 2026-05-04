using AutoMapper;
using Basket.API.Basket;
using Basket.API.Basket.CheckoutBasket;
using Basket.API.Basket.GetBaskets;
using BuildingBlocks.Messaging.Events;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class BasketMappingProfile : Profile
   {
        public BasketMappingProfile()
        {
            CreateMap<StoreBasketsRequest, StoreBasketCommnad>()
                .ConstructUsing(src =>
                    new StoreBasketCommnad(
                       src.UserName,
                       src.Items, 
                       src.TotalItemPrice
                    ));

           CreateMap<StoreBasketResult, StoreBasketsResponse>()
             .ConstructUsing(src =>
                 new StoreBasketsResponse(
                    src.UserName
                 ));

        CreateMap<GetBasketResults, GetbasketResponse>()
           .ConstructUsing(src =>
               new GetbasketResponse(
                  src._shoppingCart
               ));

        CreateMap<DeleteBasketResults, DeleteBasketResponse>()
        .ConstructUsing(src =>
         new DeleteBasketResponse(
            src.IsSuccess
         ));

        CreateMap<CheckOutBasketRequest, CheckOutBasketCommand>()
            .ConstructUsing(src => 
            new CheckOutBasketCommand(
                src.basketCheckoutDto
            ));

        CreateMap<CheckOutBasketCommand, BasketCheckoutEvent>()
           .ConstructUsing(src =>
           new BasketCheckoutEvent(
           src.basketCheckoutDto.UserName,
           src.basketCheckoutDto.CustermId,
           src.basketCheckoutDto.TotalPrice,
           src.basketCheckoutDto.FirstName,
           src.basketCheckoutDto.LastName,
           src.basketCheckoutDto.Street,
           src.basketCheckoutDto.City,
           src.basketCheckoutDto.State,
           src.basketCheckoutDto.Country,
           src.basketCheckoutDto.ZipCode,
           src.basketCheckoutDto.EmailAddress,
           src.basketCheckoutDto.PaymentMethod,
           src.basketCheckoutDto.CardName,
           src.basketCheckoutDto.CardNumber,
           src.basketCheckoutDto.CardHolderName,
           src.basketCheckoutDto.ExpirationDate,
           src.basketCheckoutDto.CVV
           ));


    //           CreateMap<CreateProductResult, CreateProductResponse>()
    //           .ConstructUsing(src =>
    //               new CreateProductResponse(
    //                   src.Id
    //               ));


    //          CreateMap<GetProductsResult, GetProductsResponse>()
    //          .ConstructUsing(src =>
    //              new GetProductsResponse(
    //                  src.Products
    //              ));

    //        CreateMap<GetProdcutsByIDResult, GetProdcutsByIdResponse>()
    //         .ConstructUsing(src =>
    //               new GetProdcutsByIdResponse(
    //                   src._products
    //              ));

    //        CreateMap<GetProductByCategoryResult, GetProductByCategoryResponse>()
    //        .ConstructUsing(src =>
    //              new GetProductByCategoryResponse(
    //                  src.Products
    //             ));

    //        CreateMap<UpdateProductResults, UpdateProductResponse>()
    //     .ConstructUsing(src =>
    //           new UpdateProductResponse(
    //               src.success
    //          ));

    //        CreateMap<UpdateProductRequest, UpdateProductCommand>()
    //  .ConstructUsing(src =>
    //        new UpdateProductCommand(
    //            src.Id, src.Name, src.Description, src.Price, src.Category, src.ImageFile
    //       ));

    //        CreateMap<DeleteProductRequest, DeleteProductCommand>()
    //.ConstructUsing(src =>
    //new DeleteProductCommand(
    //    src.Id
    //));
}
}
