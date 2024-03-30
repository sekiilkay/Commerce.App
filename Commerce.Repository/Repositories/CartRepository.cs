using Commerce.Core.Entities;
using Commerce.Core.Repositories;
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
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Cart LoggedUserCart()
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            return (_context.Carts
                .FirstOrDefault(x => x.AppUserId == userId))!;
        }
        public void AddCart(Cart cart)
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            var userProductsCart = _context.UserProducts
                .Where(up => up.AppUserId == userId)
                .ToList();

            decimal totalPriceCart = userProductsCart.Sum(up => up.Amount);

            var existingCart = _context.Carts
                .FirstOrDefault(c => c.AppUserId == userId);

            if (existingCart != null)
            {
                existingCart.TotalPrice = totalPriceCart;
                existingCart.AppUserId = userId;
            }

            else
            {
                var newCart = new Cart
                {
                    AppUserId = userId,
                    TotalPrice = totalPriceCart
                };

                _context.Carts.Add(newCart);
            }

            _context.SaveChanges();
        }
    }
}
