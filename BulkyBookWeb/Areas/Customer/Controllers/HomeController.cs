using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BulkyBook.Models;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using BulkyBook.Utility;

namespace BulkyBookWeb.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index( )
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return View(productList);
        }

        public IActionResult Details(int productId)
        {
            ShoppingCart cartObj = new ShoppingCart
            {
                Count = 1,
                ProductId = productId,
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Category,CoverType"),
            };

            return View(cartObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

            //Get record based on condidation. Later we will use this record to determine whethear this record
            //has been already added to shooping cart by user (or he/she may want to modify count of item)
            //so we don't want a new insertion in ShoopingCart table.
            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId);

            if (cartFromDb == null)//this is added for the first time in shopingCart
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();
                //    HttpContext.Session.SetInt32(SD.SessionCart,
                //        _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count);
            }

            // count modified. so, added the new number of item.
            // count modified. so, added the new number of item.
            else
            {
                _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, shoppingCart.Count);
                _unitOfWork.Save();
            }

            return RedirectToAction(nameof(Index));
        }

















        public IActionResult Privacy( )
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error( )
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
