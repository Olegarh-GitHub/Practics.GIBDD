using System;
using System.Collections.Generic;

namespace Practics.GIBDD.App.Models
{
    public class RegionCodes
    {
        public int Id { get; set; }
        public string RegionNameEN { get; set; }
        public string RegionNameRU { get; set; }
        public int? SubjectCode { get; set; }
        public string OKATOCode { get; set; }
        public string ISO3166_2Code { get; set; }
    
        public virtual ICollection<RegionCodeCodes> RegionCodeCodes { get; set; }
    }
}