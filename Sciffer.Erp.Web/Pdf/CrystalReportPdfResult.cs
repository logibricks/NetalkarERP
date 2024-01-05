using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

using CrystalDecisions.Shared;

namespace Sciffer.Erp.Web
{
    public class CrystalReportPdfResult : ActionResult

    {

        //private ERPDbContext mde = new ERPDbContext();

        private readonly byte[] _contentBytes;

        public CrystalReportPdfResult(string reportPath, ReportDocument ReportDocument)
        {
                      _contentBytes = StreamToBytes(ReportDocument.ExportToStream(ExportFormatType.PortableDocFormat));
        }
       public override void ExecuteResult(ControllerContext context)
        {

            var response = context.HttpContext.ApplicationInstance.Response;
            response.Clear();
            response.Buffer = false;
            response.ClearContent();
            response.ClearHeaders();
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.ContentType = "application/pdf";

            using (var stream = new MemoryStream(_contentBytes))
            {
                stream.WriteTo(response.OutputStream);
                stream.Flush();
            }
        }

        private static byte[] StreamToBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}