using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace GDS.Gateway.Models.Io
{
    public class DataResponse
    {

        [JsonProperty("schema")]
        public List<SchemaField> Schema { get; set; }

        [JsonProperty("rows")]
        public List<DataRow> Rows { get; set; }

        [JsonProperty("filtersApplied")]
        public bool FiltersApplied {get;set;}

    }
    public class DataRow {
        [JsonProperty("values")]
        public string[] Values { get; set; }

    }
}
/*
   {
    "schema": [
      {
        object(Field)
      }
    ],
    "rows": [
      {
        "values": [
          string
        ]
      }
    ]
  }
     
     */
