using System;
using System.Collections.Generic;

namespace Practics.GIBDD.App.Models
{
    public class Vehicles
    {
        public int Id { get; set; }
        public Guid DriverId { get; set; }
        public string VINCode { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Weight { get; set; }
        public int CarColorId { get; set; }
        public int EngineTypeId { get; set; }
        public int TypeOfDriveId { get; set; }
        public DateTime DateCreated { get; set; }
    
        public virtual CarColors CarColors { get; set; }
        public virtual Drivers Drivers { get; set; }
        
        public virtual ICollection<RegNumbers> RegNumbers { get; set; }
        
        public virtual TypeOfDrive TypeOfDrive { get; set; }
        public virtual EngineTypes EngineTypes { get; set; }
    }
}