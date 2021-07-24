//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace Gaming_Club.Controllers
//{
//    public class AddToBasketController : Controller
//    {
//        DataTable dt;
//        MobiledetailDAL _mdal = new MobiledetailDAL();

//        // GET: AddToCart  
//        public ActionResult Add(Mobiles mo)
//        {

//            if (Session["cart"] == null)
//            {
//                List<Mobiles> li = new List<Mobiles>();

//                li.Add(mo);
//                Session["cart"] = li;
//                ViewBag.cart = li.Count();


//                Session["count"] = 1;


//            }
//            else
//            {
//                List<Mobiles> li = (List<Mobiles>)Session["cart"];
//                li.Add(mo);
//                Session["cart"] = li;
//                ViewBag.cart = li.Count();
//                Session["count"] = Convert.ToInt32(Session["count"]) + 1;

//            }
//            return RedirectToAction("Index", "Home");
//        }
//}
