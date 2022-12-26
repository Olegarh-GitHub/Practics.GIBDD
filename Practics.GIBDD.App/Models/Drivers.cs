using System;
using System.Collections.Generic;

namespace Practics.GIBDD.App.Models
{
    public class Drivers
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Passport { get; set; }
        public string RegAddress { get; set; }
        public string LiveAddress { get; set; }
        public string PlaceOfWork { get; set; }
        public string WorkPosition { get; set; }
        public string PhoneMobile { get; set; }
        public string Email { get; set; }
        public int PhotoId { get; set; }
        public string Remarks { get; set; }

        public string FullName => $"{LastName} {FirstName} {MiddleName}";
    
        public virtual Photos Photos { get; set; }
        
        public virtual ICollection<Vehicles> Vehicles { get; set; }
        
        public virtual ICollection<DriverLicenses> DriverLicenses { get; set; }

    }
}