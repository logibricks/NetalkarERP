using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IRejectionReceiptService
    {
        List<rejection_receipt_VM> GetAll();
        rejection_receipt_VM Get(int id);
        string Add(rejection_receipt_VM plant);
        bool Delete(int id);
        rejection_receipt_VM GetDetails(int id);
        List<ProductionOrderReceiptVM> GetRejectionItems(string id);
    }
}
