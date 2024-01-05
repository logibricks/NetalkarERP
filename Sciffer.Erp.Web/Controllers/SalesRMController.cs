using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class SalesRMController : Controller
    {
        private readonly ISalesRMService _salesRM;
        private readonly IGenericService _Generic;
        private readonly IEmployeeService _employee;

        public SalesRMController(ISalesRMService sales_RM,IGenericService Generic,IEmployeeService employee)
        {
            _salesRM = sales_RM;
            _Generic = Generic;
            _employee = employee;
        }
        public ActionResult InlineDelete(int key)
        {
            var res = _salesRM.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorizeAttribute("SLSRM")]
        public ActionResult Index()
        {
            ViewBag.employee_list = _employee.GetEmployeeCode();
            ViewBag.DataSource = _salesRM.GetAll();
            return View();
        }
       
        public JsonResult GetSalesRM()
        {
            ViewBag.datasource = _employee.GetAll();
            var data = _salesRM.GetAll();
            return Json(new { result = data, count = data.Count }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Data(ref_sales_rm value)
        {
            var data = 0;
            var data1 = true;
            ViewBag.datasource = _employee.GetAll();
            data = _Generic.CheckDuplicate(value.employee_id.ToString(),"", "", "SalesRM", value.sales_rm_id);


            bool duplicate = false;
            if (data > 0)
            {

                duplicate = false;
                return Json(duplicate, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (value.sales_rm_id == 0)
                {
                    data1 = _salesRM.Add(value);
                }
                else
                {
                    data1 = _salesRM.Update(value);
                }

                // duplicate = true;
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
        }
    }
}