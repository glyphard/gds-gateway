using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GDS.Gateway.Models.Domain
{
    [Flags]
    public enum ConceptType
    {

        [EnumMember(Value = "DIMENSION")] //   A dimension. Dimensions are data categories with values such as names, descriptions or other characteristics of a category.
        DIMENSION = 0,
        [EnumMember(Value = "METRIC")] //  A metric. Metrics measure dimension values and represent measurements such as a sum, count, ratio, etc.
        METRIC = 1,


    }

    [Flags]
    public enum DimensionFilterType
    {

        [EnumMember(Value = "INCLUDE")] // 
        INCLUDE = 0,
        [EnumMember(Value = "EXCLUDE")] // 
        EXCLUDE = 1
    }

    [Flags]
    public enum DataType {
        [EnumMember(Value = "STRING")] //   An arbitrary string. Defined by the JSON Schema spec.
        STRING = 0,
        [EnumMember(Value = "NUMBER")] //   A numeric data type in the double-precision 64-bit floating point format (IEEE 754).
        NUMBER = 1,
        [EnumMember(Value = "BOOLEAN")] // A boolean value, either true or false. Defined by the JSON Schema spec.
        BOOLEAN = 2,
    }
}
