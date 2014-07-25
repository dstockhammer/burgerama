using System.Web.Mvc;

namespace Burgerama.Web.Maintenance.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RatingController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Rating";

            return View();
        }

        public ActionResult CreateContext()
        {
            ViewBag.Title = "Create Rating Context";

            return View();
        }

        [HttpPost]
        public ActionResult CreateContext(FormCollection collection)
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
        
        public ActionResult Synchronize()
        {
            ViewBag.Title = "Syncronize Candidates";

            return View();
        }
    }
}
