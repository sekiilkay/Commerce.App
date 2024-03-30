using Commerce.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Web.ViewComponents.Product
{
    public class ProductList : ViewComponent
    {
        private readonly IProductService _productService;
        public ProductList(IProductService productService)
        {
            _productService = productService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_productService.GetAll()); ;
        }
    }
}
