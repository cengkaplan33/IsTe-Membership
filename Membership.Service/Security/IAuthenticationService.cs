using Membership.Service.Models.Authentication;

namespace Membership.Service.Security
{
    public interface IAuthenticationService
    {
        AuthenticationResult SignIn(AuthenticationRequest request);

        LogoutResult Logout(LogoutRequest request);        
    }
}