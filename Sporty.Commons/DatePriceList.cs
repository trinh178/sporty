using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sporty.Commons
{
    public class DatePriceList
    {
        public DatePriceList()
        {
            SpanTimes = new float[] { 0f, 24f };
            SpanPrices = new float[] { 0f };

            this.SpanCount = SpanPrices.Length;
        }
        public DatePriceList(string json)
        {
            var temp = DecodeFromJson(json);
            if (temp == null)
            {
                throw new DatePriceListException("Failure to parse Json to DayPriceList object.");
            }
            this.SpanTimes = temp.SpanTimes;
            this.SpanPrices = temp.SpanPrices;

            this.SpanCount = temp.SpanPrices.Length;
        }

        public float[] SpanTimes { get; set; }
        public float[] SpanPrices { get; set; }
        
        [JsonIgnore]
        public int SpanCount { get; set; }

        // Methods
        public float Price(float beginHour, float duration)
        {
            var endHour = beginHour + duration;

            if (endHour > 24f) throw new DatePriceListException("DatePriceList.Price params invalid");

            var openTime = SpanTimes[0];
            var closeTime = SpanTimes[SpanTimes.Length - 1];
            if (!(openTime <= beginHour && beginHour < endHour && endHour <= closeTime))
            {
                throw new Exception("beginHour and endHour are invalid!");
            }
            float price = 0;
            for (int i = 0; i < SpanTimes.Length - 1; i++)
            {
                if (beginHour < SpanTimes[i])
                {
                    if (SpanTimes[i] >= endHour)
                    {
                        price = (endHour - beginHour) * SpanPrices[i - 1];
                        break;
                    }
                    else
                    {
                        // Head
                        price = (SpanTimes[i] - beginHour) * SpanPrices[i - 1];

                        // Body
                        i++;
                        for (; i < SpanTimes.Length; i++)
                        {
                            if (endHour > SpanTimes[i])
                            {
                                price += (SpanTimes[i] - SpanTimes[i - 1]) * SpanPrices[i - 1];
                            }
                            else break;
                        }

                        // Footer
                        price += (endHour - SpanTimes[i - 1]) * SpanPrices[i - 1];
                        break;
                    }
                }
            }
            return price;
        }
        public float SpanPrice(int index, out float beginHour, out float endHour)
        {
            beginHour = SpanTimes[index];
            endHour = SpanTimes[index + 1];
            return SpanPrices[index];
        }

        // Statics
        public static string EncodeToJson(DatePriceList dayPrice)
        {
            return JsonConvert.SerializeObject(dayPrice);
        }
        public static DatePriceList DecodeFromJson(string json)
        {
            var tempDayPrice = JsonConvert.DeserializeObject<DatePriceList>(json);

            // Validate
            // Open-close Time
            if (tempDayPrice.SpanTimes.Length < 2)
            {
                return null;
            }
            var openTime = tempDayPrice.SpanTimes[0];
            var closeTime = tempDayPrice.SpanTimes[tempDayPrice.SpanTimes.Length - 1];
            if (!(0f <= openTime && openTime < closeTime && closeTime <= 24f))
            {
                return null;
            }
            // Order
            for (int i = 0; i < tempDayPrice.SpanTimes.Length - 1; i++)
            {
                if (tempDayPrice.SpanTimes[i] > tempDayPrice.SpanTimes[i + 1])
                {
                    return null;
                }
            }
            // Price
            if (tempDayPrice.SpanPrices.Length != tempDayPrice.SpanTimes.Length - 1)
            {
                return null;
            }

            tempDayPrice.SpanCount = tempDayPrice.SpanPrices.Length;
            return tempDayPrice;
        }
        public static float ToValidHour(TimeSpan timeSpan)
        {
            if (timeSpan.Minutes != 0 && timeSpan.Minutes != 30)
            {
                throw new DatePriceListException("timeSpan.Minutes hasn't valid !");
            }
            return timeSpan.Hours + ((timeSpan.Minutes == 0) ? 0f : 0.5f);
        }
        public static string FormatHour(float hour)
        {
            int _hour = (int)Math.Floor(hour);
            int _minute = (int)((hour * 60) % 60);
            return _hour + ":" + _minute;
        }
        //public static string FormatPrice(float price)
        //{
        //    return price;
        //}

        public class DatePriceListException : Exception
        {
            public DatePriceListException(string message) : base(message)
            {
            }
        }
    }
}
