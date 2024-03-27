using Commerce.Core.Entities;
using Commerce.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Core.Services
{
    public interface IUserProductService : IGenericService<UserProduct>
    {
        List<UserProduct> LoggedUserCart();
        void AddCart(UserProduct userProduct);
        bool DeleteCart(UserProduct userProduct);
        void DeleteProductCart(UserProduct userProduct);
        List<CartViewModel> CartViewModel();
        void DeleteAllProduct();
    }
}
