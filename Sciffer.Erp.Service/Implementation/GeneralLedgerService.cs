using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;

namespace Sciffer.Erp.Service.Implementation
{
    public class GeneralLedgerService : IGeneralLedgerService
    {

        private readonly ScifferContext _scifferContext;

        public GeneralLedgerService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public int GetID(string st)
        {
            var X = _scifferContext.ref_general_ledger.Where(x => x.gl_ledger_code == st).FirstOrDefault();
            var id = X == null ? "0" : X.gl_ledger_id.ToString();
            return int.Parse(id);
        }
        public int GenID(string st)
        {
            var Y = _scifferContext.ref_general_ledger.Where(x => x.gl_ledger_name == st && x.is_active == true).FirstOrDefault();
            var id = Y == null ? "0" : Y.gl_ledger_id.ToString();
            return int.Parse(id);
        }

        public string ParentCode(string code)
        {
            var list = _scifferContext.ref_general_ledger.Where(x => x.gl_ledger_code == code && x.is_active == true && x.gl_head_account == 1).FirstOrDefault();
            if (list == null)
            {
                return "";
            }
            else
            {
                //list.ref_gl_acount_type.gl_account_type_description;
                return list.gl_ledger_code + "-" + list.ref_gl_acount_type.gl_account_type_description.ToLower() + "-" + list.gl_head_account + "-" + list.gl_level;
            }
        }

