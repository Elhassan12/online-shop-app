using Microsoft.EntityFrameworkCore;
using OnlineShop.Api.Data;
using OnlineShop1.API.Repository.Contracts;
using ShopOnline.Api.Entities;

namespace OnlineShop1.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopOnlineDBContext shopOnlineDbContext;

        public ProductRepository(ShopOnlineDBContext shopOnlineDbContext)
        {
            this.shopOnlineDbContext = shopOnlineDbContext;
        }
        async Task<IEnumerable<ProductCategory>> IProductRepository.GetCategories()
        {
            return await shopOnlineDbContext.ProductsCategories.ToListAsync();
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await shopOnlineDbContext.ProductsCategories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<Product> GetItem(int id)
        {
            var product = await shopOnlineDbContext.Products
                                .Include(p => p.ProductCategory)
                                .SingleOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await this.shopOnlineDbContext.Products
                                     .Include(p => p.ProductCategory).ToListAsync();

            return products;

        }

        public async Task<IEnumerable<Product>> GetItemsByCategory(int id)
        {
            var products = await this.shopOnlineDbContext.Products
                                     .Include(p => p.ProductCategory)
                                     .Where(p => p.CategoryId == id).ToListAsync();
            return products;
        }
    }
}
