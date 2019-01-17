using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Manager
{
    public class SendOrderAndCheckNeedManager
    {
        private SendOrderAndCheckNeedGateway _sendOrder = new SendOrderAndCheckNeedGateway();

        public bool SendOrder(SendOrder sendOrder)
        {

            if (_sendOrder.IsSendOrderExists(sendOrder))
            {
                return _sendOrder.UpdateSendOrder(sendOrder) > 0;
            }
            else
            {
                return _sendOrder.SendOrder(sendOrder) > 0;
            }

           
        }

        public List<CheckOrder> CheckNeed()
        {
            return _sendOrder.CheckNeed();
        }

        public List<CheckOrder> GetDepartmentRequest()
        {
            return _sendOrder.GetDepartmentRequest();
        }
        public int ConfirmCheckNeed(int id)
        {
            return _sendOrder.ConfirmCheckNeed(id);
        }

        public int ConfirmCheckNeed1(int id)
        {
            return _sendOrder.ConfirmCheckNeed1(id);
        }
        public int RejectCheckNeed(int id)
        {
            return _sendOrder.RejectCheckNeed(id);
        }

        public int RejectCheckNeed1(int id)
        {
            return _sendOrder.RejectCheckNeed1(id);
        }

        public List<ShowNeed> GetTodayNeed()
        {
            
            return _sendOrder.GetTotalUser();
        }

        public List<Employee> AutoSearchDepartment(string name)
        {
            return _sendOrder.AutoSearchDepartment(name);
        }

        public string SendOrderSave(int type, string date, string name, int breakfast, int lunch, int dinner, int sehuri, int snacks, int tea, int quantity, string menu, int menu1)
        {
            int result = _sendOrder.SendOrderSave(type, date, name, breakfast, lunch, dinner, sehuri, snacks, tea, quantity, menu, menu1);
            return null;
        }
        public string DepartmentSendOrderSave(int type, string date, int breakfast, int lunch, int dinner, int sehuri, int snacks, int tea, string Id, string name, int quantity, string menu, int menu1)
        {
            int result = _sendOrder.DepartmentSendOrderSave(type, date, breakfast, lunch, dinner, sehuri, snacks, tea, Id, name, quantity, menu, menu1);
            return null;
        }

        public List<SubmitedApplicationInfo> GetUserEmail(int? id)
        {
            return _sendOrder.GetUserEmail(id);
        }

        public List<SubmitedApplicationInfo> GetUserEmail1(int? id)
        {
            return _sendOrder.GetUserEmail1(id);
        }

        public bool SendEmail(string toEmail, string subject, string emailBody)
        {

            try
            {
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, emailBody);

                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}