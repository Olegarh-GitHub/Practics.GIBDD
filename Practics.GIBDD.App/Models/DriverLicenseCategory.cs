using System;

namespace Practics.GIBDD.App.Models
{
    public class DriverLicenseCategory
    {
        public int Id { get; set; }
        public int DriverLicenseId { get; set; }
        public int DriverLicenseCategoryId { get; set; }
        public DateTime DateCreated { get; set; }
    
        public virtual DriverLicenseCategories DriverLicenseCategories { get; set; }
        public virtual DriverLicenses DriverLicenses { get; set; }
    }
}