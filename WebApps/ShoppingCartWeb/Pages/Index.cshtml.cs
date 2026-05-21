using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCartWeb.Models.Basket;
using ShoppingCartWeb.Models.Catalog;
using ShoppingCartWeb.Services;
using System.Security.Cryptography.X509Certificates;


namespace ShoppingCartWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public ICatalogService _catalogService;
        public IBasketService _basketService;

        public IEnumerable<Product> ProductList { get; set; } = new List<Product>();

        public IndexModel(ILogger<IndexModel> logger, IBasketService basketService, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public async Task<IActionResult> OnGet()
        {
            var result = await _catalogService.GetProducts();
            ProductList = result.Products;
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            try
            {
                var UserBasketDetails = await _basketService.GetUserBaskets();
                var ProductDetails = await _catalogService.GetProduct(productId);

                if (UserBasketDetails == null)
                {
                    UserBasketDetails = new ShoppingCartModel
                    {
                        UserName = "SairajPanda",
                        Items = new List<ShoppingCartItemModel>()
                    };

                }

                UserBasketDetails.Items.Add(new ShoppingCartItemModel
                {
                    ProductName = ProductDetails._products.Name,
                    Quantity = 1,
                    Price = (decimal)ProductDetails._products.Price,
                    Color = "Black",
                    ProductId = productId
                });
                var UserStoreBasket = await _basketService.StoreBasket(new StoreBasketRequest(
                   "SairajPanda",
                   UserBasketDetails.Items,
                   UserBasketDetails.Items.Sum(x => x.Price * x.Quantity)
                ));
                return RedirectToPage("/Cart");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

           
        }
    }
}
