using System.Collections.Generic;

namespace Practics.GIBDD.App.Models
{
    public class RegionCodeCodes
    {
        public int Id { get; set; }
        public int RegionCodeId { get; set; }
        public int Code { get; set; }
    
        public virtual RegionCodes RegionCodes { get; set; }
        public virtual ICollection<RegNumbers> RegNumbers { get; set; }
    }
}