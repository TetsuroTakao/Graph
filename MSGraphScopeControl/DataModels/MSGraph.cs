using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Security.Authentication.Web;

namespace MSGraphScopeControl
{
    public class MSGraph : ProviderBase
    {
        public MSGraph()
        {
            // MSA can only use "User.Read", "User.ReadWrite"
            var scopeList = new string[] { "User.Read", "User.ReadWrite", "User.ReadBasic.All", "User.Read.All", "User.ReadWrite.All", "User.Invite.All"};
            Scope = scopeList.ToList<string>();
            tenantType = TenantType.common;
            apiVersion = "v2.0";
            //App.Current.Resources["ida:ClientID"] = "3b2ae2b7-cec6-49e0-b473-e86bae79dc9c";
            //be6ed449-d477-43c0-a29e-ff74cdce00f9
            //3b2ae2b7-cec6-49e0-b473-e86bae79dc9c
            var client_id = "client_id=3b2ae2b7-cec6-49e0-b473-e86bae79dc9c";
            OptionalParameters += client_id;
            var grant_type = "response_type=code";
            OptionalParameters += "&" + grant_type;
            //OAuth basic redirect url
            //https://login.microsoftonline.com/common/oauth2/nativeclient
            //UWP
            Uri callBackUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri();
            RedirectURL = callBackUri.AbsoluteUri;

            OptionalParameters += "&" + RedirectURL;
            var response_mode = "response_mode=query";
            OptionalParameters += "&" + response_mode;
            var scopeParameter = "scope = openid offline_access User.Read";
            OptionalParameters += "&" + scopeParameter;

            StateCode = Guid.NewGuid().ToString();
            var state = "state=" + StateCode;
            OptionalParameters += "&" + state;
            CurrentProviderTypes = ProviderTypes.MicrosoftGraph;

            Public_Profile = new User();
        }
        public DateTimeOffset TokenExpire { get; set; }
        string apiVersion { get; set; }
        public string StateCode { get; set; }
        //string scopeParameter { get; set; }
        public string OptionalParameters { get; set; }
        public string OAuthRequestURL
        {
            get
            {
                return "https://login.microsoftonline.com/" + tenantType.ToString() + "/oauth2/" + apiVersion + "/authorize?" + OptionalParameters;
            }
        }
        public string PublicProfileRequestURL
        {
            get
            {
                return "https://graph.facebook.com/v2.9/me?access_token=" + AccessToken;
            }
        }
        public string TokenRequestURL
        {
            get
            {
                return "https://login.microsoftonline.com/" + tenantType.ToString() + "/oauth2/" + apiVersion + "/token";
            }
        }
        TenantType tenantType { get; set; }
        public string RedirectURL { get; set; }
        string APIVersionString { get; set; }
        public User FaceBookUserInfo { get; set; }
        public User Public_Profile { get; set; }
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
    public enum TenantType
    {
        common,
        organizations,
        consumers,
        tenantFriendlyName,
        tenantGUID
    }
    [Flags]
    public enum UserScopeType
    {
        Read=1,
        ReadWrite=2,
        ReadBasicAll=4,
        ReadAll=8,
        ReadWriteAll=16,
        InviteAll=32
    }
}
