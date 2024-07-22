using ShopOnline.Api.Entities;

namespace OnlineShop1.API.Repository.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetItems();
        Task<IEnumerable<ProductCategory>> GetCategories();
        Task<Product> GetItem(int id);
        Task<ProductCategory> GetCategory(int id);

        Task<IEnumerable<Product>> GetItemsByCategory(int id);

    }
}
