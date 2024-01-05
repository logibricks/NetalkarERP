using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;
using System.IO;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class CompanyService : ICompanyService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;

        public CompanyService(ScifferContext scifferContext, IGenericService GenericService)
        {
            _scifferContext = scifferContext;
            _genericService = GenericService;
        }
        public bool Add(REF_COMPANY_VM COMPANY)
        {
            try
            {
                REF_COMPANY VM = new REF_COMPANY();
                ref_cost_center cost = new ref_cost_center();
                cost.cost_center_code = COMPANY.COMPANY_NAME;
                cost.cost_center_description= COMPANY.COMPANY_NAME;
                cost.cost_center_level = 0;
                cost.head_parent = 1;
                cost.is_active = true;
                cost.is_blocked = false;
                
                VM.COMPANY_DISPLAY_NAME = COMPANY.COMPANY_DISPLAY_NAME;
                VM.COMPANY_ID = COMPANY.COMPANY_ID;
                VM.COMPANY_NAME = COMPANY.COMPANY_NAME;
                VM.CORPORATE_ADDRESS = COMPANY.CORPORATE_ADDRESS;
                VM.CORPORATE_CITY = COMPANY.CORPORATE_CITY;
                VM.ORG_TYPE_ID = COMPANY.ORG_TYPE_ID;
                VM.CORPORATE_MOBILE = COMPANY.CORPORATE_MOBILE;
                VM.CORPORATE_STATE_ID = COMPANY.CORPORATE_STATE_ID;
                VM.CORPORATE_TELEPHONE = COMPANY.CORPORATE_TELEPHONE;                
                VM.REGISTERED_ADDRESS = COMPANY.REGISTERED_ADDRESS;
                VM.REGISTERED_CITY = COMPANY.REGISTERED_CITY;
                VM.REGISTERED_EMAIL = COMPANY.REGISTERED_EMAIL;
                VM.REGISTERED_MOBILE = COMPANY.REGISTERED_MOBILE;
                VM.REGISTERED_STATE_ID = COMPANY.REGISTERED_STATE_ID;
                VM.REGISTERED_TELEPHONE = COMPANY.REGISTERED_TELEPHONE;
                VM.corporate_telephone_code = COMPANY.corporate_telephone_code;
                VM.registered_telephone_code = COMPANY.registered_telephone_code;
                VM.WEBSITE = COMPANY.WEBSITE;
                VM.PAN_NO = COMPANY.PAN_NO;
                VM.CIN_NO = COMPANY.CIN_NO;
                VM.TAN_NO = COMPANY.TAN_NO;
                VM.registered_pincode = COMPANY.registered_pincode;
                VM.corporate_pincode = COMPANY.corporate_pincode;
                if (COMPANY.FileUpload != null)
                {
                    int contentLength = COMPANY.FileUpload.ContentLength;
                    byte[] bytePic = new byte[contentLength];
                    COMPANY.FileUpload.InputStream.Read(bytePic, 0, contentLength);
                    VM.LOGO = bytePic;
                }
                VM.CURRENCY_ID = COMPANY.CURRENCY_ID;
                VM.ALLOW_NEGATIVE_CASH = COMPANY.ALLOW_NEGATIVE_CASH;
                VM.ALLOW_NEGATIVE_INVENTORY = COMPANY.ALLOW_NEGATIVE_INVENTORY;
               
                _scifferContext.REF_COMPANY.Add(VM);
                _scifferContext.ref_cost_center.Add(cost);
                _scifferContext.SaveChanges();
            }
            catch(Exception EX)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
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

        public REF_COMPANY_VM Get(int id)
        {
            REF_COMPANY customer = _scifferContext.REF_COMPANY.FirstOrDefault(c => c.COMPANY_ID == id);
            Mapper.CreateMap<REF_COMPANY, REF_COMPANY_VM>().ForMember(dest => dest.FileUpload, opt => opt.Ignore()); ;
            REF_COMPANY_VM customervm = Mapper.Map<REF_COMPANY, REF_COMPANY_VM>(customer);
            var REGISTER = _scifferContext.REF_STATE.Where(s => s.STATE_ID == customervm.REGISTERED_STATE_ID)
             .Select(s => new
             {
                 COUNTRY_ID = s.COUNTRY_ID
             }).Single();
            
            var corrcountry = _scifferContext.REF_STATE.Where(s => s.STATE_ID == customervm.CORPORATE_STATE_ID)
            .Select(s => new
            {
                COUNTRY_ID = s.COUNTRY_ID
            }).Single();
            customervm.REGISTERED_COUNTRY_ID = REGISTER.COUNTRY_ID;
            customervm.CORPORATE_COUNTRY_ID = corrcountry.COUNTRY_ID;
            customervm.std_code1 = REGISTER.COUNTRY_ID;
            customervm.std_code2 = corrcountry.COUNTRY_ID;
           
            //  customervm.ATTRIBUTE_VM = attrbiuteVM;
            return customervm;
          
        }
     
        public List<REF_COMPANY_VM> GetAll()
        {
            Mapper.CreateMap<REF_COMPANY, REF_COMPANY_VM>().ForMember(dest => dest.FileUpload, opt => opt.Ignore()); 
            return _scifferContext.REF_COMPANY.Project().To<REF_COMPANY_VM>().ToList();
        }
        public List<comapnyvm> GetCompanyDetail()
        {
            var query = (from c in _scifferContext.REF_COMPANY
                         join s in _scifferContext.REF_STATE on c.REGISTERED_STATE_ID equals s.STATE_ID 
                         join c1 in _scifferContext.REF_COUNTRY on s.COUNTRY_ID equals c1.COUNTRY_ID
                         join s1 in _scifferContext.REF_STATE on c.CORPORATE_STATE_ID equals s1.STATE_ID
                         join c2 in _scifferContext.REF_COUNTRY on s1.COUNTRY_ID equals c2.COUNTRY_ID
                         join cu in _scifferContext.REF_CURRENCY on c.CURRENCY_ID equals cu.CURRENCY_ID
                         join o in _scifferContext.REF_ORG_TYPE on c.ORG_TYPE_ID equals o.ORG_TYPE_ID
                         select new comapnyvm
                         {
                             company_display_name = c.COMPANY_DISPLAY_NAME,
                             company_id = c.COMPANY_ID,
                             company_name = c.COMPANY_NAME,
                             corporate_address = c.CORPORATE_ADDRESS,
                             corporate_city = c.CORPORATE_ADDRESS,
                             corporate_country = c2.COUNTRY_NAME,
                             ORG_TYPE_NAME = o.ORG_TYPE_NAME,
                             corporate_mobile = c.CORPORATE_MOBILE,
                             corporate_state = s1.STATE_NAME,
                             corporate_telephone = c.CORPORATE_TELEPHONE,
                             registered_address = c.REGISTERED_ADDRESS,
                             registered_city = c.REGISTERED_CITY,
                             registered_country = c1.COUNTRY_NAME,
                             registered_email = c.REGISTERED_EMAIL,
                             registered_mobile = c.REGISTERED_MOBILE,
                             registered_state = s.STATE_NAME,
                             registered_telephone = c.REGISTERED_TELEPHONE,
                             website = c.WEBSITE,
                             PAN_NO = c.PAN_NO,
                             CIN_NO = c.CIN_NO,
                             TAN_NO = c.TAN_NO,
                             currency = cu.CURRENCY_NAME,
                             corporate_pincode=c.corporate_pincode,
                             registered_pincode=c.registered_pincode,                            
                             ALLOW_NEGATIVE_CASH = c.ALLOW_NEGATIVE_CASH,
                             ALLOW_NEGATIVE_INVENTORY = c.ALLOW_NEGATIVE_INVENTORY,
                             registered_telephone_code = c.registered_telephone_code,
                             corporate_telephone_code = c.corporate_telephone_code,                         

                         }).OrderByDescending(a => a.company_id).ToList();
            return query;
        }
        public bool Update(REF_COMPANY_VM COMPANY)
        {
            try
            {
                REF_COMPANY VM = new REF_COMPANY();
                VM.COMPANY_DISPLAY_NAME = COMPANY.COMPANY_DISPLAY_NAME;
                VM.COMPANY_ID = COMPANY.COMPANY_ID;
                VM.COMPANY_NAME = COMPANY.COMPANY_NAME;
                VM.CORPORATE_ADDRESS = COMPANY.CORPORATE_ADDRESS;
                VM.CORPORATE_CITY = COMPANY.CORPORATE_CITY;
                VM.ORG_TYPE_ID = COMPANY.ORG_TYPE_ID;
                VM.CORPORATE_MOBILE = COMPANY.CORPORATE_MOBILE;
                VM.CORPORATE_STATE_ID = COMPANY.CORPORATE_STATE_ID;
                VM.CORPORATE_TELEPHONE = COMPANY.CORPORATE_TELEPHONE;
                VM.REGISTERED_ADDRESS = COMPANY.REGISTERED_ADDRESS;
                VM.REGISTERED_CITY = COMPANY.REGISTERED_CITY;
                VM.REGISTERED_EMAIL = COMPANY.REGISTERED_EMAIL;
                VM.REGISTERED_MOBILE = COMPANY.REGISTERED_MOBILE;
                VM.REGISTERED_STATE_ID = COMPANY.REGISTERED_STATE_ID;
                VM.REGISTERED_TELEPHONE = COMPANY.REGISTERED_TELEPHONE;
                VM.corporate_telephone_code = COMPANY.corporate_telephone_code;
                VM.registered_telephone_code = COMPANY.registered_telephone_code;
                VM.WEBSITE = COMPANY.WEBSITE;
                VM.PAN_NO = COMPANY.PAN_NO;
                VM.CIN_NO = COMPANY.CIN_NO;
                VM.TAN_NO = COMPANY.TAN_NO;
                VM.registered_pincode = COMPANY.registered_pincode;
                VM.corporate_pincode = COMPANY.corporate_pincode;
                if (COMPANY.FileUpload != null)
                {
                    int contentLength = COMPANY.FileUpload.ContentLength;
                    byte[] bytePic = new byte[contentLength];
                    COMPANY.FileUpload.InputStream.Read(bytePic, 0, contentLength);
                    VM.LOGO = bytePic;
                }
                VM.CURRENCY_ID = COMPANY.CURRENCY_ID;
                VM.ALLOW_NEGATIVE_CASH = COMPANY.ALLOW_NEGATIVE_CASH;
                VM.ALLOW_NEGATIVE_INVENTORY = COMPANY.ALLOW_NEGATIVE_INVENTORY;
               
              
                _scifferContext.Entry(VM).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
