using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IStockSheetService
    {
        List<create_stock_sheet_vm> getall();
        string Add(create_stock_sheet_vm vm);
        create_stock_sheet_vm Get(int? id);
        List<create_stock_sheet_vm> StockQuantity_Graterthan_Zero(int id, string form);
        List<create_stock_sheet_vm> StockQuantity_Lessthan_Zero(int id, string form);
        List<create_stock_sheet_vm> StockQuantity_Equal_Zero(int id, string form);
        List<create_stock_sheet_vm> GetStockSheet();
        List<create_stock_sheet_vm> create_stock_sheet(int id);
    }
}
