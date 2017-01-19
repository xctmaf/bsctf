namespace BSCTF
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Security;
    using ByndyuSoft.Infrastructure.Common.Extensions;
    using Dapper;
    using Domain.Entities;
    using Newtonsoft.Json;
    using Web.Application.Models.User.Output;

    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(string login, string userName)
        {
            Name = login;
            User = new UserModel {Login = login, Username = userName};
            IsAuthenticated = true;
        }

        public string Name { get; }
        public UserModel User { get; }
        public string AuthenticationType { get; }
        public bool IsAuthenticated { get; }
    }

    public class AuthenticationModule : IHttpModule
    {
        private static string _authCookieName;

        public void Init(HttpApplication httpApplication)
        {
            _authCookieName = ConfigurationManager.AppSettings["AuthCookieName"] ?? "basctfuauth";
        }

        public static void OnAuthenticateRequest(object sender, EventArgs e)
        {
            var httpApplication = (HttpApplication)sender;
            var cookie = httpApplication.Request.Cookies[_authCookieName];

            if (cookie == null)
                return;

            try
            {
                var cookieValue = Encoding.UTF8.GetString(MachineKey.Unprotect(HttpUtility.UrlDecode(cookie.Value).FromBase64().ToArray(), WebConfigurationManager.AppSettings["cryptoKey"]));
                var user = JsonConvert.DeserializeObject<UserModel>(cookieValue);

                if ( string.IsNullOrWhiteSpace(user.Login) || string.IsNullOrWhiteSpace(user.Username) || IsUserExist(user.Login) == false)
                    return;

                var identity = new CustomIdentity(user.Login, user.Username);
                var principal = new GenericPrincipal(identity, new string[0]);

                HttpContext.Current.User = principal;
                Thread.CurrentPrincipal = principal;
            }
            catch
            {
                // ignored
            }
        }

        private static bool IsUserExist(string login)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
            {
                connection.Open();
                var user = connection.Query<User>(@"SELECT Login, Password, Username, Id, Salt FROM Users WHERE Login = '" + login + "'").SingleOrDefault();
                if (user == null)
                    return false;
            }
            return true;
        }

        public void Dispose()
        {
        }
    }
}