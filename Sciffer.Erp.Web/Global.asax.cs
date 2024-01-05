using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Sciffer.Erp.Web.Infrastructure;
using Sciffer.Erp.Core;
using Sciffer.Erp.Data;
using Sciffer.Erp.Web.Infrastructure.Task;
using Newtonsoft.Json;
using System.Web;
using System;
using Sciffer.Erp.Web.ScheduleTask;

namespace Sciffer.Erp.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        protected void Application_Start()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            };
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(() => EngineContext.CurrentContainer ?? IoC.Initialize()));

     
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           

            if (EngineContext.CurrentContainer != null)
                using (var container = EngineContext.CurrentContainer.GetNestedContainer())
                {
                    var allInstances = container.GetAllInstances<IRunAtInit>();
                    foreach (var task in allInstances)
                    {
                        task.Execute();
                    }
                }
            planOrder.Start();
        }

        protected void Application_BeginRequest()
        {
            //Code for context per request pattern
            EngineContext.CurrentContainer = IoC.Initialize();
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            //Response.Cache.SetNoStore();
        }

        protected void Application_EndRequest()
        {
            //Code for context per request pattern
            if (EngineContext.CurrentContainer != null)
            {
                EngineContext.CurrentContainer.Dispose();
                EngineContext.CurrentContainer = null;
            }
        }
        protected void Application_Error(object sender, EventArgs e)
        {

            Exception ex = Server.GetLastError();

            log4net.GlobalContext.Properties["user"] = 1;
            log.Error("Global" , ex);
            if (ex.Message.ToString().Contains("inner exception"))
                log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();

        }

       }
    }
