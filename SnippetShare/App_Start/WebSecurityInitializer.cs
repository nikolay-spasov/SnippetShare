﻿using WebMatrix.WebData;

namespace SnippetShare.App_Start
{
    public class WebSecurityInitializer
    {
        public static readonly WebSecurityInitializer Instance = new WebSecurityInitializer();

        private bool isNotInit = true;
        private readonly object syncRoot = new object();

        private WebSecurityInitializer()
        {
        }

        public void EnsureInitialize()
        {
            if (isNotInit)
            {
                lock (this.syncRoot)
                {
                    if (isNotInit)
                    {
                        isNotInit = false;
                        WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                            "UserProfile", "UserId", "UserName", autoCreateTables: true);
                    }
                }
            }
        }
    }
}