using ShopOnline.Api.Entities;
using ShopOnline.Models.Dtos;

namespace OnlineShop1.API.Repository.Contracts
{
    public interface IShopCartRepository
    {
        Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto);
        Task<CartItem> DeleteItem(int id);
        Task<CartItem> GetItem(int id);
        Task<IEnumerable<CartItem>> GetItems(int userId);
    }
}
