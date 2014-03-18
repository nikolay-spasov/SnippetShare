using WebMatrix.WebData;
namespace SnippetShare.Instrastructure
{
    public class WebSecurityWrapper : IWebSecurity
    {
        public bool Login(string userName, string password, bool persistCookie = false)
        {
            return WebSecurity.Login(userName, password, persistCookie);
        }

        public void Logout()
        {
            WebSecurity.Logout();
        }

        public string CreateUserAndAccount(string userName, string password, 
            object propertyValues = null, bool requireConfirmationToken = false)
        {
            return WebSecurity.CreateAccount(userName, password, requireConfirmationToken);
        }

        public bool ChangePassword(string userName, string currentPassword, string newPassword)
        {
            return WebSecurity.ChangePassword(userName, currentPassword, newPassword);
        }

        public string CreateAccount(string userName, string password, bool requireConfirmationToken = false)
        {
            return WebSecurity.CreateAccount(userName, password, requireConfirmationToken);
        }

        public int CurrentUserId
        {
            get { return WebSecurity.CurrentUserId; }
        }

        public bool IsAuthenticated
        {
            get { return WebSecurity.IsAuthenticated; }
        }
    }
}