        public bool Add1(ref_general_ledger ledger)
        {
            try
            {
                ref_general_ledger gl = new ref_general_ledger();
                gl.gl_account_type = ledger.gl_account_type;
                gl.gl_head_account = ledger.gl_head_account;
                gl.gl_ledger_code = ledger.gl_ledger_code;
                gl.gl_ledger_id = ledger.gl_ledger_id;
                gl.gl_ledger_name = ledger.gl_ledger_name;
                gl.gl_parent_ledger_code = ledger.gl_parent_ledger_code;
                gl.gl_level = ledger.gl_level;
                gl.is_active = true;
                gl.is_blocked = ledger.is_blocked;
                _scifferContext.ref_general_ledger.Add(gl);
                _scifferContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        public bool Delete(int id)
        {
            try
            {
                var ledger = _scifferContext.ref_general_ledger.FirstOrDefault(c => c.gl_ledger_id == id);
                ledger.is_active = false;
                _scifferContext.Entry(ledger).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #region dispoable methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _scifferContext.Dispose();
            }
        }
        #endregion

        public ref_general_ledger Get(int id)
        {
            var ledger = _scifferContext.ref_general_ledger.FirstOrDefault(c => c.gl_ledger_id == id);
            return ledger;
        }

        public List<ref_ledger_vm> Get_Parent_Ledger(int id)
        {
            var query = (from ed in _scifferContext.ref_general_ledger.Where(x => x.is_active == true && x.gl_head_account == 1 && x.gl_account_type == id)
                         select new ref_ledger_vm()
                         {
                             gl_ledger_code = ed.gl_ledger_code,
                             gl_ledger_name = ed.gl_ledger_code + "-" + ed.gl_ledger_name,

                         }).ToList();
            return query;
        }
        public List<ref_ledger_vm> GetAccountGeneral()
        {
            var query = (from ed in _scifferContext.ref_general_ledger.Where(x => x.is_active == true && x.gl_head_account == 2)
                         select new ref_ledger_vm
                         {
                             gl_ledger_id = ed.gl_ledger_id,
                             gl_ledger_name = ed.gl_ledger_code + " - " + ed.gl_ledger_name,
                         }).ToList();
            return query;
        }
        public List<ref_general_ledger> GetAccountGeneralLedger()
        {
            return _scifferContext.ref_general_ledger.Where(x => x.is_active == true && x.gl_head_account == 2).ToList();
        }
        public List<ref_ledger_vm> GetAll()
        {
            var query = (from g in _scifferContext.ref_general_ledger.Where(x => x.is_active == true)
                         join at in _scifferContext.ref_gl_acount_type on g.gl_account_type equals at.gl_account_type_id
                         join gp in _scifferContext.ref_general_ledger on g.gl_ledger_code equals gp.gl_parent_ledger_code into j1
                         from cpr in j1.DefaultIfEmpty()

                         select new ref_ledger_vm
                         {
                             gl_account_type = g.gl_account_type,
                             gl_account_type_name = at.gl_account_type_description,
                             gl_head_account = g.gl_head_account,
                             gl_head_account_name = g.gl_head_account == 1 ? "Head" : "Account",
                             gl_ledger_code = g.gl_ledger_code,
                             gl_ledger_id = g.gl_ledger_id,
                             gl_ledger_name = g.gl_ledger_name,
                             gl_ledger_name_drop = g.gl_ledger_code + "-" + g.gl_ledger_name,
                             gl_level = g.gl_level,
                             gl_parent_ledger_code = g.gl_parent_ledger_code/*cpr==null ?0: cpr.gl_ledger_id*/,
                             gl_parent_ledger_name = cpr.gl_ledger_name/*cpr==null?string.Empty:cpr.gl_ledger_name*/,
                             //cash_bank = g.cash_bank=="cash"?"cash": g.cash_bank=="bank" ? "bank" : "none" ,
                             //round_off = g.round_off,
                             is_blocked = g.is_blocked,
                         }).ToList();
            return query;
        }

        public bool Update(ref_general_ledger ledger)
        {
            try
            {
                ref_general_ledger gl = new ref_general_ledger();
                gl.gl_account_type = ledger.gl_account_type;
                gl.gl_head_account = ledger.gl_head_account;
                gl.gl_ledger_code = ledger.gl_ledger_code;
                gl.gl_ledger_id = ledger.gl_ledger_id;
                gl.gl_ledger_name = ledger.gl_ledger_name;
                gl.gl_parent_ledger_code = ledger.gl_parent_ledger_code;
                gl.gl_level = ledger.gl_level;
                gl.is_active = true;
                gl.is_blocked = ledger.is_blocked;
                Build(gl.gl_ledger_code, gl.gl_account_type);
                //gl.cash_bank = ledger.cash_bank;
                //gl.round_off = ledger.round_off;
                _scifferContext.Entry(gl).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public TreeViewNodeVM GetTreeVeiwList(int id)
        {
            TreeViewNodeVM rootNode = (from e1 in _scifferContext.ref_general_ledger
                                       where e1.gl_ledger_id == id && e1.is_active == true && e1.gl_parent_ledger_code == null
                                       select new TreeViewNodeVM()
                                       {
                                           gl_ledger_id = e1.gl_ledger_id,
                                           gl_ledger_name = e1.gl_ledger_name,
                                           gl_ledger_code = e1.gl_ledger_code,
                                           gl_head_account = e1.gl_head_account,
                                           gl_parent_code = e1.gl_parent_ledger_code,
                                           gl_level = e1.gl_level,
                                       }).FirstOrDefault();
            BuildChildNode(rootNode);

            return rootNode;
        }
        private void Build(string gl_ledger_code, int gl_account_type)
        {
            if (gl_ledger_code != null)
            {
                List<Node> chidNode = (from e1 in _scifferContext.ref_general_ledger
                                       where e1.gl_parent_ledger_code == gl_ledger_code
                                       select new Node
                                       {
                                           gl_account_type = e1.gl_account_type,
                                           gl_ledger_code = e1.gl_ledger_code,
                                           gl_ledger_id = e1.gl_ledger_id,
                                       }).ToList<Node>();

                if (chidNode.Count > 0)
                {
                    foreach (var childRootNode in chidNode)
                    {
                        var chilx = _scifferContext.ref_general_ledger.Where(x => x.gl_ledger_id == childRootNode.gl_ledger_id).FirstOrDefault();
                        chilx.gl_account_type = gl_account_type;
                        _scifferContext.Entry(chilx).State = EntityState.Modified;
                        // _scifferContext.SaveChanges(); 
                        Build(chilx.gl_ledger_code, gl_account_type);

                        //root.ChildNode.Add(childRootNode);
                    }
                }
            }
        }
        private void BuildChildNode(TreeViewNodeVM rootNode)
        {
            if (rootNode != null)
            {
                List<TreeViewNodeVM> chidNode = (from e1 in _scifferContext.ref_general_ledger
                                                 where e1.gl_parent_ledger_code == rootNode.gl_ledger_code
                                                 select new TreeViewNodeVM()
                                                 {
                                                     gl_ledger_name = e1.gl_ledger_name,
                                                     gl_ledger_id = e1.gl_ledger_id,
                                                     gl_ledger_code = e1.gl_ledger_code,
                                                     gl_head_account = e1.gl_head_account,
                                                     gl_parent_code = e1.gl_parent_ledger_code,
                                                     gl_level = e1.gl_level,
                                                 }).ToList<TreeViewNodeVM>();
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

        public bool Add(ref_general_ledger ledger)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("gl_account_type", typeof(int));
                dt.Columns.Add("gl_ledger_code", typeof(string));
                dt.Columns.Add("gl_ledger_name", typeof(string));
                dt.Columns.Add("gl_head_account", typeof(int));
                dt.Columns.Add("gl_parent_ledger_code", typeof(string));
                dt.Columns.Add("gl_level", typeof(int));
                dt.Columns.Add("is_active", typeof(bool));
                dt.Columns.Add("is_blocked", typeof(bool));
                dt.Rows.Add(ledger.gl_account_type, ledger.gl_ledger_code, ledger.gl_ledger_name,
                             ledger.gl_head_account, ledger.gl_parent_ledger_code, ledger.gl_level, 1, ledger.is_blocked);
                    
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_general_ledger";
                t1.Value = dt;
                int create_user = 0;
                var user = new SqlParameter("@create_user", create_user);
                var val = _scifferContext.Database.SqlQuery<string>("exec save_GeneralLedger @t1,@create_user", t1, user).FirstOrDefault();
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
            return true;
        }
        public string AddExcel(List<ref_ledger_vm> vm)
        {
            try
            {
                var errorMessage = "";
                DataTable dt = new DataTable();
                dt.Columns.Add("gl_account_type", typeof(int));
                dt.Columns.Add("gl_ledger_code", typeof(string));
                dt.Columns.Add("gl_ledger_name", typeof(string));
                dt.Columns.Add("gl_head_account", typeof(int));
                dt.Columns.Add("gl_parent_ledger_code", typeof(string));
                dt.Columns.Add("gl_level", typeof(int));
                dt.Columns.Add("is_active", typeof(bool));
                dt.Columns.Add("is_blocked", typeof(bool));
                if (vm != null)
                {
                    foreach (var items in vm)
                    {
                        var gl_ledger_code = _scifferContext.ref_general_ledger.Where(x => x.gl_ledger_code == items.gl_ledger_code).FirstOrDefault();
                        if (gl_ledger_code != null)
                        {
                            errorMessage = items.gl_ledger_code + "Gl Ledger Code Already Exist in Database";
                        }
                        else
                        {
                            dt.Rows.Add(items.gl_account_type, items.gl_ledger_code, items.gl_ledger_name, items.gl_head_account, items.gl_parent_ledger_code, 
                                items.gl_level, 1, items.is_blocked);
                        }
                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_general_ledger";
                t1.Value = dt;
                int create_user = 0;
                var user = new SqlParameter("@create_user", create_user);
                if (errorMessage == "")
                {
                    var val = _scifferContext.Database.SqlQuery<string>("exec save_GeneralLedger @t1,@create_user", t1, user).FirstOrDefault();
                    if (val == "Saved")
                    {
                        return "Saved";
                    }
                    else
                    {
                        return "Failed";
                    }
                }
                return errorMessage;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }           
        }
        public string AddExcel1(List<ref_ledger_vm> vm)
        {
            var errorMessage = "";
            try
            {
                List<ref_general_ledger> ed1 = new List<ref_general_ledger>();
                foreach (var data in vm)
                {
                    ref_general_ledger ed = new ref_general_ledger();
                    ed.gl_account_type = data.gl_account_type;
                    var gl_ledger_code = _scifferContext.ref_general_ledger.Where(x => x.gl_ledger_code == data.gl_ledger_code).FirstOrDefault();
                    if (gl_ledger_code != null)
                    {
                        errorMessage = gl_ledger_code + "Gl Ledger Code Already Exist in Database";
                    }
                    else
                    {
                        ed.gl_ledger_code = data.gl_ledger_code.Trim();
                        ed.gl_ledger_name = data.gl_ledger_name.Trim();
                        ed.gl_head_account = data.gl_head_account;
                        ed.gl_parent_ledger_code = data.gl_parent_ledger_code;
                        ed.gl_level = data.gl_level;
                        ed.is_active = true;
                        ed.is_blocked = false;
                        ed1.Add(ed);
                    }

                }
                if (errorMessage == "")
                {
                    foreach (var value in ed1)
                    {
                        _scifferContext.ref_general_ledger.Add(value);
                    }
                }
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return errorMessage = "Failed";
            }
            return errorMessage;
        }

        public List<exportdata> GetExport()
        {
            var query = (from g in _scifferContext.ref_general_ledger.Where(x => x.is_active == true)
                         join at in _scifferContext.ref_gl_acount_type on g.gl_account_type equals at.gl_account_type_id
                         select new exportdata
                         {

                             GeneralLedgerType = at.gl_account_type_description,
                             GeneralLedgerCode = g.gl_ledger_code,
                             GeneralLedgerName = g.gl_ledger_name,
                             HeadAccount = g.gl_head_account == 1 ? "Head" : "Account",
                             ParentLedgerCode = g.gl_parent_ledger_code/*cpr==null ?0: cpr.gl_ledger_id*/,
                             Level = g.gl_level,
                         }).ToList();
            return query;
        }

        public ref_ledger_vm GetChild(int id)
        {
            var data = (from ed in _scifferContext.ref_general_ledger.Where(x => x.gl_ledger_id == id)
                        select new ref_ledger_vm
                        {
                            gl_parent_ledger_code = ed.gl_ledger_code,
                            gl_account_type = ed.gl_account_type,
                        }).FirstOrDefault();
            return data;
        }
    }

}
