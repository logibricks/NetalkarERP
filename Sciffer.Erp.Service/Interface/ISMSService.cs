namespace Sciffer.Erp.Service.Interface
{
    public interface ISMSService
    {
        string sendSMS(string number, string message);
    }
}
