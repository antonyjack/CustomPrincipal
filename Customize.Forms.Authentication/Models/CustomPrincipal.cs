using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Customize.Forms.Authentication.Models
{
    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(string userName, string type)
        {
            this.Identity = new CustomIdentity(userName, type);
        }

        public IIdentity Identity
        {
            get;
            private set;
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }

    public class CustomIdentity : IIdentity
    {

        public CustomIdentity(string userName, string authType)
        {
            this.Name = userName;
            this.AuthenticationType = authType;
            this.IsAuthenticated = true;
            this.FullName = "Test Username";
        }
        public string AuthenticationType
        {
            get;
            set;
        }

        public bool IsAuthenticated
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string FullName
        {
            get;
            set;
        }
    }
}