using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using System.Data.Entity;
using AutoMapper;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class PostingPeriodsService : IPostingPeriodsService
    {
        private readonly ScifferContext _scifferContext;


        public PostingPeriodsService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool Add(ref_posting_periods_vm Posting)
        {
            try
            {
                string[] emptyStringArray = new string[0];
                try
                {
                    emptyStringArray = Posting.codelist.Split(new string[] { "~" }, StringSplitOptions.None);

                }
                catch
                {

                }
                ref_posting_periods rp = new ref_posting_periods();
                rp.financial_year_id = Posting.financial_year_id;
                rp.frequency_id = Posting.frequency_id;
                rp.is_active = true;
                rp.no_of_periods = Posting.no_of_periods;
                rp.posting_periods_id = Posting.posting_periods_id;
                var finance = _scifferContext.REF_FINANCIAL_YEAR.Where(x => x.FINANCIAL_YEAR_ID == Posting.financial_year_id).FirstOrDefault();
                DateTime dt = finance.FINANCIAL_YEAR_FROM;
                DateTime dt1 = finance.FINANCIAL_YEAR_TO;
                DateTime dt2 = DateTime.Now;
                DateTime dt3 = DateTime.Now;
                //rp.company_id = Convert.ToInt32(HttpContext.Current.Session["Comp"].ToString());
                List<ref_posting_periods_detail> rpd = new List<ref_posting_periods_detail>();
                for (int i = 0; i < emptyStringArray.Count() - 1; i++)
                {
                    ref_posting_periods_detail pd = new ref_posting_periods_detail();
                    pd.posting_periods_id = Posting.posting_periods_id;
                    if (Posting.no_of_periods==1)
                    {
                        pd.from_date = dt;
                        pd.to_date = dt1;
                    }
                    else if (Posting.no_of_periods==4)
                    {
                        if(i==0)
                        {
                            pd.from_date = dt;
                            pd.to_date = new DateTime(dt.Year, 6, 30);
                        }
                        else if(i==1)
                        {                          
                            pd.from_date = new DateTime(dt.Year, 7, 1);
                            pd.to_date = new DateTime(dt.Year, 9,30);                          
                        }
                        else if (i == 2)
                        {
                            pd.from_date = new DateTime(dt.Year, 10, 1);
                            pd.to_date = new DateTime(dt.Year, 12, 31);                           
                        }
                        else if (i == 3)
                        {
                            pd.from_date = new DateTime(dt1.Year, 1, 1);
                            pd.to_date = new DateTime(dt1.Year, 3, 31);
                            dt2 = pd.to_date;
                        }

                    }
                    else if (Posting.no_of_periods == 12)
                    {
                        if(i==0)
                        {
                            pd.from_date = dt;
                            pd.to_date = new DateTime(dt.Year, 4, 30);
                        }
                        else if (i == 1)
                        {
                            pd.from_date = new DateTime(dt.Year, 5, 1);
                            pd.to_date = new DateTime(dt.Year,5, 31);
                        }
                        else if (i == 2)
                        {
                            pd.from_date = new DateTime(dt.Year, 6, 1);
                            pd.to_date = new DateTime(dt.Year, 6, 30);
                        }
                        else if (i == 3)
                        {
                            pd.from_date = new DateTime(dt.Year, 7, 1);
                            pd.to_date = new DateTime(dt.Year, 7, 31);
                        }
                        else if (i == 4)
                        {
                            pd.from_date = new DateTime(dt.Year, 8, 1);
                            pd.to_date = new DateTime(dt.Year, 8, 31);
                        }
                        else if (i == 5)
                        {
                            pd.from_date = new DateTime(dt.Year, 9, 1);
                            pd.to_date = new DateTime(dt.Year, 9, 30);
                        }
                        else if (i == 6)
                        {
                            pd.from_date = new DateTime(dt.Year, 10, 1);
                            pd.to_date = new DateTime(dt.Year, 10, 31);
                        }
                        else if (i == 7)
                        {
                            pd.from_date = new DateTime(dt.Year, 11, 1);
                            pd.to_date = new DateTime(dt.Year, 11, 30);
                        }
                        else if (i == 8)
                        {
                            pd.from_date = new DateTime(dt.Year, 12, 1);
                            pd.to_date = new DateTime(dt.Year, 12, 31);
                        }
                        else if (i == 9)
                        {
                            pd.from_date = new DateTime(dt1.Year, 1, 1);
                            pd.to_date = new DateTime(dt1.Year, 1, 31);
                        }
                        else if (i == 10)
                        {
                            int daysInFeb = System.DateTime.DaysInMonth(dt1.Year, 2);
                            pd.from_date = new DateTime(dt1.Year, 2, 1);
                            pd.to_date = new DateTime(dt1.Year, 2, daysInFeb);
                        }
                        else if (i == 11)
                        {
                            pd.from_date = new DateTime(dt1.Year, 3, 1);
                            pd.to_date = new DateTime(dt1.Year, 3, 31);
                        }
                    }

                    pd.status_id = 1;//unblock
                    pd.posting_periods_code= emptyStringArray[i].Split(new char[] { ',' })[1];
                    rpd.Add(pd);
                }
                rp.ref_posting_periods_detail = rpd;
                _scifferContext.ref_posting_periods.Add(rp);
                _scifferContext.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool Delete(int id)
        {
            try
            {
                ref_posting_periods rp = new ref_posting_periods();
                rp = _scifferContext.ref_posting_periods.Where(x => x.posting_periods_id == id).FirstOrDefault();
                _scifferContext.Entry(rp).State = EntityState.Modified;
                rp.is_active = false;
                _scifferContext.SaveChanges();
            }
            catch(Exception ex)
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

        public ref_posting_periods_vm Get(int id)
        {
            ref_posting_periods posting = _scifferContext.ref_posting_periods.FirstOrDefault(c => c.posting_periods_id == id);
            Mapper.CreateMap<ref_posting_periods, ref_posting_periods_vm>();
            ref_posting_periods_vm postingvm = Mapper.Map<ref_posting_periods, ref_posting_periods_vm>(posting);
            return postingvm;
        }

        public List<posting_periods> GetPostingPeriods()
        {
            var query = (from p in _scifferContext.ref_posting_periods.Where(x => x.is_active == true)
                         join f in _scifferContext.ref_frequency on p.frequency_id equals f.frequency_id
                         join fi in _scifferContext.REF_FINANCIAL_YEAR on p.financial_year_id equals fi.FINANCIAL_YEAR_ID
                         select new posting_periods {
                            financial_year=fi.FINANCIAL_YEAR_NAME,
                            financial_year_id=p.financial_year_id,
                            frequency=f.frequency_name,
                            frequency_id=f.frequency_id,
                            no_of_periods=p.no_of_periods,
                            posting_periods_id=p.posting_periods_id,
                         }).OrderByDescending(a => a.posting_periods_id).ToList();
            return query;
        }
        
        public bool Update(ref_posting_periods_vm Posting)
        {
            try
            {
                string[] emptyStringArray = new string[0];
                try
                {
                    emptyStringArray = Posting.codelist.Split(new string[] { "~" }, StringSplitOptions.None);

                }
                catch
                {

                }
                ref_posting_periods rp = new ref_posting_periods();
                rp.financial_year_id = Posting.financial_year_id;
                rp.frequency_id = Posting.frequency_id;
                rp.is_active = true;
                rp.no_of_periods = Posting.no_of_periods;
                rp.posting_periods_id = Posting.posting_periods_id;
               // rp.company_id = Convert.ToInt32(HttpContext.Current.Session["Comp"].ToString());
                List<ref_posting_periods_detail> FRM = _scifferContext.ref_posting_periods_detail.Where(c => c.posting_periods_id == Posting.posting_periods_id).ToList();
                foreach (var i in FRM)
                {
                    _scifferContext.Entry(i).State = EntityState.Deleted;
                }
                List<ref_posting_periods_detail> rpd = new List<ref_posting_periods_detail>();
                for (int i = 0; i < emptyStringArray.Count() - 1; i++)
                {
                    ref_posting_periods_detail pd = new ref_posting_periods_detail();
                    pd.posting_periods_id = Posting.posting_periods_id;
                    pd.posting_periods_code = emptyStringArray[i].Split(new char[] { ',' })[1];
                    pd.posting_periods_detail_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[0]);
                    rpd.Add(pd);
                }
                foreach(var i in rpd)
                {
                    if(i.posting_periods_detail_id!=0)
                    {
                        _scifferContext.Entry(i).State = EntityState.Modified;
                    }
                    else
                    {
                        _scifferContext.Entry(i).State = EntityState.Added;
                    }
                }
                _scifferContext.Entry(rp).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        //public List<posting_periods> GetPostingPeriodsForEdit()
        //{
        //    var query = (from p in _scifferContext.ref_posting_periods                         
        //                 join pp in _scifferContext.ref_posting_periods_detail on p.posting_periods_id equals pp.posting_periods_id
        //                 join s in _scifferContext.ref_status on pp.status_id equals s.status_id
        //                 select new posting_periods {
        //                      financial_year_id=p.financial_year_id,
        //                      from_date=pp.from_date,
        //                      to_date=pp.to_date,
        //                      status_id=s.status_id,
        //                      status_name=s.status_name,
        //                      posting_periods_code=pp.posting_periods_code,
        //                 }).ToList();
        //    throw new NotImplementedException();
        //}

        public List<posting_periods> GetPostingPeriodsForEdit(int id)
        {
            var query = (from p in _scifferContext.ref_posting_periods_detail where(p.posting_periods_id==id)
                         join pp in _scifferContext.ref_posting_periods on p.posting_periods_id equals pp.posting_periods_id
                         join s in _scifferContext.ref_status on p.status_id equals s.status_id
                         join f in _scifferContext.REF_FINANCIAL_YEAR on pp.financial_year_id equals f.FINANCIAL_YEAR_ID
                         join freq in _scifferContext.ref_frequency on pp.frequency_id equals freq.frequency_id
                         select new posting_periods
                         {
                             financial_year_id = pp.financial_year_id,
                             financial_year = f.FINANCIAL_YEAR_NAME,
                             from_date = p.from_date,
                             frequency_id = pp.frequency_id,
                             frequency = freq.frequency_name,
                             to_date = p.to_date,
                             status_id = s.status_id,
                             status_name = s.status_name,
                             posting_periods_code = p.posting_periods_code,
                             no_of_periods = pp.no_of_periods,
                             posting_periods_id = p.posting_periods_id,
                             posting_periods_detail_id = p.posting_periods_detail_id,
                         }).ToList();
            return query;
        }

        public bool ChangePostingStatus(posting_periods vm)
        {
            try
            {
                var data = _scifferContext.ref_posting_periods_detail.Where(x => x.posting_periods_detail_id == vm.posting_periods_detail_id).FirstOrDefault();
                data.status_id = vm.status_id;
                _scifferContext.Entry(data).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
