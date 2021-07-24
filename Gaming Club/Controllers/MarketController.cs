using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gaming_Club.Models;
using Microsoft.AspNet.Identity;

namespace Gaming_Club.Controllers
{
     public class MarketController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Market
        public ActionResult Index(string SortOrder)
        {
            if (!(User.Identity.IsAuthenticated))
            {
                return RedirectToAction("ErrorUnathorised");
            }

            //to allow for a dynamic model link
            DynamicModel multimodel = new DynamicModel
            {
                Market = SortTable(SortOrder).ToList(),
                Users = db.Users.ToList(),
                Games = db.Games.ToList()
            };

            return View(multimodel);
        }

        // POST: Market/IndexEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            Guid itemNum = Guid.Parse(Request["item.SalesGUID"]);
            int Value = int.Parse(Request["item.Quantity"]);
            //!!!! List<Guid?> basket = new List<Guid?>();
            Dictionary<Guid?, int> basket = new Dictionary<Guid?, int>();

            if (Session["basket"] != null)
            {
                basket = (Dictionary<Guid?, int>)Session["basket"];
            }

            basket.Add(itemNum, Value);
            Session["basket"] = basket;

            return RedirectToAction("Index", "Market");
        }

        // GET: Market/Details/
        public ActionResult Details(Guid? id)
        {
            if (!(User.Identity.IsAuthenticated))
            {
                return RedirectToAction("ErrorUnathorised");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DynamicModel multimodel = new DynamicModel()
            {
                marketItem = db.Market.Find(id),
                Games = db.Games.ToList()
            };
            if (multimodel.marketItem == null)
            {
                return HttpNotFound();
            }
            return View(multimodel);
        }

        // GET: Market/Create
        public ActionResult Create()
        {
            if (!(User.Identity.IsAuthenticated))
            {
                return RedirectToAction("ErrorUnathorised");
            }

            Dictionary<string, string> gamelist = new Dictionary<string, string>();
            foreach (var game in db.Games.ToList())
            {                   
                gamelist.Add(game.GameGUID.ToString(), game.GameName);
            }

            ViewBag.myGames = gamelist;

            return View();
        }

        // POST: Market/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesGUID,ItemName,ItemDescription,GameGUID,Quantity,PPU,UserId,ListedDate")] Market market)
        {
            if (!(User.Identity.IsAuthenticated))
            {
                return RedirectToAction("ErrorUnathorised");
            }
            if (ModelState.IsValid)
            {
                market.ListedDate = DateTime.Now;

                db.Market.Add(market);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(market);
        }

        // GET: Market/Edit/
        public ActionResult Edit(Guid? id)
        {
            if (!(User.Identity.IsAuthenticated))
            {
                return RedirectToAction("ErrorUnathorised");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<string, string> gamelist = new Dictionary<string, string>();
            foreach (var game in db.Games.ToList())
            {
                gamelist.Add(game.GameGUID.ToString(), game.GameName);
            }

            ViewBag.mydict = gamelist;

            Market market = db.Market.Find(id);
            if (market == null)
            {
                return HttpNotFound();
            }
            if (market.UserId == User.Identity.GetUserId())
            {
                return View(market);
            }
            else
            {
                return RedirectToAction("ErrorDefault");
            }
        }

        // POST: Market/Edit/
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesGUID,ItemName,ItemDescription,GameGUID,Quantity,PPU,UserId,ListedDate")] Market market)
        {
            if (!(User.Identity.IsAuthenticated))
            {
                return RedirectToAction("ErrorUnathorised");
            }
            if (ModelState.IsValid)
            {
                db.Entry(market).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(market);
        }

        // GET: Market/Delete/
        public ActionResult Delete(Guid? id)
        {
            if (!(User.Identity.IsAuthenticated))
            {
                return RedirectToAction("ErrorUnathorised");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Market market = db.Market.Find(id);
            Games games = db.Games.Find(market.GameGUID);

            ViewBag.game = games.GameName;

            if (market == null)
            {
                return HttpNotFound();
            }
            return View(market);
        }

        // POST: Market/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid? id)
        {
            if (!(User.Identity.IsAuthenticated))
            {
                return RedirectToAction("ErrorUnathorised");
            }
            Market market = db.Market.Find(id);
            db.Market.Remove(market);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();

            }
            base.Dispose(disposing);
        }

        public ActionResult ErrorUnathorised()
        {
            return View("~/Views/Shared/ErrorUnauthorised.cshtml");
        }

        public ActionResult ErrorDefault()
        {
            return View("Error");
        }

        public IQueryable<Market> SortTable(string SortOrder)
        {
            //initialse sort
            ViewBag.ItemName = string.IsNullOrEmpty(SortOrder) ? "ItemName_desc" : "";
            ViewBag.ItemDescription = SortOrder == "ItemDescription" ? "ItemDescription_desc" : "ItemDescription";
            ViewBag.Game = SortOrder == "Game" ? "Game_desc" : "Game";
            ViewBag.Quantity = SortOrder == "Quantity" ? "Quantity_desc" : "Quantity";
            ViewBag.PPU = SortOrder == "PPU" ? "Price_desc" : "PPU";
            ViewBag.Username = SortOrder == "Username" ? "Username_desc" : "Username";
            ViewBag.ListedDate = SortOrder == "ListedDate" ? "ListedDate_desc" : "ListedDate";

            var SortType = from sort in db.Market
                           select sort;
            switch (SortOrder)
            {
                case "ItemName_desc":
                    SortType = SortType.OrderByDescending(s => s.ItemName);
                    break;
                case "ItemDescription":
                    SortType = SortType.OrderBy(s => s.ItemDescription);
                    break;
                case "ItemDescription_desc":
                    SortType = SortType.OrderByDescending(s => s.ItemDescription);
                    break;
                case "Game":
                    SortType = SortType.OrderBy(s => s.GameGUID);
                    break;
                case "Game_desc":
                    SortType = SortType.OrderByDescending(s => s.GameGUID);
                    break;
                case "Quantity":
                    SortType = SortType.OrderBy(s => s.Quantity);
                    break;
                case "Quantity_desc":
                    SortType = SortType.OrderByDescending(s => s.Quantity);
                    break;
                case "PPU":
                    SortType = SortType.OrderBy(s => s.PPU);
                    break;
                case "Price_desc":
                    SortType = SortType.OrderByDescending(s => s.PPU);
                    break;
                case "Username":
                    SortType = SortType.OrderBy(s => s.UserId);
                    break;
                case "Username_desc":
                    SortType = SortType.OrderByDescending(s => s.UserId);
                    break;
                case "ListedDate":
                    SortType = SortType.OrderBy(s => s.ListedDate);
                    break;
                case "ListedDate_desc":
                    SortType = SortType.OrderByDescending(s => s.ListedDate);
                    break;
                default:
                    SortType = SortType.OrderBy(s => s.ItemName);
                    break;
            }
            return SortType;
        }
            

}
}
