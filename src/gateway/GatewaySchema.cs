using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using GDS.Gateway.Constants;
using GDS.Gateway.Models;
using GDS.Gateway.Adapters;
using System.Net.Http;
using GDS.Gateway.Models.Io;
using GDS.Gateway.Adapters.DataAccess;
using GDS.Gateway.Models.Domain;

namespace GDS.Gateway
{
    public  class GatewaySchema
    {


        private readonly IGatewayAdapterRouter _adapterRouter;
        private readonly HttpClient _client;

        public GatewaySchema(IGatewayAdapterRouter adapterRouter, HttpClient httpClient)
        {
            _adapterRouter = adapterRouter;
            _client = httpClient;
        }

        [FunctionName("v202004Schema")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "v202004/Schema")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"Schema Triggered:{req.Query}");
            var licenseKey = req.Query[Lookups.LicenseKey];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var schemaReq = JsonConvert.DeserializeObject<SchemaRequest>(requestBody);
            var errMsg = string.Empty;
            var dbkey = Lookups.GetEnvironmentVariable($"dbkey_{licenseKey}");

            var schemaResponse = default(SchemaResponse);
            try
            {
                var adapter = (SqlDataAdapter)_adapterRouter.GetGatewayAdapter(GatewayTypes.SQL.ToString());
                var tblschema = schemaReq.ConfigParams[ConfigSql.SqlTable.ToString()];
                var sqlTableSchema = adapter.ParseSchemaTable(tblschema);
                schemaResponse = await adapter.GetSchemaAsync(dbkey, sqlTableSchema.SchemaName, sqlTableSchema.TableName );
            }
            catch (ArgumentException argEx) {
                errMsg = argEx.Message;
            }

        var resp = (ActionResult)new OkObjectResult(            JsonConvert.SerializeObject(schemaResponse, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }
            ));
        return resp;
        
        }
    }
}
