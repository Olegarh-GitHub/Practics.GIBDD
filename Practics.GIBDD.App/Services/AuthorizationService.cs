using System;
using System.Collections.Generic;
using System.Linq;
using Practics.GIBDD.App.Exceptions;
using Practics.GIBDD.App.Models;

namespace Practics.GIBDD.App.Services
{
    public class AuthorizationService : IDisposable
    {
        private const int AUTHORIZATION_ATTEMPTS_THRESHOLD_MINUTES = 1;
        private const int MAX_AUTHORIZATION_ATTEMPTS = 3;
        private readonly ApplicationContext _db;

        public AuthorizationService()
        {
            _db = new ApplicationContext();
        }
        public AuthorizationService(ApplicationContext db)
        {
            _db = db;
        }
        public Users Auth(string login, string password)
        {
            var authorizationAttempts = GetAuthorizationAttempts(login);
            if (authorizationAttempts.Count >= MAX_AUTHORIZATION_ATTEMPTS)
            {
                throw new TooManyAuthorizationAttempts();
            }

            RegisterAuthorizationAttempt(login);

            var user = Identify(login);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            if (Authenticate(user, password))
            {
                return user;
            }
            throw new AuthenticationFail();

        }

        private Users Identify(string login)
        {
            var user = _db.Users.FirstOrDefault(x => x.Login == login);
            return user;
        }

        private bool Authenticate(Users user, string password)
        {
            return user.Password == password;
        }

        private string GetPersonIdentity()
        {
            return Environment.MachineName.ToString();
        }

        private void RegisterAuthorizationAttempt(string login)
        {
            _db.AuthorizationAttempts.Add(
                new AuthorizationAttempts()
                {
                    Login = login,
                    PersonIdentity = GetPersonIdentity(),
                    AttemptDate = DateTime.Now
                }
            );
            _db.SaveChanges();
        }

        private List<AuthorizationAttempts> GetAuthorizationAttempts(string login)
        {
            var sinceDate = DateTime.Now;
            sinceDate = sinceDate.AddMinutes(-AUTHORIZATION_ATTEMPTS_THRESHOLD_MINUTES);

            return _db.AuthorizationAttempts.Where(x =>x.Login == login).Where(x => x.AttemptDate > sinceDate).ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}