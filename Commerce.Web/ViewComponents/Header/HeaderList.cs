using Commerce.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Web.ViewComponents.Header
{
    public class HeaderList : ViewComponent
    {
        private readonly IUserProductService _userProductService;
        public HeaderList(IUserProductService userProductService)
        {
            _userProductService = userProductService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_userProductService.CartViewModel());
        }
    }
}
