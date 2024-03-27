using Commerce.Core.Entities;
using Commerce.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IUserProductService _userProductService;
        public CartController(ICartService cartService, IUserProductService userProductService)
        {
            _cartService = cartService;
            _userProductService = userProductService;
        }

        public IActionResult AddCart(Cart cart)
        {
            _cartService.AddCart(cart);
            return RedirectToAction(nameof(UserProductController.Index), "UserProduct");
        }

        public IActionResult DeleteCart()
        {
            var cart = _cartService.LoggedUserCart();
            _cartService.Delete(cart);
            return RedirectToAction(nameof(UserProductController.Index), "UserProduct");
        }
    }
}
