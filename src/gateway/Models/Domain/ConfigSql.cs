using GDS.Gateway.Constants;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GDS.Gateway.Models.Domain
{
    [Flags]
    public enum ConfigSql
    {
        [EnumMember(Value = "ConfigSql")]
        ConfigSql = 0,

        [EnumMember(Value = "SqlConnStr")]
        SqlConnStr = 1,
        [EnumMember(Value = "SqlHost")]
        SqlHost = 2,
        [EnumMember(Value = "SqlDatabase")]
        SqlDatabase = 4,
        [EnumMember(Value = "SqlUsername")]
        SqlUsername = 8,
        [EnumMember(Value = "SqlPassword")]
        SqlPassword = 16,
        [EnumMember(Value = "SqlTable")]
        SqlTable = 32
}
}
