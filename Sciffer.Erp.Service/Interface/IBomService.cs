using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IBomService: IDisposable
    {
        List<ref_mfg_bom_VM> getall();
        string Add(ref_mfg_bom_VM bomvm);
        ref_mfg_bom_VM Get(int id);
        bool Delete(int id);
        bool Update(ref_mfg_bom_VM id); 
    }
}
