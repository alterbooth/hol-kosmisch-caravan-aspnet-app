using MyWebApp.Models;
using System.Web.Mvc;

namespace MyWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = Session["message"]?.ToString() ?? "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(MyForm item)
        {
            Session["message"] = item.Message;
            return RedirectToAction("Index");
        }
    }
}