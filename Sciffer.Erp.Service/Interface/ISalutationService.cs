using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface ISalutationService:IDisposable
    {
        List<REF_SALUTATION> GetAll();
        REF_SALUTATION Get(int id);
        bool Add(REF_SALUTATION salutation);
        bool Update(REF_SALUTATION salutation);
        bool Delete(int id);
    }
}
