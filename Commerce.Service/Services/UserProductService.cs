using Commerce.Core.Entities;
using Commerce.Core.Repositories;
using Commerce.Core.Services;
using Commerce.Core.UnitOfWork;
using Commerce.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Service.Services
{
    public class UserProductService : GenericService<UserProduct>, IUserProductService
    {
        private readonly IUserProductRepository _userProductRepository;
        public UserProductService(IGenericRepository<UserProduct> repository, IUnitOfWork unitOfWork, IUserProductRepository userProductRepository) : base(repository, unitOfWork)
        {
            _userProductRepository = userProductRepository;
        }

        public void AddCart(UserProduct userProduct)
        {
            _userProductRepository.AddCart(userProduct);
        }

        public List<UserProduct> LoggedUserCart()
        {
            return _userProductRepository.LoggedUserCart();
        }

        public List<CartViewModel> CartViewModel()
        {
            return _userProductRepository.CartViewModel();
        }

        public bool DeleteCart(UserProduct userProduct)
        {
            return _userProductRepository.DeleteCart(userProduct);
        }

        public void DeleteAllProduct()
        {
            _userProductRepository.DeleteAllProduct();
        }

        public void DeleteProductCart(UserProduct userProduct)
        {
            _userProductRepository.DeleteProductCart(userProduct);
        }
    }
}
