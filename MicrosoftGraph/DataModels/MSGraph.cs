using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace MicrosoftGraph
{
    public class MSGraph : ProviderBase
    {
        public MSGraph()
        {
            // MSA can only use "User.Read", "User.ReadWrite"
            var scopeList = new string[] { "User.Read", "User.ReadWrite", "User.ReadBasic.All", "User.Read.All", "User.ReadWrite.All", "User.Invite.All", "User.Read.All", "User.ReadWrite.All", "User.Invite.All" };

            Scope = scopeList.ToList<string>();
            appID = "1929285880642177";
            aPIVersion = "v2.0";

            var client_id = "6731de76 - 14a6 - 49ae - 97bc - 6eba6914391e";
            var code ="&code = OAAABAAAAiL9Kn2Z27UubvWFPbm0gLWQJVzCTE9UkP3pSx1aXxUjq3n8b2JRLk4OxVXr...";
            var redirect_uri = "&redirect_uri = http % 3A % 2F % 2Flocalhost % 2Fmyapp % 2F";
            var grant_type = "&grant_type = authorization_code";
            // for web app
            var client_secret = "&client_secret = JqQX2PNo9bpM0uEihUPzyrh";

            scopeParameter = "&scope=";http://blog.processtune.com/wp-admin/plugins.php
            scopeParameter += string.Join(",", Scope.Where(s => s == "public_profile" || s == "email"));
            //if OAuth server not support "", use below as redirect URL
            //RedirectURL = "https://www.facebook.com/connect/login_success.html";

            Uri callBackUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri();
            RedirectURL = callBackUri.AbsoluteUri;
            OptionalParameters = "&display=popup&response_type=token";
            CurrentProviderTypes = ProviderTypes.FaceBook;
            Public_Profile = new PublicProfile();
            //[Obsolete]

        }
        string aPIVersion { get; set; }
        string appID { get; set; }
        string scopeParameter { get; set; }
        public string OptionalParameters { get; set; }
        //https://login.microsoftonline.com/common/oauth2/v2.0/authorize
        //https://login.microsoftonline.com/common/oauth2/v2.0/token
        public string OAuthRequestURL
        {
            get
            {
                return "https://graph.microsoft.com/v2.0/me/";
            }
        }
        public string PublicProfileRequestURL
        {
            get
            {
                return "https://graph.facebook.com/v2.9/me?access_token=" + AccessToken;
            }
        }
        public string UserRequestURL
        {
            get
            {
                return "https://graph.facebook.com/v2.9/" + Public_Profile.id + "?access_token=" + AccessToken;
            }
        }
        public string RedirectURL { get; set; }
        string APIVersionString { get; set; }
        public User FaceBookUserInfo { get; set; }
        public PublicProfile Public_Profile { get; set; }
        public class PublicProfile
        {
            public string id { get; set; }
            public string cover { get; set; }
            public string name { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string age_range { get; set; }
            public string link { get; set; }
            public string gender { get; set; }
            public string locale { get; set; }
            public string picture { get; set; }
            public string timezone { get; set; }
            public string updated_time { get; set; }
            public string verified { get; set; }
        }

        enum install_type
        {
        }
        public class User
        {
            public List<string> BasicInfo = new List<string>();
            public List<string> OrganizationInfo = new List<string>();
            public User()
            {
                BasicInfo.Add("displayName");
                BasicInfo.Add("givenName");
                BasicInfo.Add("mail");
                BasicInfo.Add("photo");
                BasicInfo.Add("surname");
                BasicInfo.Add("userPrincipalName");
                OrganizationInfo.Add("aboutMe");
                OrganizationInfo.Add("birthday");
                OrganizationInfo.Add("hireDate");
                OrganizationInfo.Add("interests");
                OrganizationInfo.Add("mobilePhone");
                OrganizationInfo.Add("mySite");
                OrganizationInfo.Add("pastProjects");
                OrganizationInfo.Add("photo");
                OrganizationInfo.Add("preferredName");
                OrganizationInfo.Add("responsibilities");
                OrganizationInfo.Add("schools");
                OrganizationInfo.Add("skills");
            }
            public string displayName { get; set; }
            public string givenName { get; set; }
            public string mail { get; set; }
            public string photo { get; set; }
            public string surname { get; set; }
            public string userPrincipalName { get; set; }
            public string jobTitle { get; set; }
            public string mobilePhone { get; set; }
            public string officeLocation { get; set; }
            public string preferredLanguage { get; set; }
            public string aboutMe { get; set; }
            public string birthday { get; set; }
            public string hireDate { get; set; }
            public string interests { get; set; }
            public string mySite { get; set; }
            public string pastProjects { get; set; }
            public string preferredName { get; set; }
            public string responsibilities { get; set; }
            public string schools { get; set; }
            public string skills { get; set; }
        }
    }
}
