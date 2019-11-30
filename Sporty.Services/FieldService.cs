using Sporty.DAL.Infrastructure;
using Sporty.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Sporty.Commons;
using Sporty.Services.Exceptions;
using Sporty.Services.Models;

namespace Sporty.Services
{
    public interface IFieldService
    {
        void Create(Field field);

        void Delete(Field field);

        Field GetDetail(string Id);

        List<Field> GetList(string placeId);

        List<FieldType> GetAllType(string ownerId);

        FieldType GetFieldType(string fieldId);

        WeekPrice GetWeekPriceByType(string fieldTypeId);

        WeekPrice GetWeekPrice(string fieldId);
    }
    public class FieldService : IFieldService
    {
        private IRepository<Field, string> _fieldRepository;
        private IRepository<FieldType, string> _fieldTypeRepository;
        private IRepository<Place, string> _placeRepository;
        private IRepository<WeekPrice, string> _weekPriceRepository;
        private IRepository<ScheduleOrder, string> _scheduleOrderRepository;

        private IUnitOfWork _unitOfWork;

        public FieldService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._fieldRepository = unitOfWork.Repository<Field, string>();
            this._fieldTypeRepository = unitOfWork.Repository<FieldType, string>();
            this._placeRepository = unitOfWork.Repository<Place, string>();
            this._weekPriceRepository = unitOfWork.Repository<WeekPrice, string>();
            this._scheduleOrderRepository = unitOfWork.Repository<ScheduleOrder, string>();
        }

        public void Create(Field field)
        {
            this._fieldRepository.Insert(field);
        }

        public void Delete(Field field)
        {
            this._fieldRepository.Delete(field);
        }

        public Field GetDetail(string Id)
        {
            return this._fieldRepository.GetById(Id);
        }

        public List<Field> GetList(string placeId)
        {
            int total = 0;

            Expression<Func<Field, bool>> where = f => f.PlaceId == placeId;
            Expression<Func<Field, DateTime>> orderbycreateddate = f => f.CreatedDate;

            var lst = this._fieldRepository.GetList(out total, where, orderbycreateddate);

            return lst;
        }

        public List<FieldType> GetAllType(string ownerId)
        {
            return this._fieldRepository.GetAll().Join(
                _placeRepository.GetAll().Where(p => p.Id == ownerId),
                f => f.PlaceId,
                p => p.Id,
                (f, p) => new { f.FieldTypeId }).Join(
                _fieldTypeRepository.GetAll(),
                f => f.FieldTypeId,
                ft => ft.Id,
                (f, ft) => ft).ToList();
        }

        public FieldType GetFieldType(string fieldId)
        {
            return this._fieldTypeRepository
                .GetById(_fieldRepository.GetById(fieldId).FieldTypeId);
        }

        public WeekPrice GetWeekPriceByType(string fieldTypeId)
        {
            return this._weekPriceRepository.GetAll()
                .Where(wp => wp.FieldTypeId == fieldTypeId).DefaultIfEmpty(null).FirstOrDefault();
        }

        public WeekPrice GetWeekPrice(string fieldId)
        {
            return GetWeekPriceByType(GetFieldType(fieldId).Id);
        }
    }
}
