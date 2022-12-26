using System;

namespace Practics.GIBDD.App.Models
{
    public class AuthorizationAttempts
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PersonIdentity { get; set; }
        public DateTime AttemptDate { get; set; }
    }
}