using Commerce.Core.Entities;
using Commerce.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Web.Controllers
{
    [Authorize]
    public class UserProductController : Controller
    {
        private readonly IUserProductService _userProductService;
        private readonly IProductService _productService;
        public UserProductController(IUserProductService userProductService, IProductService productService)
        {
            _userProductService = userProductService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(_userProductService.CartViewModel());
        }
        public IActionResult AddCart(int productId)
        {
            TempData["ProductId"] = productId;
            return View(_productService.GetById(productId));
        }

        [HttpPost]
        public IActionResult AddCart(UserProduct userProduct)
        {
            var productId = (int)TempData["ProductId"]!;
            userProduct.ProductId = productId;
            _userProductService.AddCart(userProduct);

            return RedirectToAction(nameof(CartController.AddCart), "Cart");
        }
    }
}
