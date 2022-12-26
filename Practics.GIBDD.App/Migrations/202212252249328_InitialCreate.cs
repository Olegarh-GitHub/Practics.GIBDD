namespace Practics.GIBDD.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthorizationAttempts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        PersonIdentity = c.String(),
                        AttemptDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarColors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        HexCode = c.String(),
                        Name = c.String(),
                        ColorName = c.String(),
                        IsMetallic = c.Boolean(nullable: false),
                        NameEN = c.String(),
                        ColorNameEN = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DriverId = c.Guid(nullable: false),
                        VINCode = c.String(),
                        Manufacturer = c.String(),
                        Model = c.String(),
                        Year = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        CarColorId = c.Int(nullable: false),
                        EngineTypeId = c.Int(nullable: false),
                        TypeOfDriveId = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drivers", t => t.DriverId, cascadeDelete: true)
                .ForeignKey("dbo.EngineTypes", t => t.EngineTypeId, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfDrives", t => t.TypeOfDriveId, cascadeDelete: true)
                .ForeignKey("dbo.CarColors", t => t.CarColorId, cascadeDelete: true)
                .Index(t => t.DriverId)
                .Index(t => t.CarColorId)
                .Index(t => t.EngineTypeId)
                .Index(t => t.TypeOfDriveId);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LastName = c.String(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        Passport = c.String(),
                        RegAddress = c.String(),
                        LiveAddress = c.String(),
                        PlaceOfWork = c.String(),
                        WorkPosition = c.String(),
                        PhoneMobile = c.String(),
                        Email = c.String(),
                        PhotoId = c.Int(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photos", t => t.PhotoId, cascadeDelete: true)
                .Index(t => t.PhotoId);
            
            CreateTable(
                "dbo.DriverLicenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DriverId = c.Guid(nullable: false),
                        LicenseDate = c.DateTime(nullable: false),
                        ExpireDate = c.DateTime(nullable: false),
                        Series = c.String(),
                        Number = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drivers", t => t.DriverId, cascadeDelete: true)
                .Index(t => t.DriverId);
            
            CreateTable(
                "dbo.DriverLicenseCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DriverLicenseId = c.Int(nullable: false),
                        DriverLicenseCategoryId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DriverLicenseCategories", t => t.DriverLicenseCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.DriverLicenses", t => t.DriverLicenseId, cascadeDelete: true)
                .Index(t => t.DriverLicenseId)
                .Index(t => t.DriverLicenseCategoryId);
            
            CreateTable(
                "dbo.DriverLicenseCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Data = c.Binary(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExternalId = c.Int(nullable: false),
                        CarNum = c.String(),
                        LicenseNum = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        PhotoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photos", t => t.PhotoId, cascadeDelete: true)
                .Index(t => t.PhotoId);
            
            CreateTable(
                "dbo.FineStatusHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        FineId = c.Int(nullable: false),
                        FineStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FineStatuses", t => t.FineStatusId, cascadeDelete: true)
                .ForeignKey("dbo.Fines", t => t.FineId, cascadeDelete: true)
                .Index(t => t.FineId)
                .Index(t => t.FineStatusId);
            
            CreateTable(
                "dbo.FineStatuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        Name = c.String(),
                        SendToFSSP = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EngineTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LocalizedName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RegNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleId = c.Int(nullable: false),
                        RegionCodeCodeId = c.Int(nullable: false),
                        Series = c.String(),
                        Number = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RegionCodeCodes", t => t.RegionCodeCodeId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId)
                .Index(t => t.RegionCodeCodeId);
            
            CreateTable(
                "dbo.RegionCodeCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegionCodeId = c.Int(nullable: false),
                        Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RegionCodes", t => t.RegionCodeId, cascadeDelete: true)
                .Index(t => t.RegionCodeId);
            
            CreateTable(
                "dbo.RegionCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegionNameEN = c.String(),
                        RegionNameRU = c.String(),
                        SubjectCode = c.Int(),
                        OKATOCode = c.String(),
                        ISO3166_2Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeOfDrives",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Login = c.String(),
                        Password = c.String(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "CarColorId", "dbo.CarColors");
            DropForeignKey("dbo.Vehicles", "TypeOfDriveId", "dbo.TypeOfDrives");
            DropForeignKey("dbo.RegNumbers", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.RegNumbers", "RegionCodeCodeId", "dbo.RegionCodeCodes");
            DropForeignKey("dbo.RegionCodeCodes", "RegionCodeId", "dbo.RegionCodes");
            DropForeignKey("dbo.Vehicles", "EngineTypeId", "dbo.EngineTypes");
            DropForeignKey("dbo.Vehicles", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Fines", "PhotoId", "dbo.Photos");
            DropForeignKey("dbo.FineStatusHistories", "FineId", "dbo.Fines");
            DropForeignKey("dbo.FineStatusHistories", "FineStatusId", "dbo.FineStatuses");
            DropForeignKey("dbo.Drivers", "PhotoId", "dbo.Photos");
            DropForeignKey("dbo.DriverLicenses", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.DriverLicenseCategory", "DriverLicenseId", "dbo.DriverLicenses");
            DropForeignKey("dbo.DriverLicenseCategory", "DriverLicenseCategoryId", "dbo.DriverLicenseCategories");
            DropIndex("dbo.RegionCodeCodes", new[] { "RegionCodeId" });
            DropIndex("dbo.RegNumbers", new[] { "RegionCodeCodeId" });
            DropIndex("dbo.RegNumbers", new[] { "VehicleId" });
            DropIndex("dbo.FineStatusHistories", new[] { "FineStatusId" });
            DropIndex("dbo.FineStatusHistories", new[] { "FineId" });
            DropIndex("dbo.Fines", new[] { "PhotoId" });
            DropIndex("dbo.DriverLicenseCategory", new[] { "DriverLicenseCategoryId" });
            DropIndex("dbo.DriverLicenseCategory", new[] { "DriverLicenseId" });
            DropIndex("dbo.DriverLicenses", new[] { "DriverId" });
            DropIndex("dbo.Drivers", new[] { "PhotoId" });
            DropIndex("dbo.Vehicles", new[] { "TypeOfDriveId" });
            DropIndex("dbo.Vehicles", new[] { "EngineTypeId" });
            DropIndex("dbo.Vehicles", new[] { "CarColorId" });
            DropIndex("dbo.Vehicles", new[] { "DriverId" });
            DropTable("dbo.Users");
            DropTable("dbo.TypeOfDrives");
            DropTable("dbo.RegionCodes");
            DropTable("dbo.RegionCodeCodes");
            DropTable("dbo.RegNumbers");
            DropTable("dbo.EngineTypes");
            DropTable("dbo.FineStatuses");
            DropTable("dbo.FineStatusHistories");
            DropTable("dbo.Fines");
            DropTable("dbo.Photos");
            DropTable("dbo.DriverLicenseCategories");
            DropTable("dbo.DriverLicenseCategory");
            DropTable("dbo.DriverLicenses");
            DropTable("dbo.Drivers");
            DropTable("dbo.Vehicles");
            DropTable("dbo.CarColors");
            DropTable("dbo.AuthorizationAttempts");
        }
    }
}
