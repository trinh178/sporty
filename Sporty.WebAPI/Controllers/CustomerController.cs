using Sporty.WebAPI.Commons.Attributes;
using System.Web.Mvc;

namespace Sporty.WebAPI.Controllers
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