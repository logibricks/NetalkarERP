
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Sciffer.Erp.Service.Interface
{
    public interface IMachineMasterService:IDisposable
    {
        List<ref_machine_master_VM> GetAll();
        List<ref_machine_master_VM> getall();
        ref_machine_master_VM Get(int id);
        bool Add(ref_machine_master_VM ref_machine_master_VM);
        bool Update(ref_machine_master_VM ref_machine_master_VM);
        bool Delete(int id);
    }
}
