using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
//using System.Net.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Options;
using QPR_Application.Models.DTO.Utility;
using Humanizer;
using static System.Net.WebRequestMethods;

namespace SMS_EMAIL_Models
{
    public class SMS_Mail_OTP
    {
        private readonly IConfiguration _configuration;
        private static string _SmsApiUrl;
        private static string _username;
        private static string _pin;
        private static string _signature;
        private static string _dlt_entity_id;
        private static string _dlt_template_id;
        private readonly SmtpSettings _smtpSettings;
        private readonly ILogger<SMS_Mail_OTP> _logger;

        public SMS_Mail_OTP(ILogger<SMS_Mail_OTP> logger,IConfiguration configuration, IOptions<SmtpSettings> smtpSettings)
        {
            _logger = logger;
            _configuration = configuration;
            _smtpSettings = smtpSettings.Value;
            if (_configuration is not null)
            {
                _SmsApiUrl = _configuration["SmsApiUrl"];
                _username = _configuration["SmsUsername"];
                _pin = _configuration["pin"];
                _signature = _configuration["signature"];
                _dlt_entity_id = _configuration["dlt_entity_id"];
                _dlt_template_id = _configuration["dlt_template_id"];
            }
        }

        public string SendSmsNotification(string mobile, string OTP)
        {
            string returnCode = "";
            try
            {
                //string urlString = " https://smsgw.sms.gov.in/failsafe/HttpLink?username=CVC&pin=" + pin + "&message=" + sms + "&mnumber=" + mobNumber + "&signature=" + sign + "&dlt_entity_id=1001640290000010121&dlt_template_id=" + templateId;

                //string urlString = "http://control.msg91.com/sendhttp.php?user=ntpcit&password=Ntpc@432&mobiles=" + mobile + "&sender=NTPCIT&ignoreNdnc=1&route=4&DLT_TE_ID= 1307161504779804963&message= Dear Sir / Madam Message from: NTPC Equipment History Portal Message:OTP :" + OTP + "";
                // ""+SmsApiUrl+"?user=ntpcit&password=Ntpc@432&mobiles=" + mobile + "&sender=NTPCIT&ignoreNdnc=1&route=4&DLT_TE_ID= 1307161504779804963&message= Dear Sir / Madam Message from: NTPC Equipment History Portal Message:OTP :" + OTP + "";
                string urlString = " " + _SmsApiUrl + "?username=" + _username + "&pin=" + _pin + "&message=" + OTP + " is your OTP for CVC VCMS Portal. This OTP will be valid for the next 5 mins. OTP is generated on " + DateTime.Now + ". - CVC&mnumber=" + mobile + "&signature=" + _signature + "&dlt_entity_id=" + _dlt_entity_id + "&dlt_template_id=" + _dlt_template_id + "";

                //string urlString = "http://control.msg91.com/sendhttp.php?user=ntpcit&password=Ntpc@432&mobiles=" + mobile + "&sender=CVC&ignoreNdnc=1&route=4&DLT_TE_ID= 1307161504779804963&message= "+OTP+" is your OTP for https://portal.cvc.gov.in. This OTP will be valid for the next 5 mins. OTP is generated on 11:21:05 am. - CVC :";


                //462022 is your OTP for https://portal.cvc.gov.in. This OTP will be valid for the next 5 mins. OTP is generated on 11:21:05 am. - CVC
                //------------------
                // string urlString = "http://control.msg91.com/sendhttp.php?user=ntpcit&password=Ntpc@432&mobiles=" + mobile + "&sender=NTPCIT&ignoreNdnc=1&route=4&DLT_TE_ID= 1307161504779804963&message= Jadugarni:OTP :" + OTP + "";

                // I google it and find this 3lines as being helpful - THEY ARE NOT HELPING AT ALL
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.DefaultConnectionLimit = 9999;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                //(In my VS2010, I only see Ssl3 and Tls,that's it - this may be the cause?)

                HttpWebRequest request;
                HttpWebResponse response = null;
                System.IO.Stream stream = null;
                //Message from: NTPC {#var#} Message To:{#var#} Message:{#var#}{#var#}{#var#}  NTPC Ltd.
                //request = (HttpWebRequest)WebRequest.Create("http://control.msg91.com/sendhttp.php?user=ntpcit&password=Ntpc@432&mobiles=" + mobile + "&sender=NTPCIT&ignoreNdnc=1&route=4&DLT_TE_ID= 1307161504779804963&message= Dear Sir / Madam Message from: NTPC Equipment History Portal Message:" + SmsMessage + "");
                request = (HttpWebRequest)WebRequest.Create(urlString);
                request.UserAgent = "Foo";
                request.Accept = "*/*";

                request.UseDefaultCredentials = true;
                request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

                //{"The request was aborted: Could not create SSL/TLS secure channel."}
                //and response is null.
                response = (HttpWebResponse)request.GetResponse();//<<<here it breakes       
                stream = response.GetResponseStream();

                if (stream != null) stream.Close();
                if (response != null) response.Close();


                //HttpContext.Current.Session["OTP"] = OTP;
                //Generate procedure to save this OTP generation operation after this
                returnCode = "success" + "|" + OTP;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sending SMS notification failed");
                returnCode = "failed" + "|" + OTP;
            }

            return returnCode;
        }
        public string Send_Email(string mailID, string OTP)
        {
            string returnCode;
            try
            {
                using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
                {
                    Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                    EnableSsl = _smtpSettings.EnableSsl
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.Username),
                    Subject = "Login OTP for VCMS",
                    Body = "Dear User,\n\nLogin OTP for VCMS is :" + OTP + ".\nThis code is valid for 20 mins.Please do not share it.\n\nRegards,\nCVC - VCMS",
                    IsBodyHtml = false
                };

                mailMessage.To.Add(mailID);

                client.Send(mailMessage);
                returnCode = "success" + "|" + OTP;


                //SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                //using (System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(smtpSection.From, mailID))
                //{
                //    mm.Subject = "Login OTP for VCMS";

                //    mm.Body = "Dear User,\n\nLogin OTP for VCMS is :" + OTP + ".\nThis code is valid for 20 mins.Please do not share it.\n\nRegards,\nCVC - VCMS";
                //    mm.IsBodyHtml = false;
                //    SmtpClient smtp = new SmtpClient();

                //    smtp.Host = smtpSection.Network.Host;
                //    smtp.EnableSsl = smtpSection.Network.EnableSsl;
                //    NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                //    smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                //    smtp.Credentials = networkCred;
                //    smtp.EnableSsl = true;
                //    smtp.Port = smtpSection.Network.Port;
                //    smtp.Send(mm);
                //    returnCode = "success" + "|" + OTP;
                //    //SendOtpViaSms("+918859460310", "76586");
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sending Email notification failed");
                returnCode = "failed" + "|" + OTP;
            }
            return returnCode;
        }
        public string Send_Email_For_New_Password(string mailID, string Password)
        {
            string returnCode;
            try
            {
                using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
                {
                    Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                    EnableSsl = _smtpSettings.EnableSsl
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.Username),
                    Subject = "New Login Password for VCMS",
                    Body = "Dear User,\n\nNew Login Password for VCMS is :" + Password + ".\nThis Password is Your Default Password.Kindly Change This After Successful Login.Please do not share it.\n\nRegards,\nCVC - VCMS",
                    IsBodyHtml = false
                };

                mailMessage.To.Add(mailID);

                client.Send(mailMessage);
                returnCode = "success" + "|" + Password;


                //SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                //using (System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(smtpSection.From, mailID))
                //{
                //    mm.Subject = "New Login Password for VCMS";

                //    mm.Body = "Dear User,\n\nNew Login Password for VCMS is :" + Password + ".\nThis Password is Your Default Password.Kindly Change This After Successful Login.Please do not share it.\n\nRegards,\nCVC - VCMS";
                //    mm.IsBodyHtml = false;
                //    SmtpClient smtp = new SmtpClient();

                //    smtp.Host = smtpSection.Network.Host;
                //    smtp.EnableSsl = smtpSection.Network.EnableSsl;
                //    NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                //    smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                //    smtp.Credentials = networkCred;
                //    smtp.EnableSsl = true;
                //    smtp.Port = smtpSection.Network.Port;
                //    smtp.Send(mm);
                //    returnCode = "success" + "|" + Password;
                //    //SendOtpViaSms("+918859460310", "76586");
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sending Email notification failed");
                returnCode = "failed" + "|" + Password;
            }
            return returnCode;
        }
    }
}
