using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace CronFunc
{
    public static class RequestDataFunc
    {
        [FunctionName("RequestDataFunc")]
        public static async Task Run([TimerTrigger("0 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(new Uri("https://localhost:5001/api/dev/movies"));

            var movies = await response.Content.ReadAsStringAsync();

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
