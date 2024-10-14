using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Shafeh.Services;

public class SendGridEmailSender : IEmailSender
{
    private readonly IConfiguration configuration;
    private readonly ILogger logger;

    public SendGridEmailSender(IConfiguration configuration, ILogger<SendGridEmailSender> logger)
    {
        this.configuration = configuration;
        this.logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        logger.LogInformation(@$"
        
        {message}
        
        ");

        string sendGridApiKey = configuration["ExternalProviders:SendGrid:ApiKey"];
        if (string.IsNullOrEmpty(sendGridApiKey))
        {
            throw new Exception("The 'SendGridApiKey' is not configured");
        }
        var client = new SendGridClient(sendGridApiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress("sborisute@shafeh.org", "Shafeh Rockland"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(toEmail));

        var response = await client.SendEmailAsync(msg);
        if (response.IsSuccessStatusCode)
        {
            logger.LogInformation("Email queued successfully");
        }
        else
        {
            logger.LogError("Failed to send email");
            logger.LogInformation(toEmail);
            logger.LogInformation(msg.Subject);
            logger.LogInformation(msg.PlainTextContent);
        }
    }
}