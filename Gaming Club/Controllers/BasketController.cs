using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Gaming_Club.Models;

namespace Gaming_Club.Controllers
{
    public class BasketController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Basket
        public ActionResult Index()
        {
            if (!(User.Identity.IsAuthenticated))
            {
                return RedirectToAction("ErrorUnathorised","Market");
            }

            List<Market> marketlist = new List<Market>();
            DynamicModel multimodel = new DynamicModel 
            {
                Games = db.Games.ToList(), 
                Users = db.Users.ToList() 
            };

            if (Session["basket"] != null)
            {
                
                //key - guid of item
                //value - quantity in basket
                Dictionary<Guid?, int> basket = new Dictionary<Guid?, int>();
                                
                basket = (Dictionary<Guid?, int>) Session["basket"];
                foreach (var basketItem in basket)
                {
                    Market market = db.Market.Find((Guid)basketItem.Key);
                    market.Quantity = basketItem.Value;
                    marketlist.Add(market);
                }

                multimodel.Market = marketlist;
            }
            else
            {
                return RedirectToAction("BasketEmptyError");
            }
            return View(multimodel);
        }

        // GET: Basket/Delete
        public ActionResult Delete(Guid? id)
        {
            if (!(User.Identity.IsAuthenticated))
            {
                return RedirectToAction("ErrorUnathorised", "Market");
            }
            Dictionary<Guid?, int> basket = new Dictionary<Guid?, int>();

            if (Session["basket"] != null)
            {
                basket = (Dictionary<Guid?, int>)Session["basket"];
            }

            basket.Remove(id);
            Session["basket"] = basket;

            return RedirectToAction("Index", "Basket");
        }

        // GET: Basket/OrderConfirmed
        public ActionResult OrderConfirmed()
        {
            if (!(User.Identity.IsAuthenticated))
            {
                return RedirectToAction("ErrorUnathorised", "Market");
            }
            List<Market> marketlist = new List<Market>();
            DynamicModel multimodel = new DynamicModel { Games = db.Games.ToList() };

            if (Session["basket"] != null)
            {

                Dictionary<Guid?, int> basket = new Dictionary<Guid?, int>();
                basket = (Dictionary<Guid?, int>) Session["basket"];

                foreach (var basketItem in basket)
                {
                    Market market = db.Market.Find((Guid)basketItem.Key);
                    market.Quantity = basketItem.Value;
                    marketlist.Add(market);
                }

                multimodel.Market = marketlist;
            }
            else
            {
                return RedirectToAction("Error");
            }

            return View(multimodel);
        }

        public ActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
        public ActionResult BasketEmptyError()
        {
            return View("~/Views/Shared/BasketEmptyError.cshtml");
        }
    }
}

