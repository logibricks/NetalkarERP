using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface ICycleTimeService
    {
        bool AddExcel(List<cycle_time_excel> cycle_time_excel);
        List<cycle_time_excel> GetAll();
        cycle_time_excel Get(int id);

        bool BlockCycleTime(int cyclet_time_id);
    }
}
