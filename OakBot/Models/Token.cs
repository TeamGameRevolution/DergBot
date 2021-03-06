﻿using Newtonsoft.Json;

namespace OakBot.Models
{
    [JsonObject("token")]
    public class Token
    {
        #region Public Properties

        [JsonProperty("authorization")]
        public Authorization Authorization { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("valid")]
        public bool Valid { get; set; }

        #endregion Public Properties
    }
}