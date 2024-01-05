using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using Sciffer.Erp.Domain.ViewModel;
using System.Web;
using AutoMapper;

namespace Sciffer.Erp.Service.Implementation
{
    public class FinancialTemplateService : IFinancialTemplateService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _Generic;
        public FinancialTemplateService(ScifferContext scifferContext, IGenericService gen)
        {
            _scifferContext = scifferContext;
            _Generic = gen;
        }

        public bool Add(ref_fin_template_vm template)
        {
            try
            {               
                DataTable dt = new DataTable();
                dt.Columns.Add("template_detail_id", typeof(int));
                dt.Columns.Add("bs_pl_side", typeof(int));
                dt.Columns.Add("group_id", typeof(string));
                dt.Columns.Add("group_no", typeof(int));
                dt.Columns.Add("group_name", typeof(string));
                dt.Columns.Add("parent_id", typeof(string));
                dt.Columns.Add("group_level", typeof(int));
                dt.Columns.Add("main_heading", typeof(bool));
                dt.Columns.Add("bs_pl", typeof(int));
                if(template.group_name1 != null)
                {
                    for (var i = 0; i < template.group_name1.Count; i++)
                    {
                        var template_detail_id1 = template.template_detail_id1 == null ? -1 : template.template_detail_id1[i];
                        var bs_pl_side1 = template.bs_pl_side1 == null ? 0 : template.bs_pl_side1[i];
                        var group_no1 = template.group_no;
                        var group_name1 = template.group_name1[i];
                        var parent_id1 = template.parent_id1[i];
                        var group_level1 = template.group_level;
                        var main_heading1 = template.main_heading1[i]==0 ? false : true;
                        var bs_pl1 = template.bs_pl1[i];
                        var group_id1 = template.group_id1[i];
                        dt.Rows.Add(template_detail_id1,
                                    bs_pl_side1,
                                    group_id1,
                                    group_no1,
                                    group_name1,
                                    parent_id1,
                                    group_level1,
                                    main_heading1,
                                    bs_pl1);

                    }
                }
          
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_fin_template_detail";
                t1.Value = dt;
                var created_by = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var template_id = new SqlParameter("@template_id", template.template_id == 0 ? -1 : template.template_id);
                var t_name = new SqlParameter("@template_name", template.template_name == null ? "" : template.template_name);
                var t_format = new SqlParameter("@t_format", template.t_format2=="true"?true:false);
                var createdby = new SqlParameter("@created_by", created_by);
                var createdts = new SqlParameter("@created_ts", DateTime.Now);
                var val = _scifferContext.Database.SqlQuery<string>("exec save_FinancialTemplate @t1,@template_id,@template_name,@t_format,@created_by,@created_ts", 
                    t1, template_id, t_name, t_format, createdby, createdts).FirstOrDefault();
                if (val == "Saved")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            //return true;
        }
        public int TempID(string st)
        {
            var Y = _scifferContext.ref_fin_template_detail.Where(x => x.group_name == st).FirstOrDefault();
            var id = Y == null ? "0" : Y.template_detail_id.ToString();
            return int.Parse(id);
        }

       
        public List<ref_fin_template_vm> getall()
        {
            var query = (from template in _scifferContext.ref_fin_template.OrderByDescending(x => x.template_id)
                         select new ref_fin_template_vm()
                         {
                             template_id = template.template_id,
                             template_name = template.template_name,
                             t_format = template.t_format,
                             t_format2 = template.t_format==true?"Yes":"No",
                             created_by= template.created_by,
                             created_ts = template.created_ts,

                         }).OrderByDescending(x => x.template_id).ToList();
            return query;
        }
        

        public List<ref_fin_template_vm> Get_Parent_Ledger(int id)
        {
            var query = (from ed in _scifferContext.ref_fin_template_detail.Where(x => x.template_detail_id == id)
                         select new ref_fin_template_vm()
                         {
                             group_name = ed.group_name,
                             group_level = ed.group_level,

                         }).ToList();
            return query;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        

        public bool Update(ref_fin_template_detail ledger)
        {
            try
            {
                var gl = _scifferContext.ref_fin_template_detail.Where(x => x.template_detail_id == ledger.template_detail_id).FirstOrDefault();
                gl.template_id = ledger.template_id;
                gl.bs_pl = ledger.bs_pl;
                gl.bs_pl_side = ledger.bs_pl_side;
                gl.template_detail_id = ledger.template_detail_id;
                gl.group_no = ledger.group_no;
                gl.group_name = ledger.group_name;
                gl.parent_id = ledger.parent_id;
                gl.group_level = ledger.group_level;
                gl.is_active = true;
                gl.main_heading = ledger.main_heading;
                _scifferContext.Entry(gl).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

       

        public List<exportdata1> GetExport()
        {
            var query = (from g in _scifferContext.ref_fin_template_detail//.Where(x => x.is_active == true)
                                                                          //join at in _scifferContext.ref_gl_acount_type on g.gl_account_type equals at.gl_account_type_id
                         select new exportdata1
                         {
                             BsAndPl = g.bs_pl,
                             BsPlSide = g.bs_pl_side,
                             GroupNo = g.group_no,
                             GroupName = g.group_name,
                             parent_id = g.parent_id,
                             GroupLevel = g.group_level,
                             MainHeading = g.main_heading,
                         }).ToList();
            return query;
        }
        public ref_fin_template_vm GetTreeVeiwList(int id)
        {
            ref_fin_template st = _scifferContext.ref_fin_template.FirstOrDefault(c => c.template_id == id);
            Mapper.CreateMap<ref_fin_template, ref_fin_template_vm>();
            ref_fin_template_vm std = Mapper.Map<ref_fin_template, ref_fin_template_vm>(st);

            List<TreeView_Node_VM> rootNode = (from e2 in _scifferContext.ref_fin_template.Where(x=>x.template_id == id) 
                                               join e1 in _scifferContext.ref_fin_template_detail on e2.template_id equals e1.template_id
                                               where e1.is_active == true && e1.parent_id == null && e1.template_id == id
                                               select new TreeView_Node_VM()
                                               {
                                                   template_id = e2.template_id,
                                                   template_name = e2.template_name,
                                                   t_format = e2.t_format,
                                                   template_detail_id = e1.template_detail_id,
                                                   group_name = e1.group_name,
                                                   parent_id = e1.parent_id,
                                                   group_level = e1.group_level,
                                                   bs_pl = e1.bs_pl,
                                                   bs_pl_side = e1.bs_pl_side,
                                               }).ToList();
            for (var i = 0; i < rootNode.Count; i++)
            {
                BuildChildNode(rootNode[i]);
            }

            std.TreeView_Node_VM = rootNode;
            return std;
        }

        private void BuildChildNode(TreeView_Node_VM rootNode)
        {
            if (rootNode != null)
            {
                List<TreeView_Node_VM> chidNode = (from e1 in _scifferContext.ref_fin_template_detail
                                                   where e1.parent_id == rootNode.template_detail_id.ToString()
                                                   select new TreeView_Node_VM()
                                                   {
                                                       template_detail_id = e1.template_detail_id,
                                                       group_name = e1.group_name,
                                                       parent_id = e1.parent_id,
                                                       group_level = e1.group_level,
                                                       bs_pl = e1.bs_pl,
                                                       bs_pl_side = e1.bs_pl_side,
                                                   }).ToList();
                if (chidNode.Count > 0)
                {
                    foreach (var childRootNode in chidNode)
                    {
                        BuildChildNode(childRootNode);
                        rootNode.ChildNode.Add(childRootNode);
                    }
                }
            }
        }
        public List<ref_fin_template_detail> GetGroupName()
        {
            return _scifferContext.ref_fin_template_detail.ToList();
            // return;
        }
        public bool AddTemplateGLMapping(ref_fin_template_gl_mapping_vm vm)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("gl_ledger_id", typeof(int));
                dt.Columns.Add("template_detail_id", typeof(int));
                if (vm.gl_ledger_id != null)
                {
                    for (int i = 0; i < vm.gl_ledger_id.Count; i++)
                    {
                        dt.Rows.Add(vm.gl_ledger_id[i], vm.template_detail_id[i]);
                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_template_gl_mapping";
                t1.Value = dt;
                var template_id = new SqlParameter("@template_id", vm.template_id);
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_template_gl_mapping @template_id,@t1 ", template_id, t1).FirstOrDefault();
                if (val == "Saved")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                GC.Collect();
            }


        }
        public List<ref_fin_template_gl_mapping_vm> GetGroupByTemplate(int id)
        {
            var query = (from t in _scifferContext.ref_fin_template_detail.Where(x => x.is_active == true && x.main_heading == false && x.template_id == id)
                         join t1 in _scifferContext.ref_fin_template_gl_mapping on t.template_detail_id equals t1.template_detail_id into j1
                         from t2 in j1.DefaultIfEmpty()
                         select new ref_fin_template_gl_mapping_vm
                         {
                             gl_ledger_id1 = t2.gl_ledger_id,
                             template_detail_id1 = t.template_detail_id,
                         }).ToList();
            return query;
        }
    }
}
