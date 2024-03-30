using Commerce.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        public ProductController()
        {
        }
    }
}
