using System;
using System.Collections.Generic;

namespace Practics.GIBDD.App.Models
{
    public class CarColors
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string HexCode { get; set; }
        public string Name { get; set; }
        public string ColorName { get; set; }
        public bool IsMetallic { get; set; }
        public string NameEN { get; set; }
        public string ColorNameEN { get; set; }
        public DateTime DateCreated { get; set; }
    
        public virtual ICollection<Vehicles> Vehicles { get; set; }
    }
}