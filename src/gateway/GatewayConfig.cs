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
using GDS.Gateway.Models.Io;
using GDS.Gateway.Adapters.DataAccess;
using System.Collections.Generic;
using GDS.Gateway.Models.Domain;
using System.Linq;

namespace GDS.Gateway
{
    public class GatewayConfig
    {
        private readonly IGatewayAdapterRouter _adapterRouter;

        public GatewayConfig(IGatewayAdapterRouter adapterRouter) {
            _adapterRouter = adapterRouter;
        }

        [FunctionName("v202004Config")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "v202004/Config")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"Config Triggered:{req.Query}");

            var licenseKey = req.Query[Lookups.LicenseKey];
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var configRequest = JsonConvert.DeserializeObject<ConfigRequest>(requestBody) ?? new ConfigRequest() { ConfigParams = new System.Collections.Generic.Dictionary<string, string>() };
            var dbkey = Lookups.GetEnvironmentVariable($"dbkey_{licenseKey}");

            var sqlAdapter = (SqlDataAdapter) _adapterRouter.GetGatewayAdapter(GatewayTypes.SQL.ToString());
            var tables = await sqlAdapter.GetTablesAsync(dbkey, string.Empty);


            var configResponse = new ConfigResponse()
            {
                IsSteppedConfig = false,
                DateRangeRequired = false,
                ConfigParams = new List<ConfigParam>() {
                    new ConfigParam(){
                Name= ConfigSql.SqlTable.ToString(),
                DisplayName  = "Table or View",
                HelpText ="Choose a view or table",
                Type = ConfigElementType.SELECT_SINGLE,
                IsDynamic = false,
                ConfigOptions = tables.Select(t => new ConfigOption(){
                    Label = $"[{t.SchemaName}].[{t.TableName}]",
                    Value = $"[{t.SchemaName}].[{t.TableName}]",
                }).ToList(),
                ParameterControl = new ParameterControl(){AllowOverride=false },
                Placeholder = ""
                }
                }
            };

            return new OkObjectResult(configResponse);
        }
    }
}
