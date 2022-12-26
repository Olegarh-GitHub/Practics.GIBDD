using System;

namespace Practics.GIBDD.App.Models
{
    public class Users
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}