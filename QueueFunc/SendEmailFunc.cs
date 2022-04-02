using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace QueueFunc
{
    public static class SendEmailFunc
    {
        [FunctionName("SendEmailFunc")]
        [return: SendGrid(ApiKey = "SendGridApiKey")]
        public static SendGridMessage Run([QueueTrigger("test-queue", Connection = "Connection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"SendEmailTimer executed at: {DateTime.Now}");

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("gamingmega5@gmail.com", "rainbow"),
                Subject = "Message was added to Azure Queue",
                PlainTextContent = myQueueItem,
            };

            msg.AddTo(new EmailAddress("dezmond655@gmail.com", "rainbow-rapiy"));

            return msg;
        }
    }
}
