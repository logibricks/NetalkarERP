using StructureMap;

namespace Sciffer.Erp.Core
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            return new Container(c =>
            {
                c.Scan(scan =>
                {
                    scan.Assembly("Sciffer.Erp.Web");
                    scan.Assembly("Sciffer.Erp.Service");
                    scan.WithDefaultConventions();
                    scan.LookForRegistries();

                });
            });
        }
    }
}