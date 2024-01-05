using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IUserRoleMappingService : IDisposable
    {
        List<ref_user_role_mapping_VM> GetAll();
        List<ref_user_role_mapping_VM> getall();
        ref_user_role_mapping_VM Get(int id);

        bool Add(ref_user_role_mapping_VM vm);
        bool Update(ref_user_role_mapping_VM vm);
        bool Delete(int id);
    }
}
