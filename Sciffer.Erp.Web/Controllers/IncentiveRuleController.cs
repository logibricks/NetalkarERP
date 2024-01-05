using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class IncentiveRuleController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IItemService _Item;
        private readonly IMachineMasterService _MachineMaster;
        private readonly IIncentiveRuleService _IncentiveRule;


        public IncentiveRuleController(IItemService item, IMachineMasterService machinemaster, IIncentiveRuleService incentiveRule, IGenericService Generic)
        {
            _Item = item;
            _MachineMaster = machinemaster;
            _IncentiveRule = incentiveRule;
            _Generic = Generic;
        }



        // GET: ItemWipValuation
        [CustomAuthorizeAttribute("INCENTIVE")]
        public ActionResult Index()
        {

            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.ItemWIPValue = _IncentiveRule.GetAll();
            return View();
        }

        public ActionResult InlineDelete(int key)
        {
            var res = _IncentiveRule.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(VM_incentive_rule value)
        {
            if (value.incentive_rule_id == 0)
            {
                var data1 = _IncentiveRule.Add(value);

                return Json(data1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data1 = _IncentiveRule.Update(value);
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
        }

    }
}