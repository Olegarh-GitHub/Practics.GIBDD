using System;
using System.Collections.Generic;

namespace Practics.GIBDD.App.Models
{
    public class DriverLicenseCategories
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime DateCreated { get; set; }
    
        public virtual ICollection<DriverLicenseCategory> DriverLicenseCategory { get; set; }

    }
}