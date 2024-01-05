using AutoMapper;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class SkillMatrixService : ISkillMatrixService
    {
        private readonly ScifferContext _scifferContext;
        public SkillMatrixService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool AddLevel(ref_skill_matrix_vm vm)
        {
            try
            {
                int user;
                user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());


                if (vm.level_id == 0)
                {
                    ref_level bt = new ref_level();
                    bt.is_active = true;
                    bt.level_code = vm.level_code;
                    bt.level_desc = vm.level_desc;
                    bt.color_code = vm.color_code;
                    bt.percentage = vm.percentage;
                    bt.created_by = user;
                    bt.created_ts = DateTime.Now;
                    _scifferContext.Entry(bt).State = System.Data.Entity.EntityState.Added;
                    _scifferContext.SaveChanges();
                }
                else
                {
                    ref_level bt = _scifferContext.ref_level.Where(a => a.level_id == vm.level_id).FirstOrDefault();
                    bt.is_active = vm.is_active1 == false ? true : false;
                    bt.level_code = vm.level_code;
                    bt.level_desc = vm.level_desc;
                    bt.color_code = vm.color_code;
                    bt.percentage = vm.percentage;
                    bt.created_by = user;
                    bt.created_ts = DateTime.Now;
                    _scifferContext.Entry(bt).State = EntityState.Modified;
                    _scifferContext.SaveChanges();
                }



            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        public bool OperatorLevel(List<ref_skill_matrix_vm> vm)
        {
            try
            {
                int user;
                user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                int mapId = 0;

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("operator_level_mapping_id", typeof(int));
                dt1.Columns.Add("MappingId", typeof(int));
                dt1.Columns.Add("supervisor_id", typeof(int));
                dt1.Columns.Add("level_id1", typeof(int));
                dt1.Columns.Add("machine_id", typeof(int));
                dt1.Columns.Add("effective_from", typeof(DateTime));
                dt1.Columns.Add("effective_to", typeof(DateTime));
                dt1.Columns.Add("created_by", typeof(int));
                dt1.Columns.Add("machine_id_list", typeof(string));
                dt1.Columns.Add("is_block", typeof(bool));

                if (vm != null)
                {
                    if (vm.Count != 0)
                    {
                        foreach (var skillMatrix in vm)
                        {
                            dt1.Rows.Add(skillMatrix.operator_level_mapping_id == 0 ? 0 : skillMatrix.operator_level_mapping_id,
                                skillMatrix.MappingId == 0 ? 0 : skillMatrix.MappingId,
                                skillMatrix.supervisor_id == 0 ? 0 : skillMatrix.supervisor_id,
                                skillMatrix.level_id1 == 0 ? 0 : skillMatrix.level_id1,
                                skillMatrix.machine_id == 0 ? 0 : skillMatrix.machine_id,
                                skillMatrix.fromDate,
                                skillMatrix.toDate,
                                user,
                                skillMatrix.machine_id_list == null ? "" : skillMatrix.machine_id_list,
                                skillMatrix.is_active2
                                );
                            mapId = skillMatrix.MappingId;
                        }
                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_ref_skill_matrix_details";
                t1.Value = dt1;

                var id1 = new SqlParameter("@mapping_id", mapId);
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec insert_skill_matrix @mapping_id,@t1", id1, t1).FirstOrDefault();

                if (val == "Saved")
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool MachineLevel(ref_skill_matrix_vm vm)
        {
            try
            {
                int user;
                user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());


                if (vm.machine_level_mapping_id == 0)
                {
                    ref_machine_level_mapping bt = new ref_machine_level_mapping();
                    bt.is_active = true;
                    bt.machine_id = vm.machine_id2;
                    bt.level_id = vm.level_id2;
                    bt.effective_from = vm.fromDate2;
                    bt.effective_to = vm.toDate2;
                    bt.created_by = user;
                    bt.created_ts = DateTime.Now;
                    _scifferContext.Entry(bt).State = System.Data.Entity.EntityState.Added;
                    _scifferContext.SaveChanges();
                }
                else
                {
                    ref_machine_level_mapping exits = _scifferContext.ref_machine_level_mapping.Where(a => a.machine_id == vm.machine_id2 && a.level_id == vm.level_id2 && a.is_active).FirstOrDefault();
                    if (exits == null)
                    {
                        ref_machine_level_mapping old = _scifferContext.ref_machine_level_mapping.Where(a => a.machine_level_mapping_id == vm.machine_level_mapping_id).FirstOrDefault();
                        old.is_active = false;
                        old.modified_by = user;
                        old.modified_ts = DateTime.UtcNow;
                        _scifferContext.Entry(old).State = System.Data.Entity.EntityState.Modified;
                        _scifferContext.SaveChanges();
                        ref_machine_level_mapping bt = new ref_machine_level_mapping();
                        bt.is_active = true;
                        bt.machine_id = vm.machine_id2;
                        bt.level_id = vm.level_id2;
                        bt.effective_from = vm.fromDate2;
                        bt.effective_to = vm.toDate2;
                        bt.created_by = user;
                        bt.created_ts = DateTime.UtcNow;
                        _scifferContext.Entry(bt).State = System.Data.Entity.EntityState.Added;
                        _scifferContext.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public List<ref_skill_matrix_vm> GetAll(string entity, int? id)
        {
            try
            {
                var entity1 = new SqlParameter("@entity", entity);
                var id1 = new SqlParameter("@id", id);
                var val = _scifferContext.Database.SqlQuery<ref_skill_matrix_vm>(
                "exec get_all_data_skill_matrix @entity,@id",
                entity1, id1).ToList();
                return val;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
        public List<ref_machine_master_VM> GetMachineListWithOperationAndUser1(int operator_level_mapping_id)
        {
            List<ref_machine_master_VM> result = null;
            try
            {
                //result = (from mmpm in _scifferContext.map_mfg_process_machine
                //          join moo in _scifferContext.map_operation_operator on mmpm.process_id equals moo.operation_id
                //          join um in _scifferContext.ref_user_management.Where(x => x.user_id == userId) on moo.operator_id equals um.user_id
                //          join rm in _scifferContext.ref_machine on mmpm.machine_id equals rm.machine_id
                //          join rm1 in _scifferContext.ref_operator_level_mapping.Where(x => x.level_id == level_id && x.is_active) on mmpm.machine_id equals rm1.machine_id
                //          select new ref_machine_master_VM
                //          {
                //              machine_id = rm.machine_id,
                //              machine_name = rm.machine_name

                //          }).ToList();

                // var machineId = _scifferContext.ref_operator_level_mapping.FirstOrDefault(x => x.is_active && x.level_id == level_id && x.operator_id == userId).machine_id;

                //var machine = machineId.Split(',');

                //for (int i = 0; i < machine.Length; i++)
                //{
                //    int machi_d = Convert.ToInt32(machine[i].ToString());
                //    var _result = (from rm in _scifferContext.ref_machine.Where(x => x.machine_id == machi_d)
                //                   select new ref_machine
                //                   {
                //                       machine_id = rm.machine_id,
                //                       machine_name = rm.machine_name
                //                   }).FirstOrDefault();
                //    result.Add(_result);
                //}

                //try
                //{
                //    var entity1 = new SqlParameter("@entity", "get_machine_list");
                //    var id1 = new SqlParameter("@ids", machineId);
                //    var val = _scifferContext.Database.SqlQuery<ref_machine_master_VM>(
                //    "exec get_skill_matrix @entity,@ids",
                //    entity1, id1).ToList();
                //    return val;
                //}
                //catch (Exception ex)
                //{
                //    return null;
                //    throw;
                //}

                result = (from map in _scifferContext.ref_operator_level_mapping.Where(x => x.is_active && x.operator_level_mapping_id == operator_level_mapping_id)
                          join rm in _scifferContext.ref_machine on map.machine_id equals rm.machine_id
                          select new ref_machine_master_VM
                          {
                              machine_id = rm.machine_id,
                              machine_name = rm.machine_name
                          }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
            return result;
        }

        public List<ref_skill_matrix_vm> GetHistory(int userId, bool isActive)
        {
            var result = new List<ref_skill_matrix_vm>();

            try
            {
                var mapId = new SqlParameter("@mappingId", userId);
                var active = new SqlParameter("@isActive", isActive);
                var uid = new SqlParameter("@userId", userId);
                result = _scifferContext.Database.SqlQuery<ref_skill_matrix_vm>(
                "exec get_skill_matrix_History @mappingId,@isActive,@userId",
                mapId, active, uid).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ref_user_management_VM> GetUser(int userId)
        {
            var result = new List<ref_user_management_VM>();

            try
            {
                if (userId != 0)
                {
                    result = (from map in _scifferContext.ref_operator_level_mapping.Where(x => x.operator_id == userId)
                              join user in _scifferContext.ref_user_management.Where(x => !x.is_block) on map.operator_id equals user.user_id
                              select new ref_user_management_VM
                              {
                                  user_id = map.operator_id,
                                  user_code = user.user_code,
                                  user_name = user.user_name,
                                  notes = user.notes,
                                  employeephoto = user.REF_EMPLOYEE.employeephoto == "No File" ? "~/Files/EMPLOYEEIMAGE/noemployee.png" : user.REF_EMPLOYEE.employeephoto

                              }).Take(1).ToList();

                    //var user_Id = result[0].user_id;
                    //var photo = _scifferContext.ref_user_management.Where(x => x.user_id == 236).FirstOrDefault().REF_EMPLOYEE.employeephoto;
                    //var photo = _scifferContext.REF_EMPLOYEE.Where(x => x.employee_id == 236).FirstOrDefault().employeephoto;
                    //result[0].employee_name = photo;
                    //if (photo != null || photo != "")
                    //{
                    //    var webClient = new WebClient();
                    //    byte[] imageBytes = webClient.DownloadData("https://d2cblobms.blob.core.windows.net/d2clegacy/91157f79-b297-44ba-a0f1-205f9b6415e4.png");
                    //    result[0].employeePhoto = imageBytes;
                    //}
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return result;
        }

        public List<ref_level_vm> GetLevels()
        {
            var result = new List<ref_level_vm>();

            try
            {
                result = (from map in _scifferContext.ref_level.Where(x => x.is_active)
                          select new ref_level_vm
                          {
                              level_desc = map.level_desc,
                              level_code = map.level_code,
                          }).Take(5).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
            return result;
        }

        public ref_temp_operator_level_mapping Add_temp_operator_level_mapping(ref_temp_operator_level_mapping value)
        {
            try
            {
                int user;
                user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

                if (value.temp_operator_level_mapping_id == 0)
                {

                    value.is_active = true;
                    value.created_by = user;
                    value.created_ts = DateTime.Now;
                    //value.effective_date = DateTime.Now;
                    //value.modified_ts = DateTime.Now;
                    value.is_blocked = false;

                    _scifferContext.ref_temp_operator_level_mapping.Add(value);
                    _scifferContext.SaveChanges();
                    value.temp_operator_level_mapping_id = _scifferContext.ref_temp_operator_level_mapping.Max(x => x.temp_operator_level_mapping_id);
                }
                else
                {
                    var temp_operator = _scifferContext.ref_temp_operator_level_mapping.FirstOrDefault(x => x.temp_operator_level_mapping_id == value.temp_operator_level_mapping_id);
                    temp_operator.is_blocked = value.is_blocked;
                    temp_operator.modified_by = user;
                    temp_operator.modified_ts = DateTime.UtcNow;
                    _scifferContext.Entry(temp_operator).State = System.Data.Entity.EntityState.Modified;
                    _scifferContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                return value; ;
            }
            return value;
        }
        public List<temporary_skill_matrix_access_vm> GetAll1(string entity, int? id)
        {
            try
            {
                var entity1 = new SqlParameter("@entity", entity);
                var id1 = new SqlParameter("@id", id);
                var val = _scifferContext.Database.SqlQuery<temporary_skill_matrix_access_vm>(
                "exec get_all_data_skill_matrix @entity,@id",
                entity1, id1).ToList();
                return val;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public temporary_skill_matrix_access_vm GetByIdTemporarySkillMatrix(int id)
        {
            try
            {
                var entity1 = new SqlParameter("@entity", "get_temp_operator_level_index");
                var id1 = new SqlParameter("@id", id);
                var val = _scifferContext.Database.SqlQuery<ref_temp_operator_level_mapping>(
                "exec get_all_data_skill_matrix @entity,@id",
                entity1, id1).FirstOrDefault();
                Mapper.CreateMap<ref_temp_operator_level_mapping, temporary_skill_matrix_access_vm>();
                temporary_skill_matrix_access_vm mmv = Mapper.Map<ref_temp_operator_level_mapping, temporary_skill_matrix_access_vm>(val);

                return mmv;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
    }
}
