
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IGoodsReceiptService:IDisposable
    {
        List<GOODS_RECEIPT_VM> GetAll();
        List<GOODS_RECEIPT_VM> getall();
        GOODS_RECEIPT_VM Get(int id);
        string Add(GOODS_RECEIPT_VM goodsReceipt);
        bool Update(GOODS_RECEIPT_VM goodsReceipt);
        bool Delete(int id);
        double GetMAP(int item_id, int plant_id);
        GOODS_RECEIPT_VM GetDetails(int id);
        string Delete(int id, string cancellation_remarks, int reason_id);

    }
}
