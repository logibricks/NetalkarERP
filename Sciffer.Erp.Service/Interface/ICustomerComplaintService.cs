using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface ICustomerComplaintService :IDisposable
    {
        ref_customer_complaint Get(int id);
        List<ref_customer_complaint> GetAll();
        string Add(ref_customer_complaint cc);
        string Update(ref_customer_complaint cc);
    }
}
