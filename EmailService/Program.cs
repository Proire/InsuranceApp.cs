using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit.Text;
using MimeKit;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Text;
using MailKit.Net.Smtp;

internal class Program
{
    private static void Main(string[] args)
    {
        // Representing configuration settings
        var config = AppConfiguration();
        RabbitConsumer(config);   
    }

    private static IConfiguration AppConfiguration()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        return configuration.Build();
    }

    private static void RabbitConsumer(IConfiguration config)
    {
        var factory = new ConnectionFactory { HostName = config["RabbitMQ:HostName"] };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: config["RabbitMQ:QueueName"],
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        Console.WriteLine(" [*] Waiting for messages.");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Note Received: {message}");
            var emailDto = ParseMessage(message);
            SendEmail(emailDto.To, emailDto.Subject, emailDto.Body, config);
        };

        channel.BasicConsume(queue: config["RabbitMQ:QueueName"],
                             autoAck: true,
                             consumer: consumer);

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }

    private static EmailDTO ParseMessage(string message)
    {
        // Assuming the message is a JSON string that contains email details.
        // Deserialize the message to EmailDTO object.
        return System.Text.Json.JsonSerializer.Deserialize<EmailDTO>(message);
    }

    private static void SendEmail(string receiver, string subject, string body, IConfiguration config)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(config["Email:From"]));
        email.To.Add(MailboxAddress.Parse(receiver));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = body };

        using var smtp = new SmtpClient();
        smtp.Connect(config["Email:SmtpHost"], int.Parse(config["Email:SmtpPort"]), SecureSocketOptions.StartTls);
        smtp.Authenticate(config["Email:SmtpUser"], Environment.GetEnvironmentVariable("MailPassword"));
        smtp.Send(email);
        smtp.Disconnect(true);

        Console.WriteLine($" [x] Email sent to {receiver}");
    }
}

public class EmailDTO
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
