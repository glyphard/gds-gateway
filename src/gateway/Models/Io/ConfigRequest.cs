using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDS.Gateway.Models.Io
{
    public class ConfigRequest
    {
        [JsonProperty("languageCode")]
        public string LanguageCode { get; set; }
        [JsonProperty("configParams")]
        public Dictionary<string, string> ConfigParams { get; set; }

    }
}
