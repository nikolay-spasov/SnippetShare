namespace SnippetShare.Instrastructure.WebSecurity.Concrete
{
    using WebMatrix.WebData;
    using SnippetShare.Instrastructure.WebSecurity;

    public class WebSecurityWrapper : IWebSecurity
    {
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