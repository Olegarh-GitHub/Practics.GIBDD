using System;
using System.Collections.Generic;

namespace Practics.GIBDD.App.Models
{
    public class Fines
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string CarNum { get; set; }
        public string LicenseNum { get; set; }
        public DateTime CreateDate { get; set; }
        public int PhotoId { get; set; }
    
        public virtual Photos Photos { get; set; }
        
        public virtual ICollection<FineStatusHistory> FineStatusHistory { get; set; }

    }
}