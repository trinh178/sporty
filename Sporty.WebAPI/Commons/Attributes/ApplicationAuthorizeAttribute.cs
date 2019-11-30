using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Sporty.Web.Commons.Attributes
{
    public class ApplicationAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            bool skipAuthorization = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(true).Count > 0
                                 || actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(true).Count > 0;

            if (skipAuthorization)
            {
                return;
            }

            var claimsIdentity = actionContext.RequestContext.Principal.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                this.HandleUnauthorizedRequest(actionContext);
            }

            // Check if the password has been changed. If it was, this token should be not accepted any more.
            // We generate a GUID stamp upon registration and every password change, and put it in every token issued.
            var securityStamp = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "AspNet.Identity.SecurityStamp");

            if (securityStamp == null)
            {
                // There was no stamp in the token.
                this.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                // Database
                var userManager = actionContext.Request.GetOwinContext().Get<ApplicationUserManager>();
                var userId = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var task = userManager.GetSecurityStampAsync(userId);
                task.Wait();
                var dbStamp = task.Result;

                if (securityStamp.Value != dbStamp)
                {
                    // The stamp has been changed in the DB.
                    this.HandleUnauthorizedRequest(actionContext);
                }
            }


            base.OnAuthorization(actionContext);
        }
    }
}