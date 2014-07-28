using System;
using System.Diagnostics.Contracts;
using System.Web.Mvc;
using Burgerama.Messaging.Commands;
using Burgerama.Messaging.Commands.Ratings;
using Burgerama.Web.Maintenance.Models.Ratings;

namespace Burgerama.Web.Maintenance.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RatingsController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public RatingsController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Ratings";

            return View();
        }

        public ActionResult CreateContext()
        {
            ViewBag.Title = "Create Rating Context";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContext(CreateContextModel model)
        {
            Contract.Requires<ArgumentNullException>(model != null);

            if (ModelState.IsValid == false)
                return View();

            _commandDispatcher.Send(new CreateContext
            {
                ContextKey = model.ContextKey,
                GracefullyHandleUnknownCandidates = model.GracefullyHandleUnknownCandidates
            });

            return RedirectToAction("Index");
        }
        
        public ActionResult Synchronize()
        {
            ViewBag.Title = "Syncronize Candidates";

            return View();
        }
    }
}
