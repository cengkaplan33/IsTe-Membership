namespace Membership.Core
{
    public interface IWebHelper
    {        
        string GetUrlReferrer();

        string GetCurrentIpAddress();
     
        bool IsCurrentConnectionSecured();     
    }
}