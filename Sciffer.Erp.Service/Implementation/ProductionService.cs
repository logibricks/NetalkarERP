using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using AutoMapper;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class ProductionService : IProductionService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name

        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _Generic;
        public ProductionService(ScifferContext ScifferContext, IGenericService generic)
        {
            _scifferContext = ScifferContext;
            _Generic = generic;
        }
        public List<mfg_prod_order_VM> GetActivePOListForIssue()
        {
            List<mfg_prod_order_VM> list = new List<mfg_prod_order_VM>();
            try
            {
                list = (from po in _scifferContext.mfg_prod_order
                        join pod in _scifferContext.mfg_prod_order_detail on po.prod_order_id equals pod.prod_order_id
                        where pod.bal_quantity > 0 && po.order_status == true
                        select new mfg_prod_order_VM
                        {
                            prod_order_id = po.prod_order_id,
                            prod_order_date = po.prod_order_date,
                            prod_order_no = po.prod_order_no + " - " + pod.REF_ITEM.ITEM_NAME + " - " + pod.bal_quantity + " (Qty)",
                        }).ToList();
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public List<mfg_prod_order_VM> GetActivePOListForReceipt()
        {
            List<mfg_prod_order_VM> list = new List<mfg_prod_order_VM>();
            try
            {
                list = (from po in _scifferContext.mfg_prod_order.Where(x => x.balance_qty > 0)
                        select new mfg_prod_order_VM
                        {
                            prod_order_id = po.prod_order_id,
                            prod_order_date = po.prod_order_date,
                            prod_order_no = po.prod_order_no + " - " + po.REF_ITEM.ITEM_NAME + " - " + po.balance_qty + " (Qty)",
                        }).ToList();
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        public List<mfg_process_seq_alt> Getprocess_seq(int item)
        {
            var data = new List<mfg_process_seq_alt>();
            try
            {
                data = _scifferContext.mfg_process_seq_alt.Where(x => x.item_id == item).ToList();
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public List<ref_mfg_bom> GetBOMForItem(int out_item_id)
        {
            var list = new List<ref_mfg_bom>();
            try
            {
                list = _scifferContext.ref_mfg_bom.Where(x => x.out_item_id == out_item_id).ToList();
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        public List<ref_mfg_bom> GetAllBom()
        {
            var list = new List<ref_mfg_bom>();
            try
            {
                list = _scifferContext.ref_mfg_bom.ToList();
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        public List<mfg_prod_order_VM> GetTagNumbers(string machine, int prod_order_id)
        {
            var prod_order_detail_id = _scifferContext.mfg_prod_order_detail.Where(x => x.prod_order_id == prod_order_id).FirstOrDefault().prod_order_detail_id;
            var list = new List<mfg_prod_order_VM>();
            try
            {
                list = (from mmt in _scifferContext.mfg_machine_task
                        join tag in _scifferContext.inv_item_batch_detail_tag on mmt.tag_id equals tag.tag_id
                        where mmt.machine_id == machine && mmt.prod_order_detail_id == prod_order_detail_id && mmt.machine_task_status_id == 1
                        select new
                        {
                            machine_id = mmt.machine_id,
                            tag_id = mmt.tag_id,
                            tag_no = tag.tag_no,
                            status_id = mmt.machine_task_status_id,

                        }).ToList()
                        .Select(x => new mfg_prod_order_VM
                        {
                            machine_id = x.machine_id,
                            tag_no = x.tag_no,
                            status_id = x.status_id,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
            return list;
        }

        public string UpdateTagNumbers(string source, string destination, int prod_order_id)
        {
            try
            {
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var prod_order_detail_id = _scifferContext.mfg_prod_order_detail.Where(x => x.prod_order_id == prod_order_id).FirstOrDefault().prod_order_detail_id;
                var tag_no = _scifferContext.mfg_machine_task.Where(x => x.prod_order_detail_id == prod_order_detail_id && x.machine_id == source && x.machine_task_status_id == 1).ToList();
                mfg_prod_order vm = _scifferContext.mfg_prod_order.Where(x => x.prod_order_id == prod_order_id).FirstOrDefault();
                vm.modified_by = user;
                vm.modified_ts = DateTime.Today;
              
                if (tag_no.Count != 0)
                {
                    foreach (var dt in tag_no)
                    {
                        // mfg_machine_task mmt = _scifferContext.mfg_machine_task.Where(x => x.machine_task_id == dt.machine_task_id).FirstOrDefault();
                        //{
                        dt.machine_id = destination;
                        //};
                        _scifferContext.Entry(dt).State = System.Data.Entity.EntityState.Modified;
                    }
                    _scifferContext.Entry(vm).State = EntityState.Modified;
                    _scifferContext.SaveChanges();
                    return "Saved";
                }
                else
                {
                    return "notfound";
                }

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public List<mfg_prod_order_VM> GetBOMGridData(int bom_id, double itemquantity)
        {
            var list = new List<mfg_prod_order_VM>();
            try
            {
                list = (from bm in _scifferContext.ref_mfg_bom_detail
                        where bm.mfg_bom_id == bom_id
                        join rb in _scifferContext.ref_mfg_bom on bm.mfg_bom_id equals rb.mfg_bom_id
                        select new
                        {
                            mfg_bom_qty = rb.mfg_bom_qty,
                            mfg_bom_detail_id = bm.mfg_bom_detail_id,
                            in_item_group_id = bm.in_item_group_id,
                            in_item_id = bm.in_item_id,
                            in_item_code = bm.REF_ITEM.ITEM_CODE,
                            in_item_desc = bm.REF_ITEM.ITEM_NAME,
                            in_item_qty = bm.in_item_qty,
                            in_uom_id = bm.in_uom_id,
                            in_uom_name = bm.REF_UOM.UOM_NAME,
                        }).ToList()
                        .Select(x => new mfg_prod_order_VM
                        {
                            mfg_bom_detail_id = x.mfg_bom_detail_id,
                            in_item_group_id = x.in_item_group_id,
                            in_item_id = x.in_item_id,
                            in_item_code = x.in_item_code + " - " + x.in_item_desc,
                            in_item_qty = x.in_item_qty,
                            in_uom_id = x.in_uom_id,
                            in_uom_name = x.in_uom_name,
                            parent_bom_qty = x.mfg_bom_qty,
                            calculated_qty = (Math.Round((double)x.in_item_qty * itemquantity)) / x.mfg_bom_qty,
                        }).ToList();
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        public List<mfg_prod_order_VM> GetAll()
        {
            try
            {
                //var ent = new SqlParameter("@entity", "getall");
                var val = _scifferContext.Database.SqlQuery<mfg_prod_order_VM>(
                    "exec get_production_order").ToList();
                return val;

                //List<mfg_prod_order_VM> list = new List<mfg_prod_order_VM>();
                //list = (from po in _scifferContext.mfg_prod_order
                //        join pod in _scifferContext.mfg_prod_order_detail on po.prod_order_id equals pod.prod_order_id
                //        join iib in _scifferContext.inv_item_batch on pod.batch_id equals iib.item_batch_id
                //        join user in _scifferContext.ref_user_management on (int?)po.created_by equals user.user_id
                //        join user1 in _scifferContext.ref_user_management on (int?)po.modified_by equals user1.user_id
                //        into ps
                //        from temp in ps.DefaultIfEmpty()
                //        orderby po.created_ts descending
                //        select new mfg_prod_order_VM
                //        {
                //            prod_order_id = po.prod_order_id,
                //            prod_order_date = po.prod_order_date,
                //            prod_order_no = po.prod_order_no,
                //            process_seq_alt_id = po.process_seq_alt_id,
                //            process_sequence_name = po.mfg_process_seq_alt.seq_name,
                //            machine_seq = po.machine_seq,
                //            out_item_id = po.out_item_id,
                //            out_item_name = po.REF_ITEM.ITEM_CODE + " - " + po.REF_ITEM.ITEM_NAME, // check once
                //            quantity = po.quantity,
                //            plant_id = po.plant_id,
                //            plant_name = po.REF_PLANT.PLANT_NAME,
                //            child_sloc_id = po.child_sloc_id,
                //            child_sloc_name = po.REF_STORAGE_LOCATION.storage_location_name,
                //            parent_sloc_id = po.parent_sloc_id,
                //            parent_sloc_name = po.REF_STORAGE_LOCATION1.storage_location_name,
                //            // created_by = po.created_by,
                //            created_by = user.user_name,
                //            created_ts = po.created_ts,
                //            modified_by = temp.user_name,
                //            modified_ts = po.modified_ts,
                //            in_item_id = pod.in_item_id,
                //            in_item_code = pod.REF_ITEM.ITEM_CODE + " - " + pod.REF_ITEM.ITEM_NAME,
                //            challan_no = iib.batch_number,
                //        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
            //return list;
        }

        public mfg_prod_order_VM Get(int id)
        {
            mfg_prod_order po = _scifferContext.mfg_prod_order.FirstOrDefault(c => c.prod_order_id == id);
            Mapper.CreateMap<mfg_prod_order, mfg_prod_order_VM>();
            mfg_prod_order_VM mmv = Mapper.Map<mfg_prod_order, mfg_prod_order_VM>(po);
            mmv.prod_order_date_str = mmv.prod_order_date.ToString("yyyy-MM-dd");
            mmv.mfg_prod_order_detail = mmv.mfg_prod_order_detail.Where(x => x.prod_order_id == id).ToList();
            mmv.mfg_prod_order_bom = mmv.mfg_prod_order_bom.ToList();
            return mmv;
        }


        public string Add(mfg_prod_order_VM vm)
        {
            try
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("prod_order_detail_id", typeof(int));
                dt1.Columns.Add("item_batch_detail_id", typeof(int));
                dt1.Columns.Add("batch_id", typeof(int));
                dt1.Columns.Add("in_item_id", typeof(int));
                dt1.Columns.Add("quantity", typeof(double));
                dt1.Columns.Add("in_uom_id", typeof(int));
                dt1.Columns.Add("order_status", typeof(bool));
                dt1.Columns.Add("is_active", typeof(bool));
                dt1.Columns.Add("balance_quantity", typeof(double));
                if (vm.batch_in_item_id != null)
                {
                    for (var i = 0; i < vm.batch_in_item_id.Count; i++)
                    {
                        dt1.Rows.Add(vm.prod_order_detail_id[i] == "0" ? -1 : int.Parse(vm.prod_order_detail_id[i]),
                            int.Parse(vm.item_batch_detail_id[i]),
                            int.Parse(vm.item_batch_id[i]),
                            int.Parse(vm.batch_in_item_id[i]),
                            vm.batch_quantity[i] == "" ? 0 : double.Parse(vm.batch_quantity[i]),
                            int.Parse(vm.batch_in_uom_id[i]),
                            true,
                            true,
                            vm.batch_quantity[i] == "" ? 0 : double.Parse(vm.batch_quantity[i]));
                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_prod_order_detail";
                t1.Value = dt1;

                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var prod_order_id = new SqlParameter("@prod_order_id", vm.prod_order_id == 0 ? -1 : vm.prod_order_id);
                var document_date = new SqlParameter("@document_date", vm.prod_order_date);
                var process_sequence_id = new SqlParameter("@process_sequence_id", vm.process_seq_alt_id);
                var machine_seq = new SqlParameter("@machine_seq", vm.machine_seq);
                var out_item_id = new SqlParameter("@out_item_id", vm.out_item_id);
                var quantity = new SqlParameter("@quantity", vm.quantity);
                var plant_id = new SqlParameter("@plant_id", vm.plant_id);
                var created_by = new SqlParameter("@created_by", user);
                var child_sloc_id = new SqlParameter("@child_sloc_id", vm.child_sloc_id);
                var parent_sloc_id = new SqlParameter("@parent_sloc_id", vm.parent_sloc_id);
                var uom_id = new SqlParameter("@uom_id", vm.uom_id);
                var category_id = new SqlParameter("@category_id", vm.category_id);
                var shift_id = new SqlParameter("@shift_id", vm.shift_id);
                var remarks = new SqlParameter("@remarks", vm.remarks == null ? "" : vm.remarks);
                var is_blocked = new SqlParameter("@is_blocked", false);
                var order_status = new SqlParameter("@order_status", true);
                var balance_qty = new SqlParameter("@balance_qty", vm.quantity);

                var mfg_bom_id = new SqlParameter("@mfg_bom_id", vm.mfg_bom_id);

                var val = _scifferContext.Database.SqlQuery<string>("exec Save_ProductionOrder @prod_order_id,@document_date,@process_sequence_id,@machine_seq, @out_item_id, @quantity, @plant_id, @created_by, @child_sloc_id, @parent_sloc_id, @uom_id,@category_id,@shift_id,@remarks,@is_blocked,@order_status,@balance_qty,@mfg_bom_id,@t1",
                    prod_order_id, document_date, process_sequence_id, machine_seq, out_item_id, quantity, plant_id, created_by, child_sloc_id, parent_sloc_id, uom_id, category_id, shift_id, remarks, is_blocked, order_status, balance_qty, mfg_bom_id, t1).FirstOrDefault();
                if (val.Contains("Saved"))
                {
                    var sp = val.Split('~')[1];
                    return sp;
                }
                else
                {
                    return "Error";
                }

            }
            catch (Exception ex)
            {
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
                //return "error";
            }
        }

        public bool Update(mfg_prod_order_VM vm)
        {
            try
            {
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                mfg_prod_order po = _scifferContext.mfg_prod_order.Where(x => x.prod_order_id == vm.prod_order_id).FirstOrDefault();
                po.is_blocked = vm.is_blocked;
                po.modified_by = user;
                po.modified_ts = DateTime.Now;
                _scifferContext.Entry(po).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<ref_machine_master_VM> GetMachineList()
        {
            var query = (from i in _scifferContext.ref_machine.Where(x => x.is_blocked == false && x.is_active == true)
                         select new ref_machine_master_VM
                         {
                             machine_id = i.machine_id,
                             machine_code = i.machine_code + "/" + i.machine_name,
                         }).ToList();
            return query;
        }




        public List<string> GetProcessSequence(int id)
        {
            var machine_ids = _scifferContext.mfg_prod_order.Where(x => x.prod_order_id == id).Select(x => x.machine_seq).FirstOrDefault().Split(',');
            return machine_ids.ToList();
        }

        public List<string> GetProcessSequenceByProcessSequenceId(int process_sequence_id)
        {
            var machine_ids = _scifferContext.mfg_process_seq_alt.Where(x => x.process_seq_alt_id == process_sequence_id).Select(x => x.process_seq_str).FirstOrDefault().Split(',');
            return machine_ids.ToList();
        }

        //Update process sequence by production order id
        public string UpdateProcessSequence(int id, string process_sequence)
        {
            try
            {
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                mfg_prod_order vm = _scifferContext.mfg_prod_order.Where(x => x.prod_order_id == id).FirstOrDefault();
                vm.machine_seq = process_sequence;
                vm.modified_by = user;
                vm.modified_ts = DateTime.Now;
                _scifferContext.Entry(vm).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return "failed";
            }
        }

        //Update process sequence by process sequence id
        public string UpdateProcessSequenceById(int process_sequence_id, string process_sequence)
        {
            try
            {
                mfg_process_seq_alt query = _scifferContext.mfg_process_seq_alt.Where(x => x.process_seq_alt_id == process_sequence_id).FirstOrDefault();
                query.process_seq_str = process_sequence;
                _scifferContext.Entry(query).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return "failed";
            }
        }

        public List<mfg_prod_order_VM> GetProductionOrderList(int item_id, string entity_id)
        {
            //var query = (from po in _scifferContext.mfg_prod_order
            //             join pod in _scifferContext.mfg_prod_order_detail on po.prod_order_id equals pod.prod_order_id
            //             where po.balance_qty > 0 && po.out_item_id == item_id
            //             select new mfg_prod_order_VM
            //             {
            //                 prod_order_id = po.prod_order_id,
            //                 prod_order_date = po.prod_order_date,
            //                 prod_order_no = po.prod_order_no + " - " + pod.inv_item_batch.batch_number + " - " + po.balance_qty + " (Qty)",
            //             }).ToList();
            //return query;

            var itemid = new SqlParameter("@item_id", item_id);
            var entity = new SqlParameter("@entity", entity_id);
            var val = _scifferContext.Database.SqlQuery<mfg_prod_order_VM>(
            "exec GetProductionOrder @item_id,@entity", itemid, entity).ToList();
            return val;
        }


        //---------------------------------------Operation Sequence--------------------------------------------//
        public List<GetOperationSequenceWithMachine> GetOperationsByOperationSequenceId(int process_seq_alt_id)
        {
            var sequence_list = new List<GetOperationSequenceWithMachine>();

            try
            {
                var operation_sequence = _scifferContext.mfg_process_seq_alt.Where(x => x.process_seq_alt_id == process_seq_alt_id).FirstOrDefault().process_seq_str.Split(',');

                foreach (var item in operation_sequence)
                {
                    var process_id_list = item.Split('/').ToList();

                    if (process_id_list.Count > 1)
                    {
                        var sequence_list_list = new List<GetOperationSequenceWithMachineList>();
                        foreach (var list_item in process_id_list)
                        {
                            var process_id = Convert.ToInt32(list_item);
                            var list = (from rmp in _scifferContext.ref_mfg_process
                                        where rmp.process_id == process_id
                                        select new
                                        {
                                            process_id = rmp.process_id,
                                            process_name = rmp.process_code + " - " + rmp.process_description,

                                        }).ToList()
                               .Select(x => new GetOperationSequenceWithMachineList
                               {
                                   process_id = x.process_id,
                                   process_name = x.process_name,
                               }).FirstOrDefault();


                            sequence_list_list.Add(new GetOperationSequenceWithMachineList
                            {
                                process_id = list.process_id,
                                process_name = list.process_name,
                            });
                        }
                        sequence_list.Add(new GetOperationSequenceWithMachine
                        {
                            GetOperationSequenceWithMachineList = sequence_list_list,
                        });
                    }
                    else
                    {
                        var process_id = Convert.ToInt32(item);
                        var list = (from rmp in _scifferContext.ref_mfg_process
                                    where rmp.process_id == process_id
                                    select new
                                    {
                                        process_id = rmp.process_id,
                                        process_name = rmp.process_code + " - " + rmp.process_description,
                                    }).ToList()
                           .Select(x => new GetOperationSequenceWithMachine
                           {
                               process_id = x.process_id,
                               process_name = x.process_name,
                           }).FirstOrDefault();


                        sequence_list.Add(new GetOperationSequenceWithMachine
                        {
                            process_id = list.process_id,
                            process_name = list.process_name,
                        });
                    }

                }
                return sequence_list;
            }
            catch (Exception ex)
            {
                return sequence_list;
            }
        }

        //Get mapped machine listb by process id
        public List<ref_machine> GetMappedMachinesByProcessId(int process_id)
        {
            List<ref_machine> list = new List<ref_machine>();
            try
            {
                list = (from mmpm in _scifferContext.map_mfg_process_machine
                        join rm in _scifferContext.ref_machine on mmpm.machine_id equals rm.machine_id
                        where mmpm.process_id == process_id
                        orderby mmpm.machine_id ascending
                        select new
                        {
                            machine_id = rm.machine_id,
                            machine_name = rm.machine_code + "/" + rm.machine_name
                        }).ToList()
                        .Select(x => new ref_machine
                        {
                            machine_id = x.machine_id,
                            machine_name = x.machine_name
                        }).ToList();

                return list;
            }
            catch (Exception ex)
            {
                return list;
            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }



    }
}
