using GrupoThera.Business.Interfaces;
using GrupoThera.Business.Managers;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using GrupoThera.Entities.Entity;
using System.Reflection;

namespace GrupoThera.WebUI.Controllers.api.Filters
{
    public class GenericAuthenticationFilter : AuthorizeAttribute 
    {
        private bool authentication;
        //private IUserInfoService _userInfoManager; 

        //[Inject]   
        //public void UserInfoManager(IUserInfoService userInfoManager)
        //{
        //    _userInfoManager = userInfoManager;   
        //} 

        public GenericAuthenticationFilter(bool _authentication ) {
            authentication = _authentication; 
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!authentication)
                return;
            if(Authorize(actionContext) == 1)
                return;
            if (Authorize(actionContext) == 2)
                return;
            HandleUnauthorizedRequest(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var challengeMessage = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            challengeMessage.Headers.Add("WWW-Authenticate", "Basic");
            challengeMessage.Content = new StringContent("You are unauthorized to access this resource");
            actionContext.Response = challengeMessage;  
        }

        private int Authorize(HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.Request.Headers.GetValues("REMOTE_USER") != null)
                {
                    //var hashcode = actionContext.Request.Headers.GetValues("REMOTE_USER");  
                    //var userInfo = _userInfoManager.GetByUsername(hashcode.SingleOrDefault());  
                    // if (userInfo == null)
                    //     throw new Exception();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                var challengeMessage = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                challengeMessage.Headers.Add("WWW-Authenticate", "Basic");
                challengeMessage.Content = new StringContent(ex.Message);
                actionContext.Response = challengeMessage;
                return 2;
            }
            return 2;
        }

        
    } 
    


}