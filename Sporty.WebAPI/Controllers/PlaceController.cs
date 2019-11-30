using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sporty.WebAPI.Models;
using Sporty.Services;
using AutoMapper;
using Sporty.DAL.Infrastructure.Exceptions;

namespace Sporty.WebAPI.Controllers
{
    public class PlaceController : Controller
    {
        private IPlaceService _placeService;
        private IFieldService _fieldService;

        public PlaceController(IPlaceService placeService, IFieldService fieldService)
        {
            this._placeService = placeService;
            this._fieldService = fieldService;
        }

        [Route("list")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult List(ListPlaceBindingModel model)
        {
            if (ModelState.IsValid)
            {
                model = model ?? new ListPlaceBindingModel();
                int total;
                var lst = this._placeService.Search(out total,
                    model.Keyword, model.District, model.ProvinceCity, model.FieldType,
                    model.PageNumb, 10, model.SortBy);

                ViewBag.Places = Mapper.Map<List<PreviewPlaceViewModel>>(lst);

                ViewBag.PagesTotal = total;
                ViewBag.PageNumb = model.PageNumb;

                return View();
            }
            return View("Error");
        }

        [Route("detail")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Detail(DetailPlaceBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                try
                {
                    var p = this._placeService.GetDetail(model.Id);
                    var m = Mapper.Map<PlaceViewModel>(p);

                    // Price list
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
                    ViewBag.PriceList = viewModel;

                    // Field list
                    var fieldList = _fieldService.GetList(model.Id);
                    ViewBag.FieldList = Mapper.Map<List<FieldViewModel>>(fieldList);

                    return View(m);
                }
                catch (RepositoryException ex)
                {
                    return View("Error");
                }
            }
            return View("Error");
        }

        [Route("detail")]
        [HttpGet]
        [AllowAnonymous]
        public string PriceListData()
        {
            return null;
        }
    }
}