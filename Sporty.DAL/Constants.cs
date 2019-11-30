using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sporty.DAL.Constants
{
    public enum ScheduleOrderStatus
    {
        Unverified,
        Verified_Unpaid,
        Paid,
        Canceled
    }

    public static class Data
    {
        public static string[] ProvinceCitys
        {
            get;
            private set;
        }
        public static string[] FieldTypes
        {
            get;
            private set;
        }

        static Data()
        {
            ProvinceCitys = new string[]
            {
                "TP Hà Nội",
                "Hà Giang",
                "Cao Bằng",
                "Bắc Kạn",
                "Tuyên Quang",
                "Lào Cai",
                "Điện Biên",
                "Lai Châu",
                "Sơn La",
                "Yên Bái",
                "Hoà Bình",
                "Thái Nguyên",
                "Lạng Sơn",
                "Quảng Ninh",
                "Bắc Giang",
                "Phú Thọ",
                "Vĩnh Phúc",
                "Bắc Ninh",
                "Hải Dương",
                "TP Hải Phòng",
                "Hưng Yên",
                "Thái Bình",
                "Hà Nam",
                "Nam Định",
                "Ninh Bình",
                "Thanh Hoá",
                "Nghệ An",
                "Hà Tĩnh",
                "Quảng Bình",
                "Quảng Trị",
                "Thừa Thiên Huế",
                "TP Đà Nẵng",
                "Quảng Nam",
                "Quảng Ngãi",
                "Bình Định",
                "Phú Yên",
                "Khánh Hoà",
                "Ninh Thuận",
                "Bình Thuận",
                "Kon Tum",
                "Gia Lai",
                "Đắk Lắk",
                "Đắk Nông",
                "Lâm Đồng",
                "Bình Phước",
                "Tây Ninh",
                "Bình Dương",
                "Đồng Nai",
                "Bà Rịa - Vũng Tàu",
                "TP Hồ Chí Minh",
                "Long An",
                "Tiền Giang",
                "Bến Tre",
                "Trà Vinh",
                "Vĩnh Long",
                "Đồng Tháp",
                "An Giang",
                "Kiên Giang",
                "TP Cần Thơ",
                "Hậu Giang",
                "Sóc Trăng",
                "Bạc Liêu",
                "Cà Mau",
            };

            FieldTypes = new string[]
            {
                "Sân 5",
                "Sân 7",
            };
        }
    }
}
