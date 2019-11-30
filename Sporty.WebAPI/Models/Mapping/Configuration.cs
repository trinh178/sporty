using AutoMapper;
using Sporty.DAL.Models;
using System.Linq;
using Newtonsoft.Json;

namespace Sporty.Web.Models.Mapping
{
    public class Configuration
    {
        public static void Configure()
        {
            // Automapper
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Place, PreviewPlaceViewModel>()
                .BeforeMap((p, pvm) => {
                    var arr = JsonConvert.DeserializeObject<string[]>(p.ImagesArray);
                    pvm.Images = arr;
                });
                cfg.CreateMap<Place, PlaceViewModel>()
                .BeforeMap((p, pvm) => {
                    var arr = JsonConvert.DeserializeObject<string[]>(p.ImagesArray);
                    pvm.Images = arr;
                });
                //cfg.CreateMap<Place, PlaceViewModel>();
                //cfg.CreateMap<Field, ListPlaceViewModel>();
            });
        }
    }
}