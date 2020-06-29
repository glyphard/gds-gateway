using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using GDS.Gateway.Adapters;
using System.Net.Http;
using GDS.Gateway.Models;
using GDS.Gateway.Constants;
using GDS.Gateway.Models.Io;
using GDS.Gateway.Adapters.DataAccess;
using GDS.Gateway.Models.Domain;

namespace GDS.Gateway
{
    public class GatewayData
    {

        private readonly IGatewayAdapterRouter _adapterRouter;
        private readonly HttpClient _client;

        public GatewayData(IGatewayAdapterRouter adapterRouter, HttpClient httpClient)
        {
            _adapterRouter = adapterRouter;
            _client = httpClient;
        }

        [FunctionName("v202004Data")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "v202004/Data")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"Data Triggered:{req.Query}");
            var licenseKey = req.Query[Lookups.LicenseKey];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var dataReq = JsonConvert.DeserializeObject<DataRequest>(requestBody);
            var dbkey = Lookups.GetEnvironmentVariable($"dbkey_{licenseKey}");

            var errMsg = string.Empty;
            try
            {

                var adapter = (SqlDataAdapter)_adapterRouter.GetGatewayAdapter(GatewayTypes.SQL.ToString());

                var dataResponse = await adapter.GetDataAsync(dataReq, dbkey);
                var resp = (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(dataResponse, 
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }
                ));
                return resp;

            }
            catch (ArgumentException argEx)
            {
                errMsg = argEx.Message;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return (ActionResult)new BadRequestObjectResult(new
            {
                Message = errMsg
            });
        }
    }
}
