using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IParameterListService
    {
        List<ref_parameter_list_VM> GetAll();
        ref_parameter_list Get(int id);
        ref_parameter_list Add(ref_parameter_list finance);
        ref_parameter_list Update(ref_parameter_list finance);
        bool Delete(int id);
        void Dispose();
        List<ref_parameter_list_VM> GetUnBlockedParameterList();
    }
}
