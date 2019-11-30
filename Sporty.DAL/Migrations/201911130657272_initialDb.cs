namespace Sporty.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fields",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 50),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        FieldTypeId = c.String(maxLength: 50),
                        PlaceId = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FieldTypes", t => t.FieldTypeId)
                .ForeignKey("dbo.Places", t => t.PlaceId)
                .Index(t => t.FieldTypeId)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.FieldTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 50),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SpecialDayPrices",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 50),
                        Date = c.DateTime(nullable: false),
                        Data = c.String(),
                        FieldTypeId = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FieldTypes", t => t.FieldTypeId)
                .Index(t => t.FieldTypeId);
            
            CreateTable(
                "dbo.WeekPrices",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 50),
                        Monday = c.String(),
                        Tuesday = c.String(),
                        Wednesday = c.String(),
                        Thursday = c.String(),
                        Friday = c.String(),
                        Saturday = c.String(),
                        Sunday = c.String(),
                        FieldTypeId = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FieldTypes", t => t.FieldTypeId)
                .Index(t => t.FieldTypeId);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 50),
                        Name = c.String(),
                        Description = c.String(),
                        ContactPhoneNumber1 = c.String(),
                        ContactPhoneNumber2 = c.String(),
                        ImagesArray = c.String(),
                        OpenHour = c.Single(nullable: false),
                        CloseHour = c.Single(nullable: false),
                        MinDuration = c.Single(nullable: false),
                        MaxDuration = c.Single(nullable: false),
                        RatingAvgNumber = c.Single(nullable: false),
                        AddressInfo = c.String(),
                        AddressDistrict = c.String(),
                        AddressProvinceCity = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        OwnerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        PlaceId = c.String(nullable: false, maxLength: 50),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        RatingNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlaceId, t.CustomerId })
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.PlaceId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ScheduleOrders",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        Duration = c.Single(nullable: false),
                        FieldId = c.String(maxLength: 50),
                        CustomerId = c.String(maxLength: 128),
                        GuestId = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId)
                .ForeignKey("dbo.Fields", t => t.FieldId)
                .ForeignKey("dbo.Guests", t => t.GuestId)
                .Index(t => t.FieldId)
                .Index(t => t.CustomerId)
                .Index(t => t.GuestId);
            
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 50),
                        Name = c.String(),
                        ContactPhoneNumber = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ScheduleOrders", "GuestId", "dbo.Guests");
            DropForeignKey("dbo.ScheduleOrders", "FieldId", "dbo.Fields");
            DropForeignKey("dbo.ScheduleOrders", "CustomerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Ratings", "CustomerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Places", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Fields", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.WeekPrices", "FieldTypeId", "dbo.FieldTypes");
            DropForeignKey("dbo.SpecialDayPrices", "FieldTypeId", "dbo.FieldTypes");
            DropForeignKey("dbo.Fields", "FieldTypeId", "dbo.FieldTypes");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ScheduleOrders", new[] { "GuestId" });
            DropIndex("dbo.ScheduleOrders", new[] { "CustomerId" });
            DropIndex("dbo.ScheduleOrders", new[] { "FieldId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Ratings", new[] { "CustomerId" });
            DropIndex("dbo.Ratings", new[] { "PlaceId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Places", new[] { "OwnerId" });
            DropIndex("dbo.WeekPrices", new[] { "FieldTypeId" });
            DropIndex("dbo.SpecialDayPrices", new[] { "FieldTypeId" });
            DropIndex("dbo.Fields", new[] { "PlaceId" });
            DropIndex("dbo.Fields", new[] { "FieldTypeId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Guests");
            DropTable("dbo.ScheduleOrders");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Ratings");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Places");
            DropTable("dbo.WeekPrices");
            DropTable("dbo.SpecialDayPrices");
            DropTable("dbo.FieldTypes");
            DropTable("dbo.Fields");
        }
    }
}
