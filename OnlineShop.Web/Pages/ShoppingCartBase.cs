using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OnlineShop.Web.Services.Contracts;
using ShopOnline.Models.Dtos;

namespace OnlineShop.Web.Pages
{
    public class ShoppingCartBase:ComponentBase
    {
        [Inject]
        IShopCartService ShopCartService { get; set; }
        [Inject]
        public IJSRuntime jSRuntime { get; set; }
        public List<CartItemDto> CartItems { get; set; }
        public string ErrorMessage { get; set; }
        protected string TotalPrice { get; set; }
        protected int TotalQuantity { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; } 

        protected async override Task OnInitializedAsync()
        {
            try
            {
                CartItems = await ShopCartService.GetItems(userId: 1);
                CartChanged();
            }
            catch (Exception ex) {
                ErrorMessage = ex.Message;
            }
        }

        public async void MakeUpdateQtyButtonVisible(int id,bool state)
        {
            await jSRuntime.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, state);
        }
        public async Task updateQty_button(int id)
        {
            MakeUpdateQtyButtonVisible(id, true);
            

        }

        protected async Task deleteItemFromCart_Click(int itemId)
        {
           CartItemDto cartItemDto = await ShopCartService.DeleteItem(itemId);

            removeCartItem(itemId);
        }

        private async Task CalculateCartSummaryTotals()
        {
            SetTotalPrice();
            SetTotalQuantity();
        }

        private string SetTotalPrice()
        {
            this.TotalPrice = this.CartItems.Sum(p => p.TotalPrice).ToString("C");
            return TotalPrice;
        }
        private int SetTotalQuantity()
        {
            this.TotalQuantity = this.CartItems.Sum(p => p.Qty);
            return TotalQuantity;
        }

        private CartItemDto getCartItem(int id)
        {
            return CartItems.FirstOrDefault(i => i.Id == id);
        }
        private void removeCartItem(int id)
        {
            var cartItemDto = getCartItem(id);
            CartItems.Remove(cartItemDto);
            CartChanged();
        }
        protected  async Task updateItemQty_Click(int itemId,int qty)
        {
            if(qty > 0)
            {
                CartItemQtyUpdateDto cartItemQtyUpdateDto = new CartItemQtyUpdateDto { CartItemId = itemId, Qty = qty };

                var returnedCartItemQtyDto = await ShopCartService.UpdateQty(cartItemQtyUpdateDto);
                CartChanged();    
                updateItemTotalPrice(itemId);
                MakeUpdateQtyButtonVisible(itemId, false);


            }
            else
            {
                var item = CartItems.FirstOrDefault(i=> i.Id == itemId);
                if(item != null)
                {
                    item.Qty = 1;
                    item.TotalPrice = item.Price;
                    CartChanged();

                }
            }



        }  
        private void updateItemTotalPrice(int itemId)
        {
            CartItemDto cartItemDto = getCartItem(itemId);
            if(cartItemDto != null) {
                cartItemDto.TotalPrice = cartItemDto.Price * cartItemDto.Qty;
            }
        }
        private async void CartChanged()
        {
           await CalculateCartSummaryTotals();
            ShopCartService.RaiseEventOnShoppingCartChanged(TotalQuantity);
        }
        
        public void OnPayment_Click()
        {
            NavigationManager.NavigateTo("/checkout");
        }

    }
}
