using MyWebApp.Models;
using System.Web.Mvc;

namespace MyWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string message = Session["message"]?.ToString() ?? "";
            return View(new MyForm { Message = message });
        }

        [HttpPost]
        public ActionResult Index(MyForm item)
        {
            Session["message"] = item.Message;
            return RedirectToAction("Index");
        }
    }
}