using AutoMapper;
using Sporty.Services;
using Sporty.Services.Exceptions;
using Sporty.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http;

namespace Sporty.Web.API.Controllers
{
    [RoutePrefix("api/schedule-order")]
    public class ScheduleOrderController : ApiController
    {
        private IScheduleOrderService _scheduleOrderService;

        public ScheduleOrderController(IScheduleOrderService scheduleOrderService)
        {
            this._scheduleOrderService = scheduleOrderService;
        }

        [Route("get-all-possible-schedule-order-time")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAllPossibleScheduleOrderTime([FromUri] GetAllPossibleScheduleOrderTimeBingdingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                try
                {
                    var date = DateTime.ParseExact(model.DateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    var lst = this._scheduleOrderService.GetAllPossibleScheduleOrderTime(model.FieldId, date);

                    var possibleOrder = Mapper.Map<List<PossibleScheduleOrderTimeViewModel>>(lst);

                    var datePriceList = this._scheduleOrderService.GetDatePriceList(model.FieldId, date);



                    return Ok(new { datePriceList, possibleOrder });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [Route("get-price")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetPrice([FromUri] GetPriceBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                try
                {
                    var date = DateTime.ParseExact(model.DateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    var price = this._scheduleOrderService.GetPrice(model.FieldId, date, model.BeginHour, model.Duration);
                    return Ok(price);
                }
                catch (ServiceException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
    }
}