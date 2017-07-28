# How to create custom principal?
> With the help of IIdentity and IPrincipal interfaces, we can create custom principal and custom identity.

# How to create custom Identity?
```C#
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
```

# How to create custom principal?

```C#
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
            //implement role base authorization
        }
    }
```

# How to bind custom principal over current principal?
> In Global.asax.cs file need to override Init method and implement application_postauthentication() method.

```C#
    public class MvcApplication : System.Web.HttpApplication
    {
        .....        
        public override void Init()
        {
            this.PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    var customprincipal = new CustomPrincipal(authTicket.Name, "forms");
                    Context.User = Thread.CurrentPrincipal = customprincipal;
                }
            }
        }
    }
```

# How to fetch custom identity values?

```C#
var FullName = ((CustomIdentity)User.Identity).FullName;
```
