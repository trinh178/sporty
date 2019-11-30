using Sporty.DAL.Infrastructure;
using Sporty.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Sporty.Services
{
    public interface IPlaceService
    {
        void Create(Place place);

        void Delete(Place place);

        Place GetDetail(string Id);

        List<Place> Search(
            out int totalPage,
            string keyword = null,
            string district = null,
            string provinceCity = null,
            string fieldType = null,
            int pageNumb = 0,
            int pageSize = 20,
            SortBy sortBy = SortBy.Recently);

        List<Place> GetAllByOwner(string ownerId);

        List<Field> GetAllFieldsByPlace(string placeId);

        List<WeekPrice> GetWeekPriceList(string placeId);
    }

    public class PlaceService : IPlaceService
    {
        private IRepository<Place, string> _placeRepository;
        private IRepository<Field, string> _fieldRepository;
        private IRepository<FieldType, string> _fieldTypeRepository;
        private IRepository<WeekPrice, string> _weekPriceRepository;

        //private IRepository<Field, string> _fieldRepository;
        private IUnitOfWork _unitOfWork;

        public PlaceService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._placeRepository = unitOfWork.Repository<Place, string>();
            this._fieldRepository = unitOfWork.Repository<Field, string>();
            this._fieldTypeRepository = unitOfWork.Repository<FieldType, string>();
            this._weekPriceRepository = unitOfWork.Repository<WeekPrice, string>();
        }

        public void Create(Place place)
        {
            this._placeRepository.Insert(place);
        }

        public void Delete(Place place)
        {
            this._placeRepository.Delete(place);
        }

        public Place GetDetail(string Id)
        {
            return this._placeRepository.GetById(Id);
        }

        public List<Place> Search(
            out int totalPage,
            string keyword = null,
            string district = null,
            string provinceCity = null,
            string fieldType = null,
            int pageNumb = 0,
            int pageSize = 20,
            SortBy sortBy = SortBy.Recently)
        {
            //
            Expression<Func<Place, bool>> where;
            if (string.IsNullOrEmpty(provinceCity))
            {
                where = p =>
                     string.IsNullOrEmpty(keyword) ? true :
                     p.Name.Contains(keyword) ||
                     p.Description.Contains(keyword) ||
                     p.AddressInfo.Contains(keyword) ||
                     p.AddressDistrict.Contains(keyword) ||
                     p.AddressProvinceCity.Contains(keyword);
            }
            else
            {
                if (string.IsNullOrEmpty(district))
                {
                    where = p => (p.AddressProvinceCity == provinceCity) &&
                     (string.IsNullOrEmpty(keyword) ? true :
                     p.Name.Contains(keyword) ||
                     p.Description.Contains(keyword) ||
                     p.AddressInfo.Contains(keyword) ||
                     p.AddressDistrict.Contains(keyword) ||
                     p.AddressProvinceCity.Contains(keyword));
                }
                else
                {
                    where = p => (p.AddressProvinceCity == provinceCity) &&
                                 (p.AddressDistrict == district) &&
                     (string.IsNullOrEmpty(keyword) ? true :
                     p.Name.Contains(keyword) ||
                     p.Description.Contains(keyword) ||
                     p.AddressInfo.Contains(keyword) ||
                     p.AddressDistrict.Contains(keyword) ||
                     p.AddressProvinceCity.Contains(keyword));
                }
            }
            Expression<Func<Place, DateTime>> orderbycreateddate = p => p.CreatedDate;
            Expression<Func<Place, float>> orderbyrating = p => p.RatingAvgNumber;

            var lst = (sortBy == SortBy.Recently) ?
                this._placeRepository.GetList(
                    out totalPage, where, orderbycreateddate, pageSize, pageNumb) :
                this._placeRepository.GetList(
                    out totalPage, where, orderbyrating, pageSize, pageNumb);

            return lst;
        }

        public List<Place> GetAllByOwner(string ownerId)
        {
            int total = 0;

            Expression<Func<Place, bool>> where = p => p.OwnerId == ownerId;
            Expression<Func<Place, DateTime>> orderbycreateddate = p => p.CreatedDate;

            var lst = this._placeRepository.GetList(out total, where, orderbycreateddate);

            return lst;
        }

        public List<Field> GetAllFieldsByPlace(string placeId)
        {
            int total = 0;

            Expression<Func<Field, bool>> where = f => f.PlaceId == placeId;
            Expression<Func<Field, DateTime>> orderbycreateddate = f => f.CreatedDate;

            var lst = this._fieldRepository.GetList(out total, where, orderbycreateddate);

            return lst;
        }

        public List<WeekPrice> GetWeekPriceList(string placeId)
        {
            var fields = _fieldRepository.GetAll().Select(f => f.FieldTypeId).ToList();

            var types = _fieldTypeRepository.GetAll()
                .Where(ft =>
                    fields.Contains(ft.Id)).Select(ft => ft.Id);

            return _weekPriceRepository.GetAll().Where(wp => types.Contains(wp.FieldTypeId)).ToList();
        }
    }
}