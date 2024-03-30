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

        public IActionResult AddProductMyCart(int productId, UserProduct userProduct)
        {
            var product = _productService.GetById(productId);
            userProduct.ProductId = product.Id;
            _userProductService.AddCart(userProduct);

            return RedirectToAction(nameof(CartController.AddCart), "Cart");
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

        public IActionResult AllDeleteCart()
        {
            _userProductService.DeleteAllProduct();
            return RedirectToAction(nameof(CartController.DeleteCart), "Cart");
        }

        public IActionResult DeleteCart(int productId, UserProduct userProduct)
        {
            var product = _productService.GetById(productId);
            userProduct.ProductId = product.Id;

            var isSucess = _userProductService.DeleteCart(userProduct);

            if (isSucess)
                return RedirectToAction(nameof(UserProductController.Main), "UserProduct");
            else
                return RedirectToAction(nameof(CartController.DeleteCart), "Cart");
        }

        public IActionResult Main()
        {
            return View();
        }
        public IActionResult MainPartial()
        {
            return View(_productService.GetAll());
        }
    }
}
