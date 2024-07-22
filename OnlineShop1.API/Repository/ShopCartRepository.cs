using Microsoft.EntityFrameworkCore;
using OnlineShop.Api.Data;
using OnlineShop1.API.Repository.Contracts;
using ShopOnline.Api.Entities;
using ShopOnline.Models.Dtos;

namespace OnlineShop1.API.Repository
{
    public class ShopCartRepository : IShopCartRepository
    {
        private readonly ShopOnlineDBContext shopOnlineDbContext;

        public ShopCartRepository(ShopOnlineDBContext shopOnlineDbContext)
        {
            this.shopOnlineDbContext = shopOnlineDbContext;
        }

        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await this.shopOnlineDbContext.CartItmes.AnyAsync(c => c.CartId == cartId &&
                                                                     c.ProductId == productId);

        }
        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            if (await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
            {
                var item = await (from product in this.shopOnlineDbContext.Products
                                  where product.Id == cartItemToAddDto.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAddDto.CartId,
                                      ProductId = product.Id,
                                      Qty = cartItemToAddDto.Qty
                                  }).SingleOrDefaultAsync();

                if (item != null)
                {
                    var result = await this.shopOnlineDbContext.CartItmes.AddAsync(item);
                    await this.shopOnlineDbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }

            return null;

        }

        public async Task<CartItem> DeleteItem(int id)
        {
            var item = await this.shopOnlineDbContext.CartItmes.FindAsync(id);

            if (item != null)
            {
                this.shopOnlineDbContext.CartItmes.Remove(item);
                await this.shopOnlineDbContext.SaveChangesAsync();
            }

            return item;

        }

        public async Task<CartItem> GetItem(int id)
        {
            return await (from cart in this.shopOnlineDbContext.Carts
                          join cartItem in this.shopOnlineDbContext.CartItmes
                          on cart.Id equals cartItem.CartId
                          where cartItem.Id == id
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            return await (from cart in this.shopOnlineDbContext.Carts
                          join cartItem in this.shopOnlineDbContext.CartItmes
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).ToListAsync();
        }

        public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            var item = await this.shopOnlineDbContext.CartItmes.FindAsync(id);

            if (item != null)
            {
                item.Qty = cartItemQtyUpdateDto.Qty;
                await this.shopOnlineDbContext.SaveChangesAsync();
                return item;
            }

            return null;
        }
    }
}
