using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvcEntities.CustomModel;
using mvcEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrderingApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IFoodOrderingComponent _foodOrderingComponent;

        public HomeController( IFoodOrderingComponent foodOrderingComponent)
        {
            _foodOrderingComponent = foodOrderingComponent;
        }

        public IActionResult Index()
        {
            ViewBag.Selected = "Index";
            return View();
        }
        public IActionResult AddMenu()
        {
            return View();
        }

        public IActionResult Menu()
        {
            ViewBag.Selected = "Menu";
            var list = _foodOrderingComponent.GetCategory();
            return View(list);
        }
        public IActionResult OrderView(long id)
        {
            CustomMenu customMenu=_foodOrderingComponent.GetCategory().FirstOrDefault(x=>x.MenuId==id);
            return View(customMenu);
        }
        public IActionResult ViewOrder()
        {
            ViewBag.Selected = "ViewOrder";
            return View();
        }
        public IActionResult ReservationSuccessfull(Reservation res)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _foodOrderingComponent.ReserveTable(res);
                    return View();
                }
                else
                {
                    return View("Reservation");
                }
            }                                
            catch(Exception e)
            {
                return View("Reservation");
            }
        }
        public IActionResult Gallery()
        {
            ViewBag.Selected = "Gallery";
            return View();
        }
        public IActionResult Reservation()
        {
            ViewBag.Selected = "Reservation";
            return View();
        }
        public IActionResult PayNow(long id,long categoryId)
        {
           if(ModelState.IsValid)
            {
                List<ModeOfPayment> modeOfPayments = _foodOrderingComponent.GetPaymentMode();

                List<string> mode = new List<string>();
                foreach (var item in modeOfPayments)
                {
                    mode.Add(item.Mode);
                }
                ViewBag.payment = mode;
            }
            PaymentCustom payment = _foodOrderingComponent.GetPrice().FirstOrDefault(x => x.MenuId == id);
            payment.Email = string.Empty;
            payment.Address = string.Empty;
            payment.ModeId = 0;
            payment.MenuId = id;
            payment.CategoryId = categoryId;
            return View(payment);
        }
        public IActionResult OrderHistory()
        {
            var loggedInUserId = User.Claims.FirstOrDefault(x => x.Type == "LoggedInUserId")?.Value;
            var currentUser = _foodOrderingComponent.GetPastOrders().Where(x => x.UserId == Convert.ToInt64(loggedInUserId)).ToList();
            return View(currentUser);
        }
        public IActionResult PaymentSuccessfull(PaymentCustom pay)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Payment payment = new Payment()
                    {
                        MenuId = pay.MenuId,
                        Address = pay.Address,
                        Email = pay.Email,
                        ModeId = pay.ModeId
                    };
                    _foodOrderingComponent.AddPayment(payment);

                    var loggedInUserId = User.Claims.FirstOrDefault(x => x.Type == "LoggedInUserId")?.Value;
                    OrderHistory orderHistory = new OrderHistory()
                    {
                        UserId = Convert.ToInt64(loggedInUserId),
                        ItemId = pay.MenuId,
                        CategoryId = pay.CategoryId
                    };
                    _foodOrderingComponent.AddOrder(orderHistory);
                    return View();
                }
                else
                {
                    return View("PayNow");
                }
            }
            catch (Exception e)
            {
                return View("PayNow");
            }
        }
        public IActionResult About()
        {
            ViewBag.Selected = "About";
            return View();
        }
        public IActionResult Contact()
        {
            ViewBag.Selected = "Contact";
            return View();
        }
    }
}
