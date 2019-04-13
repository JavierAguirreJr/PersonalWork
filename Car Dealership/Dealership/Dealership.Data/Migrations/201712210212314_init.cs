namespace Dealership.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BodyStyles",
                c => new
                    {
                        BodyStyleId = c.Int(nullable: false, identity: true),
                        BodyType = c.String(),
                    })
                .PrimaryKey(t => t.BodyStyleId);
            
            CreateTable(
                "dbo.CarMakes",
                c => new
                    {
                        MakeID = c.Int(nullable: false, identity: true),
                        Make = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MakeID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
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
                "dbo.CarModels",
                c => new
                    {
                        ModelID = c.Int(nullable: false, identity: true),
                        Model = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        MakeName_MakeID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ModelID)
                .ForeignKey("dbo.CarMakes", t => t.MakeName_MakeID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.MakeName_MakeID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ContactUs",
                c => new
                    {
                        CustomerContactId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Messsage = c.String(),
                    })
                .PrimaryKey(t => t.CustomerContactId);
            
            CreateTable(
                "dbo.CustomerInformations",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.Int(nullable: false),
                        Email = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zipcode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.PurchaseTypes",
                c => new
                    {
                        PurchaseTypeId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.PurchaseTypeId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Phone = c.Int(nullable: false),
                        Email = c.String(),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Zipcode = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        PriceSold = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateSold = c.DateTime(nullable: false),
                        IsSold = c.Boolean(nullable: false),
                        IsFeatured = c.Boolean(nullable: false),
                        PurchaseType_PurchaseTypeId = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.PurchaseTypes", t => t.PurchaseType_PurchaseTypeId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.VehicleDetails", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId)
                .Index(t => t.PurchaseType_PurchaseTypeId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.VehicleDetails",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        VIN = c.String(nullable: false),
                        Year = c.Int(nullable: false),
                        Mileage = c.Int(nullable: false),
                        Color = c.String(nullable: false),
                        Interior = c.String(nullable: false),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MSRP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(nullable: false),
                        IsFeatured = c.Boolean(nullable: false),
                        IsNew = c.Boolean(nullable: false),
                        ImageUrl = c.String(),
                        IsSold = c.Boolean(nullable: false),
                        Image = c.Binary(),
                        BodyName_BodyStyleId = c.Int(),
                        ModelName_ModelID = c.Int(nullable: false),
                        TransmissionType_TransmissionId = c.Int(),
                        User_Id = c.String(maxLength: 128),
                        VehicleType_VehicleTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.VehicleId)
                .ForeignKey("dbo.BodyStyles", t => t.BodyName_BodyStyleId)
                .ForeignKey("dbo.CarModels", t => t.ModelName_ModelID, cascadeDelete: true)
                .ForeignKey("dbo.Transmissions", t => t.TransmissionType_TransmissionId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleType_VehicleTypeId)
                .Index(t => t.BodyName_BodyStyleId)
                .Index(t => t.ModelName_ModelID)
                .Index(t => t.TransmissionType_TransmissionId)
                .Index(t => t.User_Id)
                .Index(t => t.VehicleType_VehicleTypeId);
            
            CreateTable(
                "dbo.Transmissions",
                c => new
                    {
                        TransmissionId = c.Int(nullable: false, identity: true),
                        TransmissionName = c.String(),
                    })
                .PrimaryKey(t => t.TransmissionId);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        VehicleTypeId = c.Int(nullable: false, identity: true),
                        TypeDescription = c.String(),
                    })
                .PrimaryKey(t => t.VehicleTypeId);
            
            CreateTable(
                "dbo.Specials",
                c => new
                    {
                        SpecialsId = c.Int(nullable: false, identity: true),
                        SpecialName = c.String(),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.SpecialsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "VehicleId", "dbo.VehicleDetails");
            DropForeignKey("dbo.VehicleDetails", "VehicleType_VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.VehicleDetails", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.VehicleDetails", "TransmissionType_TransmissionId", "dbo.Transmissions");
            DropForeignKey("dbo.VehicleDetails", "ModelName_ModelID", "dbo.CarModels");
            DropForeignKey("dbo.VehicleDetails", "BodyName_BodyStyleId", "dbo.BodyStyles");
            DropForeignKey("dbo.Sales", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sales", "PurchaseType_PurchaseTypeId", "dbo.PurchaseTypes");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CarModels", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CarModels", "MakeName_MakeID", "dbo.CarMakes");
            DropForeignKey("dbo.CarMakes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.VehicleDetails", new[] { "VehicleType_VehicleTypeId" });
            DropIndex("dbo.VehicleDetails", new[] { "User_Id" });
            DropIndex("dbo.VehicleDetails", new[] { "TransmissionType_TransmissionId" });
            DropIndex("dbo.VehicleDetails", new[] { "ModelName_ModelID" });
            DropIndex("dbo.VehicleDetails", new[] { "BodyName_BodyStyleId" });
            DropIndex("dbo.Sales", new[] { "User_Id" });
            DropIndex("dbo.Sales", new[] { "PurchaseType_PurchaseTypeId" });
            DropIndex("dbo.Sales", new[] { "VehicleId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.CarModels", new[] { "User_Id" });
            DropIndex("dbo.CarModels", new[] { "MakeName_MakeID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.CarMakes", new[] { "User_Id" });
            DropTable("dbo.Specials");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Transmissions");
            DropTable("dbo.VehicleDetails");
            DropTable("dbo.Sales");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PurchaseTypes");
            DropTable("dbo.CustomerInformations");
            DropTable("dbo.ContactUs");
            DropTable("dbo.CarModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CarMakes");
            DropTable("dbo.BodyStyles");
        }
    }
}
