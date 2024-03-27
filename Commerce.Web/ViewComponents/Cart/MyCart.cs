using Commerce.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Web.ViewComponents.Cart
{
    public class MyCart : ViewComponent
    {
        private readonly IUserProductService _userProductService;
        public MyCart(IUserProductService userProductService)
        {
            _userProductService = userProductService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_userProductService.CartViewModel());
        }
    }
}
