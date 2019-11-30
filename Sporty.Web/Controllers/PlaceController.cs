using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sporty.Web.Models;
using Sporty.Services;

namespace Sporty.Web.Controllers
{
    public class PlaceController : Controller
    {
        private IPlaceService _placeService;

        public PlaceController(IPlaceService placeService)
        {
            this._placeService = placeService;
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
                    model.PageNumb, model.PageSize, model.SortBy);

                //var response = Mapper.Map<List<PreviewPlaceViewModel>>(lst);
                return View();
            }
            return View("Error");
        }
    }
}