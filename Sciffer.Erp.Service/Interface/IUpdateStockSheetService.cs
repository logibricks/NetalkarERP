using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IUpdateStockSheetService
    {
        List<update_stock_count_vm> getall();
        string Add(update_stock_count_vm vm);
        update_stock_count_vm Get(int? id);
    }
}
