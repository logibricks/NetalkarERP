using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class NotificationTypeController : Controller
    {
        private readonly INotificationTypeService _notificationService;
        private readonly IGenericService _Generic;
        public NotificationTypeController(INotificationTypeService notificationService, IGenericService gen)
        {
            _notificationService = notificationService;
            _Generic = gen;
        }
        // GET: Country
        [CustomAuthorizeAttribute("CNTRY")]
        public ActionResult Index()
        {
            ViewBag.datasource = _notificationService.GetAll();
            return View();
        }
        public ActionResult InlineDelete(int key)
        {
            var res = _notificationService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(REF_NOTIFICATION_TYPE value)
        {

            var add = _Generic.CheckDuplicate(value.NOTIFICATION_TYPE, "", "", "NotificationType", value.NOTIFICATION_ID);
            if (add == 0)
            {
                if (value.NOTIFICATION_ID == 0)
                {
                    var data1 = _notificationService.Add(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _notificationService.Update(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { text = value.NOTIFICATION_TYPE }, JsonRequestBehavior.AllowGet);

            }
        }



        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _notificationService.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}