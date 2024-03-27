using Commerce.Core.Entities;
using Commerce.Core.Repositories;
using Commerce.Core.Services;
using Commerce.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Service.Services
{
    public class CartService : GenericService<Cart>, ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(IGenericRepository<Cart> repository, IUnitOfWork unitOfWork, ICartRepository cartRepository) : base(repository, unitOfWork)
        {
            _cartRepository = cartRepository;
        }

        public void AddCart(Cart cart)
        {
            _cartRepository.AddCart(cart);
        }

        public Cart LoggedUserCart()
        {
            return _cartRepository.LoggedUserCart();
        }
    }
}
