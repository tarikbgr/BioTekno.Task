using System.Net;
using System.Net.Mail;
using Serilog;

namespace MailSenderBackgroundService.Business;

public interface IMailSendBusiness
{
    Task<bool> SendMail(int orderId, string customerEmail);
}

public class MailSendBusiness : IMailSendBusiness
{
    private readonly ILogger _logger;
    // private SmtpClient _client;
    // private MailMessage _mailMessage;

    public MailSendBusiness(ILogger logger /*, SmtpClient client, MailMessage mailMessage*/)
    {
        _logger = logger;
        // _client = client;
        // _mailMessage = mailMessage;
    }

    public async Task<bool> SendMail(int orderId, string customerEmail)
    {
        /*
        Eğer kullanmak istiyorsanız Program.cs de NetworkCredential kısmında Email ve şfirenizi giriniz bi alt satırdaki MailAddress bölümüne de Emailinizi giriniz
         _mailMessage.From = new MailAddress("Mailinizi girin...");
         _mailMessage.To.Add(customerEmail);
         _mailMessage.Subject = "Bilgilendirme";
         _mailMessage.Body = $"Siparişiniz başarıyla oluşturuldu. Sipariş numaranız: {orderId}";
         _client.Send(_mailMessage);
         */
        await Task.Delay(5000);
        _logger.Information($"{customerEmail} adresine {orderId} Id'li siparişin bilgilendirme maili gönderildi...");
        return true;
    }
}