using Sporty.Commons;
using Sporty.DAL.Infrastructure;
using Sporty.DAL.Models;
using Sporty.Services.Exceptions;
using Sporty.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static Sporty.Commons.DatePriceList;

namespace Sporty.Services
{
    public interface IScheduleOrderService
    {
        void Create(ScheduleOrder field);

        void Delete(ScheduleOrder field);

        ScheduleOrder GetDetail(string Id);

        List<PossibleScheduleOrderTime> GetAllPossibleScheduleOrderTime(string fieldId, DateTime date);

        float GetPrice(string fieldId, DateTime date, float beginHour, float duration);

        DatePriceList GetDatePriceList(string fieldId, DateTime date);
    }

    public class ScheduleOrderService : IScheduleOrderService
    {
        private IRepository<Field, string> _fieldRepository;
        private IRepository<FieldType, string> _fieldTypeRepository;
        private IRepository<Place, string> _placeRepository;
        private IRepository<WeekPrice, string> _weekPriceRepository;
        private IRepository<ScheduleOrder, string> _scheduleOrderRepository;

        private IFieldService _fieldService;

        private IUnitOfWork _unitOfWork;

        public ScheduleOrderService(IUnitOfWork unitOfWork, IFieldService fieldService)
        {
            this._unitOfWork = unitOfWork;
            this._fieldRepository = unitOfWork.Repository<Field, string>();
            this._fieldTypeRepository = unitOfWork.Repository<FieldType, string>();
            this._placeRepository = unitOfWork.Repository<Place, string>();
            this._weekPriceRepository = unitOfWork.Repository<WeekPrice, string>();
            this._scheduleOrderRepository = unitOfWork.Repository<ScheduleOrder, string>();

            this._fieldService = fieldService;
        }

        public void Create(ScheduleOrder scheduleOrder)
        {
            this._scheduleOrderRepository.Insert(scheduleOrder);
        }

        public void Delete(ScheduleOrder scheduleOrder)
        {
            this._scheduleOrderRepository.Delete(scheduleOrder);
        }

        public ScheduleOrder GetDetail(string Id)
        {
            return this._scheduleOrderRepository.GetById(Id);
        }

        public List<PossibleScheduleOrderTime> GetAllPossibleScheduleOrderTime(string fieldId, DateTime date)
        {
            try
            {
                // Get list schedule order
                var solst = _scheduleOrderRepository.GetAll().Where(so => so.FieldId == fieldId)
                    .Select(so => new
                    {
                        so.StartDate,
                        so.Duration
                    })
                    .ToList();

                // Default
                var placeId = _fieldRepository.GetById(fieldId).PlaceId;
                var place = _placeRepository.GetById(placeId);
                var pbhlst = new float[48];
                for (int i = 0; i < pbhlst.Length; i++)
                {
                    if (i >= (int)(place.OpenHour * 2) && i < (int)(place.CloseHour * 2))
                    {
                        pbhlst[i] = i * 0.5f;
                    }
                    else
                    {
                        pbhlst[i] = -1;
                    }
                }
                // Exclusion by schedule orders
                foreach (var so in solst)
                {
                    if (so.StartDate.Date == date.Date)
                    {
                        var beginHour = DatePriceList.ToValidHour(so.StartDate.TimeOfDay);
                        var endHour = beginHour + so.Duration;
                        for (float i = beginHour; i < endHour; i += 0.5f)
                        {
                            pbhlst[(int)(i * 2)] = -1;
                        }
                    }
                }
                // Exclusion by duration rule
                for (int i = 0; i < pbhlst.Length; i++)
                {
                    if (pbhlst[i] != -1)
                    {
                        var min = i + (int)(place.MinDuration * 2);
                        min = (min > pbhlst.Length) ? pbhlst.Length : min;

                        for (int j = i; j < min; j++)
                        {
                            if (pbhlst[j] == -1)
                            {
                                pbhlst[i] = -1;
                                break;
                            }
                        }
                    }
                }

                // Duration
                var lst = new List<PossibleScheduleOrderTime>();
                for (int i = 0; i < pbhlst.Length; i++)
                {
                    if (pbhlst[i] != -1)
                    {
                        var max = place.MinDuration;

                        for (int j = i + (int)(place.MinDuration * 2) - 1; j < pbhlst.Length; j++)
                        {
                            if (pbhlst[j] != -1)
                            {
                                max += 0.5f;
                                if (max == place.MaxDuration)
                                {
                                    break;
                                }
                            }
                            else break;
                        }

                        lst.Add(new PossibleScheduleOrderTime()
                        {
                            BeginHour = pbhlst[i],
                            MinDuration = place.MinDuration,
                            MaxDuration = max
                        });
                    }
                }

                return lst;
            }
            catch (DatePriceList.DatePriceListException ex)
            {
                throw new ServiceException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public float GetPrice(string fieldId, DateTime date, float beginHour, float duration)
        {
            try
            {
                var weekPrice = _fieldService.GetWeekPrice(fieldId);

                DatePriceList datePriceList = null;

                switch (date.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        datePriceList = DatePriceList.DecodeFromJson(weekPrice.Monday);
                        break;
                    case DayOfWeek.Tuesday:
                        datePriceList = DatePriceList.DecodeFromJson(weekPrice.Tuesday);
                        break;
                    case DayOfWeek.Wednesday:
                        datePriceList = DatePriceList.DecodeFromJson(weekPrice.Wednesday);
                        break;
                    case DayOfWeek.Thursday:
                        datePriceList = DatePriceList.DecodeFromJson(weekPrice.Thursday);
                        break;
                    case DayOfWeek.Friday:
                        datePriceList = DatePriceList.DecodeFromJson(weekPrice.Friday);
                        break;
                    case DayOfWeek.Saturday:
                        datePriceList = DatePriceList.DecodeFromJson(weekPrice.Saturday);
                        break;
                    case DayOfWeek.Sunday:
                        datePriceList = DatePriceList.DecodeFromJson(weekPrice.Sunday);
                        break;
                }

                return datePriceList.Price(beginHour, duration);
            }
            catch (DatePriceListException ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public DatePriceList GetDatePriceList(string fieldId, DateTime date)
        {
            try
            {
                var weekPrice = _fieldService.GetWeekPrice(fieldId);

                DatePriceList datePriceList = null;

                switch (date.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        datePriceList = DatePriceList.DecodeFromJson(weekPrice.Monday);
                        break;
                    case DayOfWeek.Tuesday:
                        datePriceList = DatePriceList.DecodeFromJson(weekPrice.Tuesday);
                        break;
                    case DayOfWeek.Wednesday:
                        datePriceList = DatePriceList.DecodeFromJson(weekPrice.Wednesday);
                        break;
                    case DayOfWeek.Thursday:
                        datePriceList = DatePriceList.DecodeFromJson(weekPrice.Thursday);
                        break;
                    case DayOfWeek.Friday:
                        datePriceList = DatePriceList.DecodeFromJson(weekPrice.Friday);
                        break;
                    case DayOfWeek.Saturday:
                        datePriceList = DatePriceList.DecodeFromJson(weekPrice.Saturday);
                        break;
                    case DayOfWeek.Sunday:
                        datePriceList = DatePriceList.DecodeFromJson(weekPrice.Sunday);
                        break;
                }

                return datePriceList;
            }
            catch (DatePriceListException ex)
            {
                throw new ServiceException(ex.Message);
            }
        }
    }
}