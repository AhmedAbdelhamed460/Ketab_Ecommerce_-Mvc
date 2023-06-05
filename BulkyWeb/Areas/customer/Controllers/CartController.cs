using Ketab_DataAcces.IRepository;
using Ketab_Models;
using Ketab_Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ketab_Web.Areas.customer.Controllers
{
    [Area(nameof(customer))]
    public class CartController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public ShopingCartVM ShopingCartVM { get; set; }
        public CartController( IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShopingCartVM cartVM = new ShopingCartVM()
            {



                shoppingCartsList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId,IncludeProp :"Product")
            };

            foreach (var cart in cartVM.shoppingCartsList)
            {
                cart.Price = GetPriceBasedonQuantity(cart);
                cartVM.orderList += (cart.Price * cart.Count);
            }
         
                return View(cartVM);
        }



        public IActionResult Plus(int? CartId)
        {
            var CartFDB = _unitOfWork.ShoppingCart.Get(n=>n.Id== CartId);
            CartFDB.Count += 1;
            _unitOfWork.ShoppingCart.update(CartFDB);
            _unitOfWork.save();
            return RedirectToAction(nameof(Index));

        }



        public IActionResult Remove(int? CartId)
        {
            var CartFDB = _unitOfWork.ShoppingCart.Get(n => n.Id == CartId);
            
            _unitOfWork.ShoppingCart.remove(CartFDB);
            _unitOfWork.save();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Minus(int? CartId)
        {
            var CartFDB = _unitOfWork.ShoppingCart.Get(n => n.Id == CartId);
            if(CartFDB.Count <= 1)
            {
                _unitOfWork.ShoppingCart.remove(CartFDB);
            }
            else
            {
                CartFDB.Count -= 1;
                _unitOfWork.ShoppingCart.update(CartFDB);
            }
           
            _unitOfWork.save();
            return RedirectToAction(nameof(Index));

        }


        public IActionResult Summary()
        {
            return View();
        }



        private double GetPriceBasedonQuantity( ShoppingCart shoppingCart)
        {
            if(shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.Price;
            }
            else
            {
                if (shoppingCart.Count <= 100)
                {
                    return shoppingCart.Product.Price50;

                }
                else
                {
                    return shoppingCart.Product.Price100;
                }

            }
        }
    }
}
