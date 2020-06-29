using GDS.Gateway.Constants;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GDS.Gateway.Models.Domain
{
    [Flags]
    public enum ConfigBlobs
    {
        [EnumMember(Value = "ConfigBlobs")]
        ConfigBlobs = 0,

        [EnumMember(Value = "BlobKey")]
        BlobKey = 1,

        [EnumMember(Value = "BlobPath")]
        BlobPath = 2,

}
}
