using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Implementation
{
    public class IncetiveHolidayService: IIncetiveHolidayService
    {
        private readonly ScifferContext _scifferContext;

        public IncetiveHolidayService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public ref_mfg_incentive_holiday Add(ref_mfg_incentive_holiday date)
        {
            try
            {
                ref_mfg_incentive_holiday ext = _scifferContext.ref_mfg_incentive_holiday.Where(x => x.holiday_date == date.holiday_date).FirstOrDefault();
                if(ext == null)
                {
                    ref_mfg_incentive_holiday rc = new ref_mfg_incentive_holiday();
                    rc.holiday_date = date.holiday_date;
                    rc.holiday_desc = date.holiday_desc;
                    _scifferContext.ref_mfg_incentive_holiday.Add(rc);
                    _scifferContext.SaveChanges();
                    return rc;
                }
                else
                {
                    ext.holiday_desc = date.holiday_desc;
                    _scifferContext.Entry(ext).State = EntityState.Modified;
                    _scifferContext.SaveChanges();
                    return ext;
                }
            }
            catch (Exception e)
            {
                return date;
            }

        }

        public bool Delete(DateTime date)
        {
            try
            {
                var tag = _scifferContext.ref_mfg_incentive_holiday.FirstOrDefault(c => c.holiday_date == date);
                _scifferContext.Entry(tag).State = EntityState.Deleted;
                _scifferContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public ref_mfg_incentive_holiday Get(DateTime date)
        {
            return _scifferContext.ref_mfg_incentive_holiday.Where(x => x.holiday_date == date).FirstOrDefault();
        }

        public List<ref_mfg_incentive_holiday> GetAll()
        {
            return _scifferContext.ref_mfg_incentive_holiday.ToList();
        }

    }
}
