using Microsoft.AspNetCore.Components;
using OnlineShop.Web.Services.Contracts;
using ShopOnline.Models.Dtos;

namespace OnlineShop.Web.Pages
{
    public partial class ProductDetails:ComponentBase
    {
        public ProductDto Product { get; set; }

        [Parameter]
        public int ProductId { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IShopCartService ShopCartService { get; set; }
        public string ErrorMessage { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try {
                Product = await ProductService.GetItem(ProductId);
            }
            catch(Exception ex) {
               ErrorMessage = ex.Message;
            }
          
        }
        protected async Task addToCart_Click(CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var cartItemDto = ShopCartService.AddItem(cartItemToAddDto);
                NavigationManager.NavigateTo("/shopcart");

            }catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        

        

    }
}
