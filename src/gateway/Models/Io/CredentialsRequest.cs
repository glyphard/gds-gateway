using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDS.Gateway.Models.Io
{
    public class CredentialsRequest
    {

        [JsonProperty("key")]
        public string Key { get; set; }

    }
}
