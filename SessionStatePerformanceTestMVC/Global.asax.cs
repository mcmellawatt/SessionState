using Apache.Geode.Client;
using Pivotal.GemFire.Session;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SessionStatePerformanceTestMVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private Cache Cache { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Properties<string, string> properties = new Properties<string, string>();
            properties.Load("c:\\dev\\gemfire-dotnetsession\\SampleWebApp\\geode.properties");
            properties.Insert("cache-xml-file", "c:\\dev\\gemfire-dotnetsession\\SampleWebApp\\cache.xml");
            properties.Insert("appdomain-enabled", "true");

            var cacheFactory = CacheFactory.CreateCacheFactory(properties);

            Cache = cacheFactory.Create();
            Serializable.RegisterPdxSerializer(new GetItemExclusiveSerializer(new SessionStateStoreDataSerializer()));
        }
    }
}