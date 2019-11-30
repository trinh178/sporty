namespace Sporty.DAL.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Sporty.DAL.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Sporty.DAL.SportyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Sporty.DAL.SportyDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            // Roles
            var ownerRole = new ApplicationRole
            {
                Id = "role_00",
                Name = "Owner"
            };
            var customerRole = new ApplicationRole
            {
                Id = "role_01",
                Name = "Customer"
            };
            context.Roles.Add(ownerRole);
            context.Roles.Add(customerRole);
            context.SaveChanges();


            // Users
            var userManager = new UserManager<ApplicationUser, string>(new UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context));
            // Owners
            var owner = new ApplicationUser()
            {
                Email = "phambatrinh@sporty.com",
                UserName = "trinh"
            };
            userManager.Create(owner, "123456");
            // Customers
            var customer = new ApplicationUser()
            {
                Email = "test@sporty.com",
                UserName = "test"
            };
            userManager.Create(customer, "123456");


            // UserRoles
            userManager.AddToRole(owner.Id, ownerRole.Name);
            userManager.AddToRole(customer.Id, customerRole.Name);




            // Guests
            var guest = new Guest()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Guest_0",
                ContactPhoneNumber = "0123456789",
                Note = "Note"
            };
            context.Guests.Add(guest);


            // Places
            var place0 = new Place()
            {
                Id = Guid.NewGuid().ToString(),

                Name = "Place_0",
                Description = "Description",
                ContactPhoneNumber1 = "0123456789",
                ContactPhoneNumber2 = "0123456789",
                ImagesArray = "['image_0.png', 'image_1.png']",
                OpenHour = 6.0f,
                CloseHour = 22.0f,
                MinDuration = 1.0f,
                MaxDuration = 3.0f,
                RatingAvgNumber = 5f,
                AddressInfo = "abcxyz",
                AddressDistrict = "Quận 1",
                AddressProvinceCity = "TP Hồ Chí Minh",
                CreatedDate = DateTime.Now,

                OwnerId = owner.Id
            };
            context.Places.Add(place0);

            var place1 = new Place()
            {
                Id = Guid.NewGuid().ToString(),

                Name = "Place_1",
                Description = "Description",
                ContactPhoneNumber1 = "0123456789",
                ContactPhoneNumber2 = "0123456789",
                ImagesArray = "['image_1.png', 'image_0.png']",
                OpenHour = 6.0f,
                CloseHour = 22.0f,
                MinDuration = 1.0f,
                MaxDuration = 3.0f,
                RatingAvgNumber = 5f,
                AddressInfo = "abcxyz",
                AddressDistrict = "Quận 1",
                AddressProvinceCity = "TP Hồ Chí Minh",
                CreatedDate = DateTime.Now,

                OwnerId = owner.Id
            };
            context.Places.Add(place1);

            var place2 = new Place()
            {
                Id = Guid.NewGuid().ToString(),

                Name = "Place_2",
                Description = "Description",
                ContactPhoneNumber1 = "0123456789",
                ContactPhoneNumber2 = "0123456789",
                ImagesArray = "['image_0.png', 'image_1.png']",
                OpenHour = 6.0f,
                CloseHour = 22.0f,
                MinDuration = 1.0f,
                MaxDuration = 3.0f,
                RatingAvgNumber = 5f,
                AddressInfo = "abcxyz",
                AddressDistrict = "Quận 1",
                AddressProvinceCity = "TP Hồ Chí Minh",
                CreatedDate = DateTime.Now,

                OwnerId = owner.Id
            };
            context.Places.Add(place2);


            // FieldTypes
            var fieldType0 = new FieldType()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "FieldType_0",
            };
            context.FieldTypes.Add(fieldType0);

            var fieldType1 = new FieldType()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "FieldType_1",
            };
            context.FieldTypes.Add(fieldType1);

            var fieldType2 = new FieldType()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "FieldType_2",
            };
            context.FieldTypes.Add(fieldType2);


            // WeekPrices
            var weekPrices = new WeekPrice()
            {
                Id = Guid.NewGuid().ToString(),

                Monday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",
                Tuesday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",
                Wednesday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",
                Thursday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",
                Friday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",
                Saturday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",
                Sunday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",

                FieldTypeId = fieldType0.Id
            };
            context.WeekPrices.Add(weekPrices);
            weekPrices = new WeekPrice()
            {
                Id = Guid.NewGuid().ToString(),

                Monday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",
                Tuesday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",
                Wednesday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",
                Thursday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",
                Friday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",
                Saturday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",
                Sunday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",

                FieldTypeId = fieldType1.Id
            };
            context.WeekPrices.Add(weekPrices);
            weekPrices = new WeekPrice()
            {
                Id = Guid.NewGuid().ToString(),

                Monday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",
                Tuesday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",
                Wednesday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",
                Thursday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",
                Friday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",
                Saturday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",
                Sunday = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",

                FieldTypeId = fieldType2.Id
            };
            context.WeekPrices.Add(weekPrices);


            // SpecialDays
            var specialDayPrice = new SpecialDayPrice()
            {
                Id = Guid.NewGuid().ToString(),

                Date = new DateTime(2019, 11, 20),
                Data = "{spantimes:[5.0,10.0,15.0,22.0], spanprices:[300,250,350]}",

                FieldTypeId = fieldType0.Id
            };
            context.SpecialDayPrices.Add(specialDayPrice);


            // Fields
            var field0 = new Field()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Field_0",

                CreatedDate = DateTime.Now,

                FieldTypeId = fieldType0.Id,
                PlaceId = place0.Id,
            };
            context.Fields.Add(field0);

            var field1 = new Field()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Field_1",

                CreatedDate = DateTime.Now,

                FieldTypeId = fieldType0.Id,
                PlaceId = place0.Id,
            };
            context.Fields.Add(field1);

            var field2 = new Field()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Field_2",

                CreatedDate = DateTime.Now,

                FieldTypeId = fieldType0.Id,
                PlaceId = place0.Id,
            };
            context.Fields.Add(field2);

            var field3 = new Field()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Field_3",

                CreatedDate = DateTime.Now,

                FieldTypeId = fieldType0.Id,
                PlaceId = place2.Id,
            };
            context.Fields.Add(field3);

            var field4 = new Field()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Field_4",

                CreatedDate = DateTime.Now,

                FieldTypeId = fieldType0.Id,
                PlaceId = place2.Id,
            };
            context.Fields.Add(field3);


            // Schedules
            var scheduleOrder0 = new ScheduleOrder()
            {
                Id = Guid.NewGuid().ToString(),

                CreatedDate = DateTime.Now,
                Status = DAL.Constants.ScheduleOrderStatus.Unverified,
                StartDate = new DateTime(2019, 11, 15, 6, 0, 0),
                Duration = 2,

                FieldId = field0.Id,
                CustomerId = customer.Id,
                GuestId = null
            };
            context.SchedulesOrder.Add(scheduleOrder0);

            var scheduleOrder1 = new ScheduleOrder()
            {
                Id = Guid.NewGuid().ToString(),

                CreatedDate = DateTime.Now,
                Status = DAL.Constants.ScheduleOrderStatus.Verified_Unpaid,
                StartDate = new DateTime(2019, 11, 14, 6, 0, 0),
                Duration = 2,

                FieldId = field0.Id,
                CustomerId = customer.Id,
                GuestId = null
            };
            context.SchedulesOrder.Add(scheduleOrder1);

            var scheduleOrder2 = new ScheduleOrder()
            {
                Id = Guid.NewGuid().ToString(),

                CreatedDate = DateTime.Now,
                Status = DAL.Constants.ScheduleOrderStatus.Paid,
                StartDate = new DateTime(2019, 11, 11, 6, 0, 0),
                Duration = 2,

                FieldId = field0.Id,
                CustomerId = customer.Id,
                GuestId = null
            };
            context.SchedulesOrder.Add(scheduleOrder2);


            // Ratings
            var rating = new Rating()
            {
                RatingNumber = 5,
                PlaceId = place0.Id,
                CustomerId = customer.Id
            };
            context.Ratings.Add(rating);
        }
    }
}
