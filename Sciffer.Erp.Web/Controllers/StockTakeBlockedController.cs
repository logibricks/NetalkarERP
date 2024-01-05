using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class StockTakeBlockedController : Controller
    {
        private readonly IStockTakeBlockedService _stockBlocked;
        private readonly IGenericService _Generic;
        private readonly IBucketService _bucket;
        public StockTakeBlockedController(IStockTakeBlockedService stockBlocked, IGenericService gen, IBucketService bucket)
        {
            _stockBlocked = stockBlocked;
            _Generic = gen;
            _bucket = bucket;
        }
        // GET: StockTakeBlocked
        public ActionResult Index()
        {
            ViewBag.datasource = _stockBlocked.GetAll();
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sloc_list = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.bucket_list = new SelectList(_bucket.GetAll(), "bucket_id", "bucket_name");
            ViewBag.item_category_list = new SelectList(_Generic.GetItemCategoryList(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }            
        public ActionResult InlineInsert(stock_take_blocked_vm value)
        {

            if (value.stock_take_blocked_id == 0)
            {
                var data1 = _stockBlocked.Add(value);
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data1 = _stockBlocked.Update(value);
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _stockBlocked.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}