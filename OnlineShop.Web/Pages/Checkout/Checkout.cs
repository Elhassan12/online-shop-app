using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OnlineShop.Web.Services.Contracts;
using ShopOnline.Models.Dtos;

namespace OnlineShop.Web.Pages.Checkout
{
    public partial class Checkout:ComponentBase
    {
        public List<CartItemDto> CartItems { get; set; }
        [Inject]
        public IShopCartService ShopCartService { get; set; }
        public string TotalPrice { get; set; }
        public string PaymentDescription { get; set; }
        public decimal PaymentAmount { get; set; }
        public int TotalQty { get; set; }
        
        public string DisplayButtons { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected async override Task OnInitializedAsync()
        {
            CartItems = await ShopCartService.GetItems(1);
            SetTotalPrice();
            if (CartItems != null && CartItems.Count() > 0)
            {
                Guid orderGuid = Guid.NewGuid();

                PaymentAmount = CartItems.Sum(p => p.TotalPrice);
                TotalQty = CartItems.Sum(p => p.Qty);
                PaymentDescription = $"O_{1}_{orderGuid}";

            }
            else
            {
                DisplayButtons = "none";
            }
        }
        protected override async void OnAfterRender(bool firstRender)
        {
            if(firstRender) {
              await  JSRuntime.InvokeVoidAsync("initPayPalButton");
            }
        }

        private void SetTotalPrice()
        {
            this.TotalPrice = this.CartItems.Sum(p => p.TotalPrice).ToString("C");
        }

    }
}
