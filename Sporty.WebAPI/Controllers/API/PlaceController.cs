using AutoMapper;
using Sporty.DAL.Infrastructure.Exceptions;
using Sporty.DAL.Models;
using Sporty.Services;
using Sporty.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sporty.WebAPI.API.Controllers
{
    [RoutePrefix("api/place")]
    public class PlaceController : ApiController
    {
        private IPlaceService _placeService;

        public PlaceController(IPlaceService placeService)
        {
            this._placeService = placeService;
        }

        [Route("list")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult List([FromUri] ListPlaceBindingModel model)
        {
            if (ModelState.IsValid)
            {
                model = model ?? new ListPlaceBindingModel();
                int total;
                var lst = this._placeService.Search(out total,
                    model.Keyword, model.District, model.ProvinceCity, model.FieldType,
                    model.PageNumb, model.PageSize, model.SortBy);

                var response = Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<PreviewPlaceViewModel>>(lst));
                response.Headers.Add("Access-Control-Allow-Origin", "*");
                return ResponseMessage(response);
            }
            return BadRequest(ModelState);
        }

        [Route("detail")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Detail([FromUri] DetailPlaceBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                try
                {
                    var p = this._placeService.GetDetail(model.Id);
                    return Ok(Mapper.Map<PlaceViewModel>(p));
                }
                catch (RepositoryException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [Route("get-all-by-owner")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAllByOwner(string owner_id)
        {
            try
            {
                var p = this._placeService.GetAllByOwner(owner_id);
                return Ok(Mapper.Map<List<PreviewPlaceViewModel>>(p));
            }
            catch (RepositoryException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("get-all-fields-by-place")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAllFieldsByPlace(string place_id)
        {
            try
            {
                var p = this._placeService.GetAllFieldsByPlace(place_id);
                return Ok(Mapper.Map<List<FieldViewModel>>(p));
            }
            catch (RepositoryException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("price-list")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult PriceList([FromUri] DetailPlaceBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                try
                {
                    var viewModel = new PlacePriceListViewModel();

                    var wpl = this._placeService.GetWeekPriceList(model.Id);

                    foreach (var wp in wpl)
                    {
                        viewModel.Monday.Add(new FieldTypePriceViewModel
                        {
                            FieldTypeName = wp.FieldType.Name,
                            DayPrice = wp.Monday
                        });
                        viewModel.Tuesday.Add(new FieldTypePriceViewModel
                        {
                            FieldTypeName = wp.FieldType.Name,
                            DayPrice = wp.Tuesday
                        });
                        viewModel.Wednesday.Add(new FieldTypePriceViewModel
                        {
                            FieldTypeName = wp.FieldType.Name,
                            DayPrice = wp.Wednesday
                        });
                        viewModel.Thursday.Add(new FieldTypePriceViewModel
                        {
                            FieldTypeName = wp.FieldType.Name,
                            DayPrice = wp.Thursday
                        });
                        viewModel.Friday.Add(new FieldTypePriceViewModel
                        {
                            FieldTypeName = wp.FieldType.Name,
                            DayPrice = wp.Friday
                        });
                        viewModel.Saturday.Add(new FieldTypePriceViewModel
                        {
                            FieldTypeName = wp.FieldType.Name,
                            DayPrice = wp.Saturday
                        });
                        viewModel.Sunday.Add(new FieldTypePriceViewModel
                        {
                            FieldTypeName = wp.FieldType.Name,
                            DayPrice = wp.Sunday
                        });
                    }

                    return Ok(viewModel);
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