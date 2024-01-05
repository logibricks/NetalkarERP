using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class CycleTimeService : ICycleTimeService
    {
        private readonly ScifferContext _scifferContext;
        public CycleTimeService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public bool AddExcel(List<cycle_time_excel> cycle_time_excel)
        {
            try
            {
                foreach (var data in cycle_time_excel)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("cycle_time_id", typeof(int));
                    dt.Columns.Add("item_id", typeof(int));
                    dt.Columns.Add("operation_id", typeof(int));
                    dt.Columns.Add("machine_id", typeof(int));
                    dt.Columns.Add("cycle_time", typeof(int));
                    dt.Columns.Add("other", typeof(int));
                    dt.Columns.Add("loading_unloading", typeof(int));
                    dt.Columns.Add("total_cycle_time", typeof(int));
                    dt.Columns.Add("effective_date", typeof(DateTime));
                    dt.Columns.Add("incentive_rate", typeof(decimal));
                    dt.Columns.Add("is_active", typeof(bool));
                    foreach (var items in cycle_time_excel)
                    {
                        if (data.item_id != 0)
                        {
                            dt.Rows.Add(items.cycle_time_id == 0 ? -1 : items.cycle_time_id, items.item_id, items.operation_id,
                                items.machine_id, items.cycle_time, items.other, items.loading_unloading, items.other + items.cycle_time + items.loading_unloading, items.effective_date, items.incentive_rate, items.is_active);
                        }
                    }
                    var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                    t1.TypeName = "dbo.temp_Cycle_time_detail";
                    t1.Value = dt;
                    int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                    var cycle_time_id = new SqlParameter("@cycle_time_id", cycle_time_excel[0].cycle_time_id == 0 ? -1 : cycle_time_excel[0].cycle_time_id);
                    var user = new SqlParameter("@create_user", create_user);

                    var val = _scifferContext.Database.SqlQuery<string>("exec Save_Cycle_time @cycle_time_id, @create_user,@t1", cycle_time_id, user, t1).FirstOrDefault();

                    if (val == "Saved")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }


        public List<cycle_time_excel> GetAll()
        {
            var query = (from cd in _scifferContext.ref_cycle_time.Where(x=> x.is_active)
                         join d in _scifferContext.REF_ITEM on cd.item_id equals d.ITEM_ID
                         join c in _scifferContext.ref_machine on cd.machine_id equals c.machine_id
                         join e in _scifferContext.ref_mfg_process on cd.process_id equals e.process_id
                         select new cycle_time_excel
                         {
                             cycle_time_id = cd.cycle_time_id,
                             machine_name = c.machine_name,
                             item_name = d.ITEM_NAME,
                             process_name = e.process_description,
                             other = cd.others,
                             cycle_time = cd.cycle_time,
                             total_cycle_time = cd.total,
                             loading_unloading = cd.loading_unloading,
                             effective_date = cd.effective_date,
                             incentive_rate = cd.incentive_rate
                         }).OrderByDescending(x=> x.cycle_time_id).ToList();
            return query;
        }

        public cycle_time_excel Get(int id)
        {
            var query = (from cd in _scifferContext.ref_cycle_time.Where(x => x.cycle_time_id == id && x.is_active)
                         join d in _scifferContext.REF_ITEM on cd.item_id equals d.ITEM_ID
                         join c in _scifferContext.ref_machine on cd.machine_id equals c.machine_id
                         join e in _scifferContext.ref_mfg_process on cd.process_id equals e.process_id
                         select new cycle_time_excel
                         {
                             cycle_time_id = cd.cycle_time_id,
                             machine_name = c.machine_name,
                             machine_id = cd.machine_id,
                             item_id = cd.item_id,
                             operation_id = cd.process_id,
                             item_name = d.ITEM_NAME,
                             process_name = e.process_description,
                             other = cd.others,
                             cycle_time = cd.cycle_time,
                             total_cycle_time = cd.total,
                             loading_unloading = cd.loading_unloading,
                             effective_date = cd.effective_date,
                             incentive_rate = cd.incentive_rate
                         }).FirstOrDefault();
            return query;
        }

        public bool BlockCycleTime(int cyclet_time_id)
        {

            var cycle_time = _scifferContext.ref_cycle_time.FirstOrDefault(x => x.cycle_time_id == cyclet_time_id);

            cycle_time.is_active = false;

            return _scifferContext.SaveChanges() == 1 ? true : false;
        }
    }
}
