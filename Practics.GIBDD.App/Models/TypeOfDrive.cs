using System;
using System.Collections.Generic;

namespace Practics.GIBDD.App.Models
{
    public class TypeOfDrive
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    
        public virtual ICollection<Vehicles> Vehicles { get; set; }
    }
}