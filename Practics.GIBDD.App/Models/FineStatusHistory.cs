using System;

namespace Practics.GIBDD.App.Models
{
    public class FineStatusHistory
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public int FineId { get; set; }
        public int FineStatusId { get; set; }
    
        public virtual Fines Fines { get; set; }
        public virtual FineStatuses FineStatuses { get; set; }
    }
}