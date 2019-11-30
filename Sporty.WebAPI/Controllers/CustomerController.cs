using Sporty.Web.Commons.Attributes;
using System.Web.Mvc;

namespace Sporty.Web.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        public ActionResult ScheduleOrder()
        {
            return View();
        }
    }
}