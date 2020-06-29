using GDS.Gateway.Constants;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GDS.Gateway.Models.Domain
{
    [Flags]
    public enum ActionTypes
    {

        [EnumMember(Value = "AuthActionType")]
        AuthActionType = 0,

        [EnumMember(Value = "SetCredentials")]
        SetCredentials = 1,

        [EnumMember(Value = "IsAuthValid")]
        IsAuthValid = 2,

        [EnumMember(Value = "ResetAuth")]
        ResetAuth = 4


    }
}
