using System.Web.Mvc;

namespace Burgerama.Web.Maintenance.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OutingsController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Outings";

            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Create Outing";

            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
