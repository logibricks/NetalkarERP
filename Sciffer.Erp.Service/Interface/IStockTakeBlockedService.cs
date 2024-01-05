using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IStockTakeBlockedService : IDisposable
    {
        List<stock_take_blocked_vm> GetAll();
        stock_take_blocked Get(int id);
        stock_take_blocked_vm Add(stock_take_blocked_vm country);
        stock_take_blocked_vm Update(stock_take_blocked_vm country);       
    }
}
