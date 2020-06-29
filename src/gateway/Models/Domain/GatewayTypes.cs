using GDS.Gateway.Constants;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GDS.Gateway.Models.Domain
{
    [Flags]
    public enum GatewayTypes
    {
        // [JsonConverter(typeof(StringEnumConverter))]
        // [EnumMember(Value = "Action Movie")]



        [EnumMember(Value = "GatewayType")]
        GatewayType = 0,

        [EnumMember(Value = "Blob")]
        Blob = 1,

        [EnumMember(Value = "SQL")]
        SQL = 2


}
}
