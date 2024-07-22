using Microsoft.AspNetCore.Components;
using OnlineShop.Web.Services.Contracts;
using ShopOnline.Models.Dtos;
using System.ComponentModel;

namespace OnlineShop.Web.Pages
{
    public class ProductsBase:ComponentBase
    {
        [Inject]
        public IProductService  ProductService { get; set; }
       public IEnumerable<ProductDto> Products { get; set; }

        protected override async  Task OnInitializedAsync()
        {
            Products = await ProductService.GetItems();
            
        }
        protected IOrderedEnumerable<IGrouping<int,ProductDto>> getGroupedProductsByCategory()
        {
            return from product in Products
                   group product by product.CategoryId into proByCatGroup
                   orderby proByCatGroup.Key
                   select proByCatGroup;
        }



    }
}
