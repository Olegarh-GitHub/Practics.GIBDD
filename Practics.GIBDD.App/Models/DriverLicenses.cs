using System;
using System.Collections.Generic;

namespace Practics.GIBDD.App.Models
{
    public class DriverLicenses
    {
        public int Id { get; set; }
        public Guid DriverId { get; set; }
        public DateTime LicenseDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public DateTime DateCreated { get; set; }
    
        public virtual ICollection<DriverLicenseCategory> DriverLicenseCategory { get; set; }
        public virtual Drivers Drivers { get; set; }
    }
}