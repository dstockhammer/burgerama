using System.Web.Mvc;

namespace Burgerama.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Burgerama";

            return View();
        }
    }
}
