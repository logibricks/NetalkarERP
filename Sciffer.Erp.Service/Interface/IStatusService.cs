using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IStatusService:IDisposable
    {
        List<ref_status> GetAll();
        ref_status Get(int id);
        bool Add(ref_status Status);
        bool Update(ref_status Status);
        bool Delete(int id);
    }
}
