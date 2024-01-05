using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IIncetiveHolidayService
    {
        List<ref_mfg_incentive_holiday> GetAll();
        ref_mfg_incentive_holiday Get(DateTime id);
        ref_mfg_incentive_holiday Add(ref_mfg_incentive_holiday date);
        bool Delete(DateTime id);
    }
}
