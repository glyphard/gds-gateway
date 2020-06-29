using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDS.Gateway.Models.Io
{
    public class SetCredentialsRequest
    {
        [JsonProperty("key")]
        public string KeyAuth { get; set; }
        [JsonProperty("userPass")]
        public  UserPass UserPassAuth{ get; set; }
        [JsonProperty("userToken")]
        public UserToken UserTokenAuth { get; set; }
    }

    public class UserPass {
    [JsonProperty("username")]
    public string Username { get; set; }
    [JsonProperty("password")]
    public string Password { get; set; }

}
public class UserToken {
    [JsonProperty("username")]
    public string Username { get; set; }
    [JsonProperty("token")]
    public string Token { get; set; }

}
}
