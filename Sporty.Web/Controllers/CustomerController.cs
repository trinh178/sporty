using Sporty.Services;
using Sporty.Web.Commons.Attributes;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AutoMapper;
using Sporty.Web.Models;
using System.Collections.Generic;
using Sporty.DAL.Models;

namespace Sporty.Web.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private IScheduleOrderService _scheduleOrderService;
        private IFieldService _fieldService;

        public CustomerController(IScheduleOrderService scheduleOrderService, IFieldService fieldService)
        {
            _scheduleOrderService = scheduleOrderService;
            _fieldService = fieldService;
        }

        public ActionResult ScheduleOrder()
        {
            var s = _scheduleOrderService.List(User.Identity.GetUserId());

            var m = Mapper.Map<List<ScheduleOrderViewModel>>(s, otp => otp.AfterMap((src, des) =>
            {
                var ss = src as List<ScheduleOrder>;
                var ms = des as List<ScheduleOrderViewModel>;

                for (int i = 0; i < ss.Count; i++)
                {
                    var f = _fieldService.GetDetail(ss[i].FieldId);
                    ms[i].PlaceId = f.PlaceId;
                }
            }));
            return View(m);
        }
    }
}