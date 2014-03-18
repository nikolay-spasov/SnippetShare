namespace SnippetShare.Instrastructure
{
    public interface IWebSecurity
    {
        bool Login(string userName, string password, bool persistCookie = false);
        void Logout();
        string CreateUserAndAccount(string userName, string password, object propertyValues = null,
               bool requireConfirmationToken = false);
        bool ChangePassword(string userName, string currentPassword, string newPassword);
        string CreateAccount(string userName, string password, bool requireConfirmationToken = false);
        
        int CurrentUserId { get; }
        bool IsAuthenticated { get; }
    }
}