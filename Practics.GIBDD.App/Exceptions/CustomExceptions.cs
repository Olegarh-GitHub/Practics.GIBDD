using System;

namespace Practics.GIBDD.App.Exceptions
{
    public class UserNotFoundException : Exception { }
    public class AuthenticationFail : Exception { }
    public class TooManyAuthorizationAttempts : Exception { }
}