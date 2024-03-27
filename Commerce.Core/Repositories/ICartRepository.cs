using Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Core.Repositories
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        void AddCart(Cart cart);
        Cart LoggedUserCart();
    }
}
