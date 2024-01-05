using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IShiftService:IDisposable
    {
        List<shift> GetShiftList();
        List<ref_shifts> GetAll();
        ref_shifts Get(int? id);
        shift Add(shift Shift);
        shift Update(shift Shift);
        bool Delete(int? id);
    }
}
