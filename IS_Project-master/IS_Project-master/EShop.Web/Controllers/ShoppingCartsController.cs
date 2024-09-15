using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Domain.Domain;
using EShop.Repository;
using EShop.Service.Interface;
using System.Security.Claims;
using EShop.Service.Implementation;
using Stripe;
using EShop.Domain.Payment;
using Microsoft.Extensions.Options;

namespace EShop.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
     
        private readonly IShoppingCartService shoppingCartService;
        private readonly StripeSettings _stripeSettings;
        public ShoppingCartsController(IShoppingCartService shoppingCartServicet,IOptions<StripeSettings> stripeSettings)
        {
            this.shoppingCartService = shoppingCartServicet;
            _stripeSettings = stripeSettings.Value; 

        }

        // GET: ShoppingCarts
        public  IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dto = shoppingCartService.getShoppingCartInfo(userId);
            return View(dto);
        }

        public IActionResult DeleteFromShoppingCart(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            shoppingCartService.deleteProductFromShoppingCart(userId, id);

            return RedirectToAction("Index");

        }
        public IActionResult order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = shoppingCartService.orderProducts(userId);
            //if (result == true)
            return RedirectToAction("Index", "ShoppingCarts");


        }

        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
            var customerService = new CustomerService();
            var chargeService = new ChargeService();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

            var cart = shoppingCartService.getShoppingCartInfo(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(cart.TotalPrice) * 100),
                Description = "EShop Application Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                this.order();
                return RedirectToAction("SuccessPayment");

            }
            else
            {
                return RedirectToAction("NotsuccessPayment");
            }

            return null;
        }

        public IActionResult SuccessPayment()
        {
            return View();
        }


    }
}
