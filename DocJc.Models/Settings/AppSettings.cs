namespace DocJc.Model.Settings
{
    using System.Collections.Generic;

    public class AppSettings
    {
        public HealthSettings HealthSettings { get; set; }
        public string MockUser { get; set; }
        public string MockPass { get; set; }
    }

    public class SandboxUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class HealthSettings
    {
        public string HealthServiceAuthenticationEndpoint { get; set; }
        public string HealthServiceDataEndpoint { get; set; }
        public List<SandboxUser> SandboxUsers { get; set; }
   }
}
