using AutoMapper;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Linq;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class IncentiveBenchmarkService : IIncentiveBenchmarkService
    {

        private readonly ScifferContext _scifferContext;

        public IncentiveBenchmarkService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;

        }


        public ref_mfg_incentive_benchmark_vm Add(ref_mfg_incentive_benchmark_vm incben)
        {
            try
            {

                if (incben.mfg_incentive_benchmark_id == 0)
                {
                    ref_mfg_incentive_benchmark er = new ref_mfg_incentive_benchmark();
                    er.item_id = incben.item_id;
                    er.plant_id = incben.plant_id;
                    er.machine_id = incben.machine_id;
                    er.operation_id = incben.operation_id;
                    er.reporting_quantity = incben.reporting_quantity;
                    er.incentive = incben.incentive;
                    er.created_by = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                    er.created_ts = DateTime.Now;
                    er.is_active = true;
                    _scifferContext.ref_mfg_incentive_benchmark.Add(er);
                    _scifferContext.SaveChanges();
                    incben.mfg_incentive_benchmark_id = _scifferContext.ref_mfg_incentive_benchmark.Max(x => x.mfg_incentive_benchmark_id);
                }

                else
                {
                    ref_mfg_incentive_benchmark er = _scifferContext.ref_mfg_incentive_benchmark.Where(a => a.mfg_incentive_benchmark_id == incben.mfg_incentive_benchmark_id).FirstOrDefault();
                    er.item_id = incben.item_id;
                    er.plant_id = incben.plant_id;
                    er.machine_id = incben.machine_id;
                    er.operation_id = incben.operation_id;
                    er.reporting_quantity = incben.reporting_quantity;
                    er.modified_by = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                    er.modified_ts = DateTime.Now;
                    er.incentive = incben.incentive;
                    _scifferContext.Entry(er).State = System.Data.Entity.EntityState.Modified;
                    _scifferContext.SaveChanges();
                }


            }
            catch (Exception ex)
            {
                return incben;
            }
            return incben;
        }

        public ref_mfg_incentive_benchmark_vm Get(int id)
        {
            ref_mfg_incentive_benchmark GI = _scifferContext.ref_mfg_incentive_benchmark.FirstOrDefault(c => c.mfg_incentive_benchmark_id == id);
            Mapper.CreateMap<ref_mfg_incentive_benchmark, ref_mfg_incentive_benchmark_vm>();
            ref_mfg_incentive_benchmark_vm GIV = Mapper.Map<ref_mfg_incentive_benchmark, ref_mfg_incentive_benchmark_vm>(GI);

            return GIV;
        }

        public bool Delete(int id)
        {
            try
            {
                _scifferContext.Database.ExecuteSqlCommand("update [dbo].[ref_mfg_incentive_benchmark] set [is_active] = 0 where mfg_incentive_benchmark_id = " + id);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
