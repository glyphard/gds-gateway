using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GDS.Gateway.Models.Domain
{
    public enum AggregationTypes
    {

        [EnumMember(Value = "NONE")] //     No aggregation
        NONE = 0,
        [EnumMember(Value = "AUTO")] //     Should be set for calculated fields involving an aggregation
        AUTO = 1,
        [EnumMember(Value = "AVG")] //  The numerical average (mean) of the entries.
        AVG = 2,
        [EnumMember(Value = "COUNT")] //    The number of entries.
        COUNT = 3,
        [EnumMember(Value = "COUNT_DISTINCT")] //   The number of distinct entries.
        COUNT_DISTINCT = 4,
        [EnumMember(Value = "MAX")] //  The maximum of the entries.
        MAX = 5,
        [EnumMember(Value = "MIN")] //  The minimum of the entries.
        MIN = 6,
        [EnumMember(Value = "SUM")] //  The sum of the entries.
        SUM = 7

    }
}
