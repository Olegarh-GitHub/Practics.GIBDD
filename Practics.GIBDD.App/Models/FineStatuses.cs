using System;
using System.Collections.Generic;

namespace Practics.GIBDD.App.Models
{
    public class FineStatuses
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public bool SendToFSSP { get; set; }
    
        public virtual ICollection<FineStatusHistory> FineStatusHistory { get; set; }

    }
}