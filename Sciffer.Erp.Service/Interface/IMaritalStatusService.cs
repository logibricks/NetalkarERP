using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IMaritalStatusService:IDisposable
    {
        List<REF_MARITAL_STATUS> GetAll();
        REF_MARITAL_STATUS Get(int? id);
        bool Add(REF_MARITAL_STATUS MSTATUS);
        bool Update(REF_MARITAL_STATUS MSTATUS);
        bool Delete(int? id);
    }
}
