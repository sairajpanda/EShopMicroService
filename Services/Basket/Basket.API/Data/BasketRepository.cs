using Basket.API.DBContext;
using static MassTransit.ValidationResultExtensions;

namespace Basket.API.Data;

public class BasketRepository : IBasketRepository
{
    public BasketDbContext _dbcontext;
    public BasketRepository(BasketDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken)
    {
        var deleteresult = await _dbcontext.ShoppingCarts.Include(x => x.Items).AsNoTracking().Where(x => x.UserName == userName).FirstOrDefaultAsync(cancellationToken);
        _dbcontext.ShoppingCarts.Remove(deleteresult);
        await _dbcontext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
    {
        return await _dbcontext.ShoppingCarts.Include(x => x.Items).AsNoTracking().Where(x => x.UserName == userName).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken)
    {
        await _dbcontext.ShoppingCarts.AddAsync(basket);
        var result = await _dbcontext.SaveChangesAsync(cancellationToken);
        Console.WriteLine($"Rows affected: {result}");
        return basket;
    }

}
