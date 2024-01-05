using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Interface
{
    public interface IProductionReceiptService : IDisposable
    {
        List<prod_receipt_VM> GetAll();
        prod_receipt_VM Get(int id);
        string Add(prod_receipt_VM plant);
        // bool Update(prod_receipt_VM plant);
        bool Delete(int id);
        List<ProductionOrderReceiptVM> GetProductionOrderItems(string id);
        prod_receipt_VM GetDetails(int id);
    }
}
