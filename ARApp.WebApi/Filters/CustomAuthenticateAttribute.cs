using ARApp.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ARApp.WebApi.Filters
{
    public class CustomAuthenticateAttribute
        : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            AuthenticateUser(actionContext);
        }

        private void AuthenticateUser(HttpActionContext actionContext)
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return;
            }
            base.OnAuthorization(actionContext);
            var AuthHeader = actionContext.Request.Headers.Authorization;
            if (AuthHeader != null)
            {
                if (AuthHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                   !string.IsNullOrWhiteSpace(AuthHeader.Parameter))
                {
                    var rawCredentials = AuthHeader.Parameter;
                    var encoding = Encoding.GetEncoding("iso-8859-1");
                    var credentials = encoding.GetString(Convert.FromBase64String(rawCredentials));
                    var split = credentials.Split(':');
                    var username = split[0];
                    var password = split[1];
                    if (username == "Amit" && password == "Redkar")
                    {
                        var principle = new GenericPrincipal(
                            new GenericIdentity(username), new[] { "Admin", "User" });
                        Thread.CurrentPrincipal = principle;
                        return;
                    }
                }
            }
            HandleUnauthorized(actionContext);
        }

        private void HandleUnauthorized(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW_Authenticate", "Basic Scheme='App' location='login page'");
        }
    }
}



/*
Fetch Role from currnt principle
System.Security.Principal.IPrincipal principle1 = System.Threading.Thread.CurrentPrincipal;
principle1.IsInRole("Admin");
true
*/
