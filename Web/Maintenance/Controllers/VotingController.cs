using System.Web.Mvc;

namespace Burgerama.Web.Maintenance.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class VotingController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Voting";

            return View();
        }

        public ActionResult CreateContext()
        {
            ViewBag.Title = "Create Voting Context";

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
