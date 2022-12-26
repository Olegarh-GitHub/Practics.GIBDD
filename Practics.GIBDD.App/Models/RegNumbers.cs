using System;

namespace Practics.GIBDD.App.Models
{
    public class RegNumbers
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int RegionCodeCodeId { get; set; }
        public string Series { get; set; }
        public int Number { get; set; }
        public DateTime DateCreated { get; set; }
    
        public virtual RegionCodeCodes RegionCodeCodes { get; set; }
        public virtual Vehicles Vehicles { get; set; }
    }
}