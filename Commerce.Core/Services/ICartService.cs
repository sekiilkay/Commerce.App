using Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Core.Services
{
    public interface ICartService : IGenericService<Cart>
    {
        void AddCart(Cart cart);
        Cart LoggedUserCart();
    }
}
