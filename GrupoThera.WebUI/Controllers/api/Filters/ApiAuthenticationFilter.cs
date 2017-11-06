using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;

namespace Mdm.WebUI.Controllers.api.Filters
{
    /// <summary>
    /// Custom Authentication Filter Extending basic Authentication
    /// </summary>
    public class ApiAuthenticationFilter : GenericAuthenticationFilter
    { 
        /// <summary>
        /// AuthenticationFilter constructor with isActive parameter
        /// </summary>
        /// <param name="isActive"></param>
        public ApiAuthenticationFilter(bool isActive)
            : base(isActive)
        {
        }

        
       /// <summary>
       /// Protected overriden method for authorizing user
       /// </summary> 
       /// <param name="actionContext"></param>
       /// <returns></returns>
       protected override bool OnAuthorizeUser(HttpActionContext actionContext)
       {
           try
           {
               if (actionContext.Request.Headers.GetValues("REMOTE_USER") != null)
               {
                   var hashcode = actionContext.Request.Headers.GetValues("REMOTE_USER");

                   return true;
               }
           }
           catch (Exception ex) {
               return false;
           }
           return false;
       }
       
    }

}