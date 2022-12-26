using System.Data.Entity;
using Practics.GIBDD.App.Models;

namespace Practics.GIBDD.App
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("GIBDD_Vehicles")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DriverLicenseCategory>().ToTable("DriverLicenseCategory");

            modelBuilder.Entity<Drivers>()
                .HasKey(entity => entity.Id)
                .HasMany(driver => driver.Vehicles)
                .WithRequired(vehicle => vehicle.Drivers)
                .HasForeignKey(vehicle => vehicle.DriverId);

            modelBuilder.Entity<Photos>()
                .HasMany(entity => entity.Drivers)
                .WithRequired(entity => entity.Photos)
                .HasForeignKey(driver => driver.PhotoId);

            modelBuilder.Entity<Vehicles>()
                .HasMany(vehicle => vehicle.RegNumbers)
                .WithRequired(number => number.Vehicles)
                .HasForeignKey(number => number.VehicleId);

            modelBuilder.Entity<CarColors>()
                .HasMany(color => color.Vehicles)
                .WithRequired(vehicle => vehicle.CarColors)
                .HasForeignKey(vehicle => vehicle.CarColorId);

            modelBuilder.Entity<DriverLicenseCategories>()
                .HasMany(cat => cat.DriverLicenseCategory)
                .WithRequired(category => category.DriverLicenseCategories)
                .HasForeignKey(category => category.DriverLicenseCategoryId);

            modelBuilder.Entity<DriverLicenses>()
                .HasMany(license => license.DriverLicenseCategory)
                .WithRequired(cat => cat.DriverLicenses)
                .HasForeignKey(cat => cat.DriverLicenseId);

            modelBuilder.Entity<Drivers>()
                .HasMany(driver => driver.DriverLicenses)
                .WithRequired(licenses => licenses.Drivers)
                .HasForeignKey(licenses => licenses.DriverId);
            
            modelBuilder.Entity<EngineTypes>()
                .HasMany(type => type.Vehicles)
                .WithRequired(vehicle => vehicle.EngineTypes)
                .HasForeignKey(vehicle => vehicle.EngineTypeId);

            modelBuilder.Entity<Fines>()
                .HasMany(fine => fine.FineStatusHistory)
                .WithRequired(history => history.Fines)
                .HasForeignKey(history => history.FineId);

            modelBuilder.Entity<FineStatuses>()
                .HasMany(status => status.FineStatusHistory)
                .WithRequired(history => history.FineStatuses)
                .HasForeignKey(history => history.FineStatusId);

            modelBuilder.Entity<Photos>()
                .HasMany(photo => photo.Fines)
                .WithRequired(fine => fine.Photos)
                .HasForeignKey(fines => fines.PhotoId);

            modelBuilder.Entity<RegionCodeCodes>()
                .HasMany(code => code.RegNumbers)
                .WithRequired(number => number.RegionCodeCodes)
                .HasForeignKey(code => code.RegionCodeCodeId);

            modelBuilder.Entity<RegionCodes>()
                .HasMany(code => code.RegionCodeCodes)
                .WithRequired(codes => codes.RegionCodes)
                .HasForeignKey(codes => codes.RegionCodeId);

            modelBuilder.Entity<TypeOfDrive>()
                .HasMany(type => type.Vehicles)
                .WithRequired(vehicles => vehicles.TypeOfDrive)
                .HasForeignKey(vehicles => vehicles.TypeOfDriveId);
            
            
        }

        public virtual DbSet<AuthorizationAttempts> AuthorizationAttempts { get; set; }
        public virtual DbSet<Drivers> Drivers { get; set; }
        public virtual DbSet<Photos> Photos { get; set; }
        public virtual DbSet<RegionCodeCodes> RegionCodeCodes { get; set; }
        public virtual DbSet<RegionCodes> RegionCodes { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Fines> Fines { get; set; }
        public virtual DbSet<FineStatuses> FineStatuses { get; set; }
        public virtual DbSet<FineStatusHistory> FineStatusHistory { get; set; }
        public virtual DbSet<CarColors> CarColors { get; set; }
        public virtual DbSet<RegNumbers> RegNumbers { get; set; }
        public virtual DbSet<TypeOfDrive> TypeOfDrive { get; set; }
        public virtual DbSet<Vehicles> Vehicles { get; set; }
        public virtual DbSet<EngineTypes> EngineTypes { get; set; }
        public virtual DbSet<DriverLicenseCategories> DriverLicenseCategories { get; set; }
        public virtual DbSet<DriverLicenseCategory> DriverLicenseCategory { get; set; }
        public virtual DbSet<DriverLicenses> DriverLicenses { get; set; }
    }
}