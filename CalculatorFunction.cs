using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CalculatorFunc.Dtos;
using System.Text.Json;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http.Features;
using CalculatorFunc.Helpers;

namespace CalculatorFunc
{
    public static class CalculatorFunction
    {
        [FunctionName("CalculatorFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                int x = int.Parse(req.Query["x"]);
                int y = int.Parse(req.Query["y"]);
                int sum = x + y;
                string date = DateTime.Now.ToString();
                string ip = HttpContextHelper.GetClientIPAddress(req.HttpContext);

                var str = Environment.GetEnvironmentVariable("sqldb_connection");
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    var text = $"INSERT INTO Calcution(X, Y, Sum, Date, IP) VALUES({x}, {y}, {sum}, '{date}', '{ip}')";

                    using SqlCommand cmd = new SqlCommand(text, conn);
                    var rows = await cmd.ExecuteNonQueryAsync();
                    log.LogInformation($"{rows} row was added");
                }

                var responseDto = new CalculatorDto(sum, date, ip);

                return new OkObjectResult(JsonSerializer.Serialize(responseDto));
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message, ex);
            }
            return new OkObjectResult(JsonSerializer.Serialize(new CalculatorDto()));
        }


    }
}
