using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IPartyTypeService:IDisposable
    {
        List<ref_party_type> GetAll();
        ref_party_type Get(int? id);
        bool Add(ref_party_type party);
        bool Update(ref_party_type party);
        bool Delete(int? id);
    }
}
