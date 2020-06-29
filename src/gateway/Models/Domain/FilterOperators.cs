using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GDS.Gateway.Models.Domain
{
    [Flags]
    public enum FilterOperators
    {
        [EnumMember(Value = "EQUALS")]
        EQUALS = 0,
        [EnumMember(Value = "CONTAINS")]
        CONTAINS = 1,
        [EnumMember(Value = "REGEXP_PARTIAL_MATCH")]
        REGEXP_PARTIAL_MATCH = 2,
        [EnumMember(Value = "REGEXP_EXACT_MATCH")]
        REGEXP_EXACT_MATCH = 4,
        [EnumMember(Value = "IN_LIST")]
        IN_LIST = 8,
        [EnumMember(Value = "IS_NULL")]
        IS_NULL = 16,
        [EnumMember(Value = "BETWEEN")]
        BETWEEN = 32,
        [EnumMember(Value = "NUMERIC_GREATER_THAN")]
        NUMERIC_GREATER_THAN = 64,
        [EnumMember(Value = "NUMERIC_GREATER_THAN_OR_EQUAL")]
        NUMERIC_GREATER_THAN_OR_EQUAL = 128,
        [EnumMember(Value = "NUMERIC_LESS_THAN")]
        NUMERIC_LESS_THAN = 256,
        [EnumMember(Value = "NUMERIC_LESS_THAN_OR_EQUAL")]
        NUMERIC_LESS_THAN_OR_EQUAL = 512,
    }
}
