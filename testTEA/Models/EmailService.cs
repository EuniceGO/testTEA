using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public void EnviarCorreo(string asunto, string mensaje)
    {
        var from = _config["EmailSettings:From"];
        var password = _config["EmailSettings:Password"];
        var smtp = _config["EmailSettings:SmtpServer"];
        var port = int.Parse(_config["EmailSettings:Port"]);
        var to = _config["EmailSettings:AdminEmail"];

        var mail = new MailMessage();
        mail.From = new MailAddress(from);
        mail.To.Add(to);
        mail.Subject = asunto;
        mail.Body = mensaje;

        var client = new SmtpClient(smtp, port)
        {
            Credentials = new NetworkCredential(from, password),
            EnableSsl = true
        };

        client.Send(mail);
    }
}
