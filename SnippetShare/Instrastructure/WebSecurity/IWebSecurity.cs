namespace SnippetShare.Instrastructure.WebSecurity
{
    public interface IWebSecurity
    {
        int CurrentUserId { get; }

        bool IsAuthenticated { get; }
    }
}