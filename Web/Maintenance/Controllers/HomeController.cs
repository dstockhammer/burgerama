using System.Web.Mvc;

namespace Burgerama.Web.Maintenance.Controllers
{
    [Authorize]
    public sealed class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Maintenance Panel";

            return View();
        }
    }
}