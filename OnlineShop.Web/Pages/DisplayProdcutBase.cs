using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;

namespace OnlineShop.Web.Pages
{
    public class DisplayProdcutBase:ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
