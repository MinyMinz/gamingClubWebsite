using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gaming_Club.Models;
using Microsoft.AspNet.Identity;

namespace Gaming_Club.Controllers
{
    public class GamesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Games
        public ActionResult Index()
        {
            if (User.Identity.Name == "Admin"){
                return View(db.Games.ToList());
            }
            else
            {
                return RedirectToAction("ErrorUnathorised", "Market");
            }
        }

      
        // GET: Games/Create
        public ActionResult Create()
        {
            if(User.Identity.Name == "Admin"){
                return View();
            }
            else
            {
                return RedirectToAction("ErrorUnathorised", "Market");
            }
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameGUID,GameName")] Games games)
        {
            if (User.Identity.Name == "Admin")
            {
                if (ModelState.IsValid)
                {
                    games.GameGUID = Guid.NewGuid();
                    db.Games.Add(games);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(games);
            }
            else
            {
                return RedirectToAction("ErrorUnathorised", "Market");
            }
        }

        // GET: Games/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (User.Identity.Name == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Games games = db.Games.Find(id);
                if (games == null)
                {
                    return HttpNotFound();
                }
                return View(games);
            }
            else
            {
                return RedirectToAction("ErrorUnathorised", "Market");
            }
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameGUID,GameName")] Games games)
        {
            if (User.Identity.Name == "Admin")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(games).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(games);
            }
            else
            {
                return RedirectToAction("ErrorUnathorised", "Market");
            }
        }

        // GET: Games/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (User.Identity.Name == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Games games = db.Games.Find(id);
                if (games == null)
                {
                    return HttpNotFound();
                }

                return View(games);
            }
            else
            {
                return RedirectToAction("ErrorUnathorised", "Market");
            }
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (User.Identity.Name == "Admin")
            {
                Games games = db.Games.Find(id);
                db.Games.Remove(games);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("ErrorUnathorised", "Market");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
