using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IGenderService:IDisposable
    {
        List<REF_GENDER> GetAll();
        REF_GENDER Get(int id);
        bool Add(REF_GENDER gender);
        bool Update(REF_GENDER gender);
        bool Delete(int id);
    }
}
