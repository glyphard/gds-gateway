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
using System.Collections.Generic;
using GDS.Gateway.Models.Domain;
using GDS.Gateway.Models.Io;

namespace GDS.Gateway
{
    public  class GatewayAuth
    {
        public GatewayAuth() { }

        [FunctionName("v202004Auth")]
        public  async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "v202004/Auth")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"Auth Triggered:{req.Query}");
            var authActionType = ActionTypes.AuthActionType;
            string authActionVal = req.Query[ActionTypes.AuthActionType.ToString()];
            Enum.TryParse<ActionTypes>(authActionVal, true, out authActionType);
            var licenseKey = req.Query[Lookups.LicenseKey];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            switch (authActionType) {

                case ActionTypes.IsAuthValid:
                case ActionTypes.SetCredentials:
                    var credsObj = JsonConvert.DeserializeObject<CredentialsRequest>(requestBody);
                    var authResult = false;// _licensingClient.IsLicenseKeyValid(credsObj?.Key ?? licenseKey);
                    var dbkey = Lookups.GetEnvironmentVariable($"dbkey_{licenseKey}");
                    if (!string.IsNullOrEmpty(dbkey)) {
                        authResult = true;
                    }
                    return new OkObjectResult(authResult);
                    break;
                case ActionTypes.ResetAuth:
                    break;
                default:
                    break;
            }

            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var name = string.Empty;

            var responseMessage = string.Empty;
            return new OkObjectResult(responseMessage);
        }
    }
}
