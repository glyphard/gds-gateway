using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDS.Gateway.Models.Io
{
    public class SchemaRequest
    {
        [JsonProperty("configParams")]
        public Dictionary<string,string> ConfigParams { get; set; }
    }

//    "{\"configParams\":{\"sqlserver\":\"srv1\",\"password\":\"pwd1\",\"username\":\"usr1\",\"database\":\"db1\"}}"

}
