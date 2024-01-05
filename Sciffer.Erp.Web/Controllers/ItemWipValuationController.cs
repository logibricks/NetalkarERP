using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;

namespace Sciffer.Erp.Web.Controllers
{
    public class ItemWipValuationController : Controller
    {

        private readonly IGenericService _Generic;
        private readonly IItemService _Item;
        private readonly IMachineMasterService _MachineMaster;
        private readonly IItemWipValuationService _Itemwip;


        public ItemWipValuationController(IItemService item, IMachineMasterService machinemaster, IItemWipValuationService itemwip, IGenericService Generic) {
            _Item = item;
            _MachineMaster = machinemaster;
            _Itemwip = itemwip;
            _Generic = Generic;
        }



        // GET: ItemWipValuation
        public ActionResult Index()
        {

            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.ItemWIPValue = _Itemwip.GetAll();
            return View();
        }

        public ActionResult InlineDelete(int key)
        {
            var res = _Itemwip.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(VM_ref_item_wip_valuation value)
        {
            if (value.item_wip_valuation_id == 0)
            {
                var data1 = _Itemwip.Add(value);

                return Json(data1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data1 = _Itemwip.Update(value);
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
        }



    }
}