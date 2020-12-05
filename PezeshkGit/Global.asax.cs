using AutoMapper;
using log4net;
using log4net.Config;
using PezeshkGit.App_Start;
using System;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PezeshkGit
{
    public class MvcApplication : HttpApplication
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MvcApplication));
        protected void Application_Start()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            XmlConfigurator.Configure();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastErrorInfo = Server.GetLastError();
            Exception errorInfo = null;

            bool isNotFound = false;
            bool isInternal = false;
            bool isBadRequest = false;
            if (lastErrorInfo != null)
            {
                errorInfo = lastErrorInfo.GetBaseException();
                var error = errorInfo as HttpException;
                if (error != null)
                {
                    isNotFound = error.GetHttpCode() == (int)HttpStatusCode.NotFound;
                    isInternal = error.GetHttpCode() == (int)HttpStatusCode.InternalServerError;
                    isBadRequest = error.GetHttpCode() == (int)HttpStatusCode.BadRequest;
                }
            }
            if (isNotFound)
            {
                Server.ClearError();
                Response.Redirect("~/Error/NotFound");
            }
            else if (isInternal)
            {
                Log.Error("Got an Internal error: ", errorInfo);
                Server.ClearError();
                Response.Redirect("~/Error/InternalError");
            }
            else if (isBadRequest)
            {
                Server.ClearError();
                Response.Redirect("~/Error/BadRequest");
            }
        }
    }
}
