using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IInjobWorkService
    {
        List<in_jobwork_in_VM> getall();
        string Add(in_jobwork_in_VM JobWorkIn);
        in_jobwork_in_VM Get(int id);
        bool Delete(int id);
        string DuplicateChalanNo(string no);
        string Update(in_jobwork_in_VM JobWorkIn);
        List<in_jobwork_in_VM> getcustomerlistplantwise(int id);
        string Delete(int id, string cancellation_remarks, int cancellation_reason_id);
    }
}
