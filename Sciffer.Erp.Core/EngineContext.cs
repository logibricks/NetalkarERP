using System.Web;
using StructureMap;

namespace Sciffer.Erp.Core
{
    public static class EngineContext
    {
        public static IContainer CurrentContainer
        {
            get
            {
                if (HttpContext.Current.Items["_container"] != null)
                {
                    return (IContainer)HttpContext.Current.Items["_cotainer"];
                }
                return IoC.Initialize().GetNestedContainer();
            }
            set
            {
                HttpContext.Current.Items["_container"] = value;
            }
        }
    }
}
