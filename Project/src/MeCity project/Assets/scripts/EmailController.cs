using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EmailController : MonoBehaviour
{
    public static EmailController instance;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    public void SendEmail(string type, string title, string description, string dateLogged)
    {
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

        mail.From = new MailAddress("dev.ferranti@gmail.com");
        mail.To.Add("dev.ferranti@gmail.com");
        mail.Subject = string.Format("[{0}] {1}: {2}", dateLogged, type.ToUpper(), title);
        mail.Body = description;

        SmtpServer.Port = 587;
        SmtpServer.Credentials = new NetworkCredential("dev.ferranti@gmail.com", "AeSh4rgE_2waEb%Y");
        SmtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };

        SmtpServer.Send(mail);
    }
}
