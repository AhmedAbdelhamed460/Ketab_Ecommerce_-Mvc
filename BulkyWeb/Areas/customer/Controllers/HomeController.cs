using Ketab_DataAcces.IRepository;
using Ketab_DataAcces.Repository;
using Ketab_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Diagnostics;
using System.Security.Claims;

namespace Ketab_Web.Areas.customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger , IUnitOfWork unitOfWork )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IStatusCodeActionResult Index()
        {
            IEnumerable<Product> ProductList = _unitOfWork.Product.GetAll(IncludeProp : "Category");
            return View(ProductList);
        }
		public IStatusCodeActionResult Details(int id)
		{

            ShoppingCart cart = new ShoppingCart()
            {
                Product = _unitOfWork.Product.Get(n => n.Id == id, IncludeProp: "Category"),
                Count = 1,
                productId = id 


            };

        
			
			return View(cart);
		}


        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart cart)
        {
            cart.Id = 0;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

           cart.ApplicationUserId = userId;
            ShoppingCart CartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId && u.productId == cart.productId);

            if(CartFromDb != null)
            {
                //Cart exists
                CartFromDb.Count += cart.Count;
                _unitOfWork.ShoppingCart.update(CartFromDb);

            }
            else
            {
                // add new record 
                _unitOfWork.ShoppingCart.add(cart);
            }

            TempData["success"] = "Cart Create successFully";

            _unitOfWork.save();
            return RedirectToAction(nameof(Index));

        }
		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}