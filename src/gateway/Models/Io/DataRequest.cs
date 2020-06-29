using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using GDS.Gateway.Models.Domain;
using Newtonsoft.Json.Converters;

namespace GDS.Gateway.Models.Io
{

    public class ScriptParams
    { 
    
        [JsonProperty("sampleExtraction")]
        public bool SampleExtraction { get; set; }
        [JsonProperty("lastRefresh")]
        public string LastRefresh { get; set; }

    }
    public class DateRange
    { 
        [JsonProperty("startDate")]
        public string StartDate { get; set; }
        [JsonProperty("endDate")]
        public string EndDate { get; set; }

    }
    public class Field {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("forFilterOnly")]
        public bool ForFilterOnly { get; set; }
        
    }

    public class DimensionsFilter
    {

        [JsonProperty("fieldName")]
        public string FieldName { get; set; }
        [JsonProperty("values")]
        public List<string> Values { get; set; }
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public DimensionFilterType FilterType { get; set; } // "INCLUDE" | "EXCLUDE"

        [JsonProperty("operator")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FilterOperators FilterOperator { get; set; }

    }

    public class DataRequest
    {
        [JsonProperty("configParams")]
        public Dictionary<string, string> ConfigParams { get; set; }

        [JsonProperty("scriptParams")]
        public ScriptParams ScritParam { get; set; }

        [JsonProperty("dateRange")]
        public DateRange DateRange { get; set; }

        [JsonProperty("fields")]
        public List<Field> Fields { get; set; }

        [JsonProperty("dimensionsFilters")]
        public List<List<DimensionsFilter>> DimensionsFilters { get; set; } // A nested array of the user selected filters. The innermost arrays should be ORed together, the outermost arrays should be ANDed together.

    }


 /*
 Operator
EQUALS
CONTAINS
REGEXP_PARTIAL_MATCH
REGEXP_EXACT_MATCH
IN_LIST
IS_NULL
BETWEEN
NUMERIC_GREATER_THAN
NUMERIC_GREATER_THAN_OR_EQUAL
NUMERIC_LESS_THAN
NUMERIC_LESS_THAN_OR_EQUAL    
 */

    /*

        {
          "configParams": object,
          "scriptParams": {
            "sampleExtraction": boolean,
            "lastRefresh": string
          },
          "dateRange": {
            "startDate": string,
            "endDate": string
          },
          "fields": [
            {
              "name": string
            }
          ],
          "dimensionsFilters": [
            [{
              "fieldName": string,
              "values": string[],
              "type": DimensionsFilterType,
              "operator": Operator
            }]
          ]
        }
     
     */
}
