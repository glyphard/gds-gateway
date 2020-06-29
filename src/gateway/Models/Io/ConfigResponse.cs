using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using GDS.Gateway.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDS.Gateway.Models.Io
{
    public class ConfigResponse
    {

        [JsonProperty("configParams")]
        public List<ConfigParam> ConfigParams { get; set; }

        [JsonProperty("isSteppedConfig")]
        public bool IsSteppedConfig { get; set; }
        [JsonProperty("dateRangeRequired")]
        public bool DateRangeRequired { get; set; }
    }

    public class ConfigParam
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ConfigElementType Type { get; set; }
        /*
            TEXTINPUT	The input element will be a single-line text box.
            TEXTAREA	The input element will be a multi-line textarea box.
            SELECT_SINGLE	The input element will be a dropdown for single-select options.
            SELECT_MULTIPLE	The input element will be a dropdown for multi-select options.
            CHECKBOX	The input element will be a single checkbox that can be used to capture boolean values.
            INFO    This is a static plain-text box that can be used to provide instructions or information to the user. Any links will be clickable.
        */

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("helpText")]
        public string HelpText { get; set; }

        [JsonProperty("placeholder")]
        public string Placeholder { get; set; }


        [JsonProperty("isDynamic")]
        public bool IsDynamic { get; set; }

        [JsonProperty("parameterControl")]
        public ParameterControl ParameterControl { get; set; }
        [JsonProperty("options")]
        public List<ConfigOption> ConfigOptions { get; set; }
    }
    public class ConfigOption
    {

        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
    public class ParameterControl
    {
        [JsonProperty("allowOverride")]
        public bool AllowOverride { get; set; }
    }
}

