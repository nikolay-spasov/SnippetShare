namespace SnippetShare.App_Start
{
    using WebMatrix.WebData;

    public class WebSecurityInitializer
    {
        public static readonly WebSecurityInitializer Instance = new WebSecurityInitializer();

        private readonly object syncRoot = new object();
        private bool isNotInit = true;

        private WebSecurityInitializer()
        {
        }

        public void EnsureInitialize()
        {
            if (this.isNotInit)
            {
                lock (this.syncRoot)
                {
                    if (this.isNotInit)
                    {
                        this.isNotInit = false;
                        WebSecurity.InitializeDatabaseConnection(
                            "DefaultConnection",
                            "UserProfile", 
                            "UserId", 
                            "UserName", 
                            autoCreateTables: true);
                    }
                }
            }
        }
    }
}