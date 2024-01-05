using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;
using Sciffer.Erp.Domain.ViewModel;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly ScifferContext _scifferContext;

        public LoginService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public ref_user_management CheckLoginParameters(string user_name, string password)
        {
            try
            {
                return _scifferContext.ref_user_management.Where(x => x.user_name == user_name && x.password == password && x.is_block == false).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public bool CheckOperatorLogin(int user_id, string role_str)
        {
            var op = _scifferContext.ref_user_role_mapping.Where(x => x.user_id == user_id).FirstOrDefault();
            if (op != null)
            {
                var role = _scifferContext.ref_user_management_role.Where(x => x.role_id == op.role_id && x.role_code == role_str).FirstOrDefault();
                if (role != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public bool UpdatePassword(string Uname, string Password)
        {
            try
            {

                var ss = HttpContext.Current.Session["User_Id"].ToString();
                var user_id = Convert.ToInt32(ss);
                ref_user_management RCC = _scifferContext.ref_user_management.Where(x => x.user_id == user_id && x.user_name == Uname).FirstOrDefault();

                RCC.password = Password;

                _scifferContext.Entry(RCC).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }

            catch (Exception e)
            {

                return false;
            }

            return true;

        }

        public int GetOpentaskcount(int user)
        {
            try
            {

                ref_user_management RCC1 = _scifferContext.ref_user_management.Where(x => x.user_id == user).FirstOrDefault();

                ref_user_management RCC = _scifferContext.ref_user_management.Where(x => x.user_name == "Admin").FirstOrDefault();
                if (user == RCC.user_id)
                {
                    var status = _scifferContext.ref_status.Where(x => x.status_name == "Open" && x.form == "MCAL").FirstOrDefault();
                    var cnt = _scifferContext.ref_task.Where(v => v.status_id == status.status_id).Count();
                    return cnt;
                }
                else
                {
                    var status = _scifferContext.ref_status.Where(x => x.status_name == "Open" && x.form == "MCAL").FirstOrDefault();
                    var cnt = _scifferContext.ref_task.Where(v => v.task_doer_id == RCC1.employee_id && v.status_id == status.status_id).Count();
                    return cnt;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                GC.Collect();
            }

        }

        public int GetOverduetask(int user)
        {
            try
            {
                ref_user_management RCC1 = _scifferContext.ref_user_management.Where(x => x.user_id == user).FirstOrDefault();

                ref_user_management RCC = _scifferContext.ref_user_management.Where(x => x.user_name == "Admin").FirstOrDefault();
                if (user == RCC.user_id)
                {
                    var cnt = _scifferContext.ref_task.Where(v => v.due_date < DateTime.Now).Count();
                    return cnt;
                }
                else
                {
                    var cnt = _scifferContext.ref_task.Where(v => v.task_doer_id == RCC1.employee_id && v.due_date < DateTime.Now).Count();
                    return cnt;
                }
            }
            catch (Exception)
            {

                return 0;
            }
            finally
            {
                GC.Collect();
            }

        }


        //public int GetPurchaseRequisitionAllCount(int user)
        //{
        //    try
        //    {
        //        //   var status = _scifferContext.ref_status.Where(x => x.status_name == "Open" && x.form == "PO").FirstOrDefault();
        //        //  var cnt = _scifferContext.pur_requisition.Where(v => v.status_id == status.status_id && v.approval_status == 0).Count();
        //          var cnt = _scifferContext.pur_requisition.Where(v =>v.approval_status == 0).Count();

        //        return cnt;    
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //    finally
        //    {
        //        GC.Collect();
        //    }

        //}


        public int GetPurchaseRequisitionAllRejectAndApprovedcount(int user)
        {
            try
            {
                var status = _scifferContext.ref_status.Where(x => x.status_name == "Open" && x.form == "PO").FirstOrDefault();
                var cnt = _scifferContext.pur_requisition.Where(v => v.approval_status == 1 && v.is_seen == false).Count();
                //var cnt = _scifferContext.pur_requisition.Where(v => v.approval_status == 1 || v.approval_status == 2).Count();

                return cnt;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                GC.Collect();
            }

        }

        public int GetPurchaseRequisitionRejectedcount(int user)
        {
            try
            {
                //var status = _scifferContext.ref_status.Where(x => x.status_name == "Open" && x.form == "PO").FirstOrDefault();
                var cnt = _scifferContext.pur_requisition.Where(v => v.approval_status == 2 && v.is_seen == false).Count();
                return cnt;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                GC.Collect();
            }

        }

        //public int GetApprovedPurchaseOrderCount(int user)
        //{
        //    try
        //    {             
        //        var status = _scifferContext.ref_status.Where(x => x.status_name == "Open" && x.form == "PO").FirstOrDefault();
        //          //  var cnt = _scifferContext.pur_po.Where(v => v.status_id == status.status_id && v.approval_status == 0).Count();
        //            var cnt = _scifferContext.pur_po.Where(v => v.approval_status == 0 ).Count();

        //            return cnt;
        //        }
        //        //else
        //        //{
        //        //    // var status = _scifferContext.ref_status.Where(x => x.status_name == "Open" && x.form == "PO").FirstOrDefault();
        //        //    //var cnt = _scifferContext.pur_po.Where(v => v.status_id == status.status_id).Count();
        //        //    //return cnt;
        //        //    var status = _scifferContext.ref_status.Where(x => x.status_name == "Open" && x.form == "PO").FirstOrDefault();
        //        //    var cnt = _scifferContext.pur_po.Where(v => v.po_id == RCC1.employee_id && v.status_id == status.status_id && v.approval_status == 0).Count();
        //        //    return cnt;
        //        //}
        // //   }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //    finally
        //    {
        //        GC.Collect();
        //    }

        //}

        public int GetPurchaseOrderAllRejectAndApprovedcount(int user)
        {
            try
            {
                var status = _scifferContext.ref_status.Where(x => x.status_name == "Open" && x.form == "PO").FirstOrDefault();
                var cnt = _scifferContext.pur_po.Where(v => v.approval_status == 1 && v.is_seen == false).Count();
                return cnt;
            }


            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                GC.Collect();
            }

        }
        public int GetPurchaseOrderAllRejectdcount(int user)
        {
            try
            {
                // var status = _scifferContext.ref_status.Where(x => x.status_name == "Open" && x.form == "PO").FirstOrDefault();
                var cnt = _scifferContext.pur_po.Where(v => v.approval_status == 2 && v.is_seen == false).Count();
                return cnt;
            }


            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                GC.Collect();
            }

        }

        public int GetApprovedMRICount(int user)
        {
            try
            {
                var status = _scifferContext.ref_status.Where(x => x.status_name == "Open" && x.form == "MRI").FirstOrDefault();
                //var cnt = _scifferContext.material_requision_note.Where(v => v.approval_status == 0).Count();
                var cnt = _scifferContext.material_requision_note.Where(v => v.status_id == status.status_id && v.is_seen == false).Count();
                return cnt;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                GC.Collect();
            }

        }



    }
}