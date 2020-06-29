using GDS.Gateway.Constants;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GDS.Gateway.Models.Domain
{
    [Flags]
    public enum ConfigSteps
    {
        [EnumMember(Value = "ChooseTable")]
        ChooseTable = 0,


        [EnumMember(Value = "ConfigComplete")]
        ConfigComplete = 1024,
    }
}
