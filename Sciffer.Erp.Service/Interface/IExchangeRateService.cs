using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IExchangeRateService:IDisposable
    {
        List<ref_exchangerate_vm> GetExchanagelist();
        List<ref_exchange_rate> GetAll();
        ref_exchange_rate Get(int? id);
        ref_exchangerate_vm Add(ref_exchangerate_vm exchange);
        ref_exchangerate_vm Update(ref_exchangerate_vm exchange);
        bool Delete(int? id);
    }
}
