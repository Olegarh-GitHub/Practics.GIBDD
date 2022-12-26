using System;
using System.Collections.Generic;

namespace Practics.GIBDD.App.Models
{
    public class Photos
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public DateTime? DateCreated { get; set; }
    
        public virtual ICollection<Drivers> Drivers { get; set; }
        public virtual ICollection<Fines> Fines { get; set; }
    }
}