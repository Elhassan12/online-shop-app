using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;

namespace OnlineShop.Web.Components
{
    public partial class ItemCard
    {
        [Parameter]
        public ProductDto Product { get; set; }

    }
}
