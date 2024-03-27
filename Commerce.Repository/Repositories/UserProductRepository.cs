using Commerce.Core.Entities;
using Commerce.Core.Repositories;
using Commerce.Core.ViewModels;
using Commerce.Repository.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Repository.Repositories
{
    public class UserProductRepository : GenericRepository<UserProduct>, IUserProductRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserProductRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddCart(UserProduct userProduct)
        {
            var userProducts = _context.UserProducts.ToList();
            var userId = _httpContextAccessor.HttpContext!.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            var productId = userProduct.ProductId;
            var product = _context.Products.Find(productId);

            var existingProductCart = userProducts
                .FirstOrDefault(x => x.ProductId == productId && x.AppUserId == userId);

            if (existingProductCart != null)
            {
                existingProductCart.Count++;
                existingProductCart.Amount = product!.Price * existingProductCart.Count;
                _context.UserProducts.Update(existingProductCart);
                _context.SaveChanges();
            }

            else
            {
                var newCartProduct = new UserProduct
                {
                    ProductId = productId,
                    AppUserId = userId,
                    Count = 1,
                    Amount = product!.Price
                };

                _context.UserProducts.Add(newCartProduct);
                _context.SaveChanges();
            }
        }

        public List<CartViewModel> CartViewModel()
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            var appUserProducts = LoggedUserCart();

            var totalAmount = _context.Carts
                .Where(x => x.AppUserId == userId)
                .Select(x => x.TotalPrice)
                .Sum();

            var cartViewModels = new List<CartViewModel>();

            foreach (var userProduct in appUserProducts)
            {
                var product = _context.Products.Find(userProduct.ProductId);

                if (product != null)
                {
                    var cartViewModel = new CartViewModel
                    {
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        ImageUrl = product.ImageUrl,
                        Count = userProduct.Count,
                        TotalPrice = totalAmount,
                        ProductId = product.Id
                    };

                    cartViewModels.Add(cartViewModel);
                }
            }

            return cartViewModels;
        }

        public void DeleteAllProduct()
        {
            var userProducts = LoggedUserCart();

            foreach (var userProduct in userProducts)
            {
                _context.UserProducts.Remove(userProduct);
                _context.SaveChanges();
            }
        }

        public bool DeleteCart(UserProduct userProduct)
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            var productId = userProduct.ProductId;
            var product = _context.Products.Find(productId);

            var existingProductCart = _context.UserProducts
                .FirstOrDefault(x => x.ProductId == productId && x.AppUserId == userId);

            existingProductCart!.Count--;

            if (existingProductCart.Count == 0)
                _context.UserProducts.Remove(existingProductCart);

            else
            {
                existingProductCart.Amount = existingProductCart.Count * product!.Price;
                _context.UserProducts.Update(existingProductCart);
            }

            var cart = _context.Carts.FirstOrDefault(x => x.AppUserId == userId);

            if (cart != null)
            {
                cart.TotalPrice = _context.UserProducts
                    .Where(x => x.AppUserId == userId)
                    .Sum(x => x.Amount);

                _context.Carts.Update(cart);
            }

            _context.SaveChanges();

            var totalProductCount = _context.UserProducts.
                Where(x => x.AppUserId == userId).
                Sum(x => x.Count);

            return totalProductCount != 0;
        }

        public void DeleteProductCart(UserProduct userProduct)
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            var productId = userProduct.ProductId;
            var product = _context.Products.Find(productId);

            var existingProductCart = _context.UserProducts
                .FirstOrDefault(x => x.ProductId == productId && x.AppUserId == userId);

            _context.UserProducts.Remove(existingProductCart!);
            _context.SaveChanges();

            var cart = _context.Carts.FirstOrDefault(x => x.AppUserId == userId);

            if (cart != null)
            {
                cart.TotalPrice = _context.UserProducts
                    .Where(x => x.AppUserId == userId)
                    .Sum(x => x.Amount);

                _context.Carts.Update(cart);
            }

            _context.SaveChanges();
        }

        public List<UserProduct> LoggedUserCart()
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            return _context.UserProducts
                .Where(x => x.AppUserId == userId)
                .ToList();
        }
    }
}
