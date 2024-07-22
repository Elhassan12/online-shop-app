using Microsoft.AspNetCore.Components;
using OnlineShop.Web.Services.Contracts;
using ShopOnline.Models.Dtos;

namespace OnlineShop.Web.Pages
{
    public class ShoppingCartBase:ComponentBase
    {
        [Inject]
        IShopCartService ShopCartService { get; set; }
        public IEnumerable<CartItemDto> CartItems { get; set; }
        public string ErrorMessage { get; set; }
        public CartItemDto CartItem { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                CartItems = await ShopCartService.GetItems(userId: 1);
                CartItem = CartItems.First();
            }
            catch (Exception ex) {
                ErrorMessage = ex.Message;
            }
        }
    }
}
