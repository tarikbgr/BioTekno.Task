using System.Net;
using System.Net.Mail;
using MailSenderBackgroundService.Business;
using MailSenderBackgroundService.Consumers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SendMail;
using Serilog;

await Host.CreateDefaultBuilder()
    .ConfigureServices(service =>
    {
        // service.AddSingleton(new SmtpClient
        // {
        //     Host = "smtp.gmail.com",
        //     Port = 587,
        //     Credentials = new NetworkCredential("Mailinizi girin...", "Şifrenizi girin..."),
        //     EnableSsl = true
        // });
       // service.AddSingleton(new MailMessage());
        service.AddSingleton<IMailSendBusiness, MailSendBusiness>();
        service.AddMassTransit(mt =>
        {
            mt.AddConsumer<MessageMailConsumer>();
            mt.UsingRabbitMq((context, cfg) =>
            {
                //Hangi sunucuya baglanacıgının bilgileri bağlantı bilgileri
                cfg.Host(
                    "localhost",
                    5672,
                    "/",
                    "MailSenderBackgroundService",
                    h =>
                    {
                        h.Username("admin");
                        h.Password("123456");
                    }
                );
                //endpoint ayarları hangi kuyurugu dinleyip nereye göndereyim
                cfg.ReceiveEndpoint("SendMail", endpoint =>
                {
                    endpoint.PrefetchCount = 8; //aynıanda  kaç mesaj alıcağı
                    endpoint.ConfigureConsumer<MessageMailConsumer>(context);
                });
            });
        });
        
    }).UseSerilog((context, config) =>
    {
        config.WriteTo.Console();
        config.WriteTo.File("log.txt");
    })
    //.ConfigureLogging((hostContext, logging) => { logging.AddConsole(); })
    .Build()
    .RunAsync();