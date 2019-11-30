using AutoMapper;
using Sporty.DAL.Infrastructure.Exceptions;
using Sporty.Services;
using Sporty.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sporty.Web.API.Controllers
{
    [RoutePrefix("api/field")]
    public class FieldController : ApiController
    {
        private IFieldService _fieldService;

        public FieldController(IFieldService fieldService)
        {
            this._fieldService = fieldService;
        }

        [Route("list")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult List([FromUri] ListFieldBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                try
                {
                    var p = this._fieldService.GetList(model.PlaceId);
                    return Ok(Mapper.Map<List<FieldViewModel>>(p));
                }
                catch (RepositoryException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [Route("detail")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Detail([FromUri] DetailFieldBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                try
                {
                    var p = this._fieldService.GetDetail(model.Id);
                    return Ok(Mapper.Map<FieldViewModel>(p));
                }
                catch (RepositoryException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [Route("price-list")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult PriceList([FromUri] DetailFieldBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                try
                {
                    var wp = this._fieldService.GetWeekPrice(model.Id);
                    return Ok(Mapper.Map<WeekPriceViewModel>(wp));
                }
                catch (RepositoryException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
    }
}