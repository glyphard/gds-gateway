using System;
using System.Collections.Generic;
using System.Text;

namespace GDS.Gateway.Constants
{
    public class Lookups
    {
        public const string LicenseKey = "key";
        public const string ConfigStep = "ConfigStep";
        public static string GetEnvironmentVariable(string name)
        {
            return System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);           
        }


        /*
        public const string AuthActionType = "AuthActionType";
        public const string AuthActionType_SetCredentials = "SetCredentials";
        public const string AuthActionType_IsAuthValid = "IsAuthValid";
        public const string AuthActionType_ResetAuth = "ResetAuth";

        

        public const string GatewayType = "GatewayType";
        public const string GatewayType_Blob = "Blob";
        public const string GatewayType_SQL = "SQL";

        public const string BlobKey = "BlobKey";
        public const string BlobPath = "BlobPath";

        public const string SqlConnStr = "SqlConnStr";
        public const string SqlHost = "SqlHost";
        public const string SqlDatabase = "SqlDatabase";
        public const string SqlUsername = "SqlUsername";
        public const string SqlPassword = "SqlPassword";
        public const string SqlTable = "SqlTable";
        */
    }
}
