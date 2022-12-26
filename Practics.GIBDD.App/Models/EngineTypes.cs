using System.Collections.Generic;

namespace Practics.GIBDD.App.Models
{
    public class EngineTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LocalizedName { get; set; }
    
        public virtual ICollection<Vehicles> Vehicles { get; set; }
    }
}