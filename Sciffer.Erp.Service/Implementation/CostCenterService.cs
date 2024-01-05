using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using AutoMapper;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class CostCenterService : ICostCenterService

    {
        private readonly ScifferContext _scifferContext;

        public CostCenterService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }

        public List<ref_cost_center> GetAll()
        {
            return _scifferContext.ref_cost_center.ToList().Where(x => x.is_active == true).ToList();            
        }
        public List<ref_cost_center_vm> GetDropdownParent()
        {
            var parent = (from ed in _scifferContext.ref_cost_center.ToList().Where(x => x.is_active == true && x.head_parent==1)
                          select new ref_cost_center_vm()
                          {
                              cost_center_description = ed.cost_center_code + " - " + ed.cost_center_description,
                              cost_center_code = ed.cost_center_code,
                            }).ToList();
            return parent;

        }

        public ref_cost_center Get(int id)
        {
            
                ref_cost_center CC = _scifferContext.ref_cost_center.FirstOrDefault(c => c.cost_center_id == id);
                return CC;
        }

        public bool Add(ref_cost_center_vm CostCenter)
        {
            try {
                ref_cost_center ref_cost_center = new ref_cost_center();
                ref_cost_center.cost_center_code = CostCenter.cost_center_code;
                ref_cost_center.cost_center_description = CostCenter.cost_center_description;
                ref_cost_center.is_active = true;
                ref_cost_center.is_blocked = CostCenter.is_blocked;
                ref_cost_center.parent_code = CostCenter.parent_code;
                ref_cost_center.cost_center_level = CostCenter.cost_center_level;
                ref_cost_center.head_parent = CostCenter.head_parent;
                _scifferContext.ref_cost_center.Add(ref_cost_center);
                _scifferContext.SaveChanges();
                //CostCenter.cost_center_id = _scifferContext.ref_cost_center.Max(a => a.cost_center_id);

            } catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Update(ref_cost_center_vm CostCenter)
        {
            try
            {
                var ref_cost_center = _scifferContext.ref_cost_center.Where(x => x.cost_center_id == CostCenter.cost_center_id).FirstOrDefault();

                ref_cost_center.cost_center_code = CostCenter.cost_center_code;
                ref_cost_center.cost_center_description = CostCenter.cost_center_description;
                ref_cost_center.is_active = true;
                ref_cost_center.is_blocked = CostCenter.is_blocked;
                ref_cost_center.parent_code = CostCenter.parent_code;
                ref_cost_center.cost_center_level = CostCenter.cost_center_level ;                
                ref_cost_center.head_parent = CostCenter.head_parent;        
               
                _scifferContext.Entry(ref_cost_center).State = EntityState.Modified;

                //var levelChange = _scifferContext.ref_cost_center.Where(x => x.parent_code == ref_cost_center.cost_center_code).ToList();
                //foreach(var i in levelChange)
                //{
                //    ref_cost_center ed = new ref_cost_center();
                //    ed.cost_center_level = i.cost_center_level - 1;
                //    _scifferContext.Entry(ed).State = EntityState.Modified;
                //}
                _scifferContext.SaveChanges();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {

            try {
                var CC = _scifferContext.ref_cost_center.FirstOrDefault(d => d.cost_center_id == id);
                CC.is_active = false;
                _scifferContext.Entry(CC).State = EntityState.Modified;
                _scifferContext.SaveChanges();


            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

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

        public int getLevel(int id)
        {
            if(id==0)
            {
                return 1;
            }
            else
            {
                return _scifferContext.ref_cost_center.Where(x => x.cost_center_id == id).FirstOrDefault().cost_center_level + 1;
            }
            
        }

        public List<ref_cost_center_vm> GetCostCenter()
        {
            var query = (from c in _scifferContext.ref_cost_center.Where(x => x.is_active == true)
                         select new ref_cost_center_vm
                         {
                          cost_center_id=c.cost_center_id,
                          cost_center_code=c.cost_center_code+"-"+c.cost_center_description,
                         }).ToList();
            return query;
        }


        public TreeViewNodeCostCenter GetTreeVeiwList(string code)
        {
            TreeViewNodeCostCenter rootNode = (from e1 in _scifferContext.ref_cost_center
                                       where e1.cost_center_code == code && e1.is_active == true && e1.parent_code == null
                                       select new TreeViewNodeCostCenter()
                                       {
                                           cost_center_id = e1.cost_center_id,
                                           cost_center_description = e1.cost_center_description,
                                           cost_center_code = e1.cost_center_code,
                                           cost_center_level = e1.cost_center_level,
                                           parent_code = e1.parent_code,     
                                           head_parent = e1.head_parent,                                   
                                       }).FirstOrDefault();
            BuildChildNode(rootNode);
            return rootNode;
        }

        private void BuildChildNode(TreeViewNodeCostCenter rootNode)
        {
            if (rootNode != null)
            {
                List<TreeViewNodeCostCenter> chidNode = (from e1 in _scifferContext.ref_cost_center
                                                 where e1.parent_code == rootNode.cost_center_code && e1.is_active==true
                                                 select new TreeViewNodeCostCenter()
                                                 {
                                                     cost_center_id = e1.cost_center_id,
                                                     cost_center_description = e1.cost_center_description,
                                                     cost_center_code = e1.cost_center_code,
                                                     cost_center_level = e1.cost_center_level,
                                                     parent_code = e1.parent_code,
                                                     head_parent = e1.head_parent,
                                                 }).ToList<TreeViewNodeCostCenter>();
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

        public string GetParent()
        {
            var parent_id = _scifferContext.ref_cost_center.Where(x=>x.parent_code==null).FirstOrDefault();
            return parent_id.cost_center_code==null?"":parent_id.cost_center_code;
        }
        public int GetLevel(string code)
        {
            var level = _scifferContext.ref_cost_center.Where(x => x.cost_center_code == code && x.is_active==true).FirstOrDefault();
            return level.cost_center_level;

        }
        public ref_cost_center_vm GetChild(int id)
        {
            var data = (from ed in _scifferContext.ref_cost_center.Where(x => x.cost_center_id == id && x.is_active==true)
                        select new ref_cost_center_vm
                        {
                            parent_code = ed.parent_code,
                        }).FirstOrDefault();
            return data;
        }
        public List<exportdataCostCenter> GetExport()
        {
            var query = (from g in _scifferContext.ref_cost_center.Where(x => x.is_active == true)
                         select new exportdataCostCenter
                         {
                             CostCenterCode = g.cost_center_code,
                             CostCenterName = g.cost_center_description,
                             HeadAccount = g.head_parent == 1 ? "Head" : "Account",
                             ParentCode = g.parent_code,
                         }).ToList();
            return query;
        }
    }
}
