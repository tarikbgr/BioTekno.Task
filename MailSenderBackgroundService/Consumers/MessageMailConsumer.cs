using MailSenderBackgroundService.Business;
using MassTransit;
using SendMail;
using Serilog;


namespace MailSenderBackgroundService.Consumers
{
    public class MessageMailConsumer : IConsumer<ISendMail>
    {
        private readonly ILogger _logger;
        private readonly IMailSendBusiness _mailSendBusiness;

        public MessageMailConsumer(ILogger logger, IMailSendBusiness mailSendBusiness)
        {
            _logger = logger;
            _mailSendBusiness = mailSendBusiness;
        }
        public async Task Consume(ConsumeContext<ISendMail> context)
        {
            var msg = context.Message;

           _logger.Information($"{msg.OrderId} Id'li sipariş oluşturuldu, {msg.CustomerEmail} adresine bilgilendirme maili gönderiliyor...");
           
            await _mailSendBusiness.SendMail(msg.OrderId,msg.CustomerEmail);
            
            await Task.CompletedTask;
        }
    }
}