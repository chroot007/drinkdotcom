using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkDotCom.Models
{
    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }

        [JsonProperty("challenge_ts")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty("hostname")]
        public string HostName { get; set; }
    }
}