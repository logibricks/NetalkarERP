using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Implementation
{
    public class OperatorIncentiveService : IOperatorIncentiveService
    {

        private readonly ScifferContext _scifferContext;

        public OperatorIncentiveService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public string UploadEasyHrData(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    var t1 = new SqlParameter("@dt", SqlDbType.Structured);
                    t1.TypeName = "dbo.temp_easy_hr_data";
                    t1.Value = dt;
                    var val = _scifferContext.Database.SqlQuery<string>(
                        "exec save_easy_hr_data @dt ", t1).FirstOrDefault();
                    return val;
                }

                return "null";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UpdateRecords(int start_date_shift_id, int end_date_shift_id, DateTime from_date, DateTime to_date, string plant_id, DataTable IDT)
        {
            try
            {

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_mfg_operator_incentive_detail";
                t1.Value = IDT;

                var start_date_shiftid = new SqlParameter("@start_date_shift_id", start_date_shift_id);
                var end_date_shiftid = new SqlParameter("@end_date_shift_id", end_date_shift_id);
                var fromDate = new SqlParameter("@from_date", from_date);
                var toDate = new SqlParameter("@to_date", to_date);
                var plant = new SqlParameter("@plant_id", plant_id == null ? "" : plant_id);

                var val = _scifferContext.Database.SqlQuery<string>(
                   "exec save_operator_incentive @start_date_shift_id,@end_date_shift_id,@from_date,@to_date,@plant_id,@t1 ", start_date_shiftid, end_date_shiftid, fromDate, toDate, plant, t1).FirstOrDefault();
                if (val.Contains("Saved"))
                {
                    return val;
                }
                else
                {
                    return val;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public string UpdateStatus(string status, int start_date_shift_id, int end_date_shift_id, DateTime from_date, DateTime to_date, string plant_id)
        {
            try
            {


                var status_name = new SqlParameter("@status", status);
                var start_date_shiftid = new SqlParameter("@start_date_shift_id", start_date_shift_id);
                var end_date_shiftid = new SqlParameter("@end_date_shift_id", end_date_shift_id);
                var fromDate = new SqlParameter("@from_date", from_date);
                var toDate = new SqlParameter("@to_date", to_date);
                var plant = new SqlParameter("@plant_id", plant_id == null ? "" : plant_id);

                var val = _scifferContext.Database.SqlQuery<string>(
                   "exec save_operator_incentive_status @status,@start_date_shift_id,@end_date_shift_id,@from_date,@to_date,@plant_id ", status_name, start_date_shiftid, end_date_shiftid, fromDate, toDate, plant).FirstOrDefault();
                if (val.Contains("Saved"))
                {
                    return val;
                }
                else
                {
                    return val;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //        var list = _scifferContext.mfg_operator_incentive.ToList();

        //        if (list.Count != 0)
        //        {
        //            foreach (var item in list)
        //            {
        //                _scifferContext.Entry(item).State = EntityState.Deleted;
        //            }
        //        }

        //        for (var i = 0; i < vm.shift_ids.Count; i++)
        //        {
        //            {
        //                mfg_operator_incentive rmc = new mfg_operator_incentive();
        //                rmc.date = Convert.ToDateTime(vm.dates[i]);
        //                rmc.shift_id = Convert.ToInt32(vm.shift_ids[i]);
        //                rmc.user_id = Convert.ToInt32(vm.user_ids[i]);
        //                rmc.is_incentive_appl = Convert.ToBoolean(vm.incentive_app[i]);
        //                rmc.incentive_amt = Convert.Todouble(vm.inc_amt[i]);

        //                _scifferContext.mfg_operator_incentive.Add(rmc);

        //            }
        //        }
        //        _scifferContext.SaveChanges();
        //    }
        //    catch (Exception ex)

        //    {
        //        return false;

        //    }
        //    return true;
        //}



    }
}