using Eumis.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Eumis.Portal.Web.Helpers;
using Microsoft.Ajax.Utilities;

namespace Eumis.Portal.Web.Controllers
{
    [Serializable]
    public enum SplashType
    {
        Welcome = 1,
        MessageNotification = 2,
    }

    [Serializable]
    public class SplashContext
    {
        private static readonly string SplashContextSessionKey = "SplashContextSessionKey";

        public Dictionary<SplashType, bool> State = new Dictionary<SplashType, bool>();

        public static SplashContext Current
        {
            get
            {
                return (SplashContext)System.Web.HttpContext.Current.Session[SplashContextSessionKey];
            }
            set
            {
                System.Web.HttpContext.Current.Session[SplashContextSessionKey] = value;
            }
        }
    }

    //[RoutePrefix("api/splash")]
    public class SplashController : ApiController
    {
        public SplashController()
        {
            if (SplashContext.Current == null)
            {
                SplashContext.Current = new SplashContext()
                {
                    State = new Dictionary<SplashType, bool>()
                    {
                        { SplashType.Welcome, false },
                        { SplashType.MessageNotification, false }
                    }
                };
            }
        }

        [HttpGet]
        public bool IsShown(SplashType type)
        {
            return SplashContext.Current.State[type];
        }

        [HttpPost]
        public void MarkAsShown(SplashType type)
        {
            SplashContext.Current.State[type] = true;
        }
    }
}
