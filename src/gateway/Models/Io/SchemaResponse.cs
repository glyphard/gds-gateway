using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using GDS.Gateway.Models.Domain;
using Newtonsoft.Json.Converters;

namespace GDS.Gateway.Models.Io
{
    public class SchemaResponse
    {
        [JsonProperty("schema")]
        public List<SchemaField> Fields { get; set; } 
    }
    public class SchemaField
    {
        [JsonProperty("name")]
        public string FieldName { get; set; } // "Created",
        [JsonProperty("label")]
        public string FieldLabel { get; set; } // "Date Created",
        [JsonProperty("description")]
        public string Description { get; set; } // "description": "The date that this was created",
        [JsonProperty("dataType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public DataType? DataType { get; set; } // "description": "The date that this was created",

        [JsonProperty("group")]
        public string Group { get; set; } // "description": "The date that this was created",

        [JsonProperty("isDefault")]
        public bool? IsDefault { get; set; } // "description": "The date that this was created",
        [JsonProperty("defaultAggregationType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AggregationTypes? DefaultAggregationType { get; set; } //"NONE"

        [JsonProperty("semantics")]

        public FieldSemantics Semantics { get; set; } // "description": "The date that this was created",

        

    }

    public class FieldSemantics
    {

        [JsonProperty("conceptType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ConceptType ConceptType { get; set; } //"DIMENSION"
        [JsonProperty("semanticGroup")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SemanticGroups? SemanticGroup { get; set; } //: "DATE_AND_TIME")]
        [JsonProperty("semanticType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SemanticTypes SemanticType { get; set; } //: "YEAR_MONTH_DAY")]
        [JsonProperty("isReaggregatable")] //: false
        public bool IsReaggregatable { get; set; }
    }

    /*
     {
    "schema": [
      {
        "name": "Created",
        "label": "Date Created",
        "description": "The date that this was created",
        "dataType": "STRING",
        "group": "Date",
        "isDefault": true,
        "semantics": {
          "conceptType": "DIMENSION",
          "semanticGroup": "DATE_AND_TIME",
          "semanticType": "YEAR_MONTH_DAY",
          "isReaggregatable": false
        }
      },

    AggregationTypes:
    AVG	Enum	Average.
COUNT	Enum	Count.
COUNT_DISTINCT	Enum	Count Distinct.
MAX	Enum	Max.
MIN	Enum	Min.
SUM	Enum	Sum.
AUTO

    FieldTypes:
    ----
    YEAR	Enum	Year in the format of YYYY such as 2017.
YEAR_QUARTER	Enum	Year and quarter in the format of YYYYQ such as 20171.
YEAR_MONTH	Enum	Year and month in the format of YYYYMM such as 201703.
YEAR_WEEK	Enum	Year and week in the format of YYYYww such as 201707.
YEAR_MONTH_DAY	Enum	Year, month, and day in the format of YYYYMMDD such as 20170317.
YEAR_MONTH_DAY_HOUR	Enum	Year, month, day, and hour in the format of YYYYMMDDHH such as 2017031703.
YEAR_MONTH_DAY_SECOND	Enum	Year, month, day, hour, and second in the format of YYYYMMDDHHss such as 201703170330.
QUARTER	Enum	Quarter in the format of 1, 2, 3, or 4).
MONTH	Enum	Month in the format of MM such as 03.
WEEK	Enum	Week in the format of ww such as 07.
MONTH_DAY	Enum	Month and day in the format of MMDD such as 0317.
DAY_OF_WEEK	Enum	A number in the range of [0,6] with 0 representing Sunday.
DAY	Enum	Day in the format of DD such as 17.
HOUR	Enum	Hour in the format of HH such as 13.
MINUTE	Enum	Minute in the format of mm such as 12.
DURATION	Enum	A duration of time in seconds.
COUNTRY	Enum	A country such as United States.
COUNTRY_CODE	Enum	A country code such as US.
CONTINENT	Enum	A continent such as Americas.
CONTINENT_CODE	Enum	A continent code such as 019.
SUB_CONTINENT	Enum	A sub-continent such as North America.
SUB_CONTINENT_CODE	Enum	A sub-continent code such as 003.
REGION	Enum	A region such as California.
REGION_CODE	Enum	A region code such as CA.
CITY	Enum	A city such as Mountain View.
CITY_CODE	Enum	A city code such as 1014044.
METRO	Enum	A metro such as San Francisco-Oakland-San Jose CA.
METRO_CODE	Enum	A metro code such as 200807.
LATITUDE_LONGITUDE	Enum	A latitude longitude pair such as 51.5074, -0.1278.
NUMBER	Enum	A decimal number.
PERCENT	Enum	Decimal percentage (can be over 1.0). For example, 137% is represented as 1.37.
TEXT	Enum	Free-form text.
BOOLEAN	Enum	A true or false boolean value.
URL	Enum	A URL as text such as https://google.com.
HYPERLINK	Enum	A hyperlink. Only use this for calculated fields with the HYPERLINK function.
IMAGE	Enum	An image. Only use this for calculated fields with the IMAGE function.
IMAGE_LINK	Enum	An image link. Only use this for calculated fields with the HYPERLINK function while using IMAGE for the link label.
CURRENCY_AED	Enum	Currency from AED.
CURRENCY_ALL	Enum	Currency from ALL.

     */
}
