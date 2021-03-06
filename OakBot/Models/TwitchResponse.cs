﻿using Newtonsoft.Json;
using OakBot.Enums;
using System;

namespace OakBot.Models
{
    public class TwitchResponse
    {
        #region Public Constructors

        public TwitchResponse()
        {
        }

        #endregion Public Constructors

        #region Public Properties

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("_total")]
        public long Total { get; set; }

        #endregion Public Properties

        #region Public Methods

        public State GetState()
        {
            State state;
            switch (Status)
            {
                case 204:
                    state = State.success;
                    break;

                case 404:
                    state = State.not_found;
                    break;

                case 422:
                    state = State.failed;
                    break;

                case 503:
                    state = State.failed;
                    break;

                default:
                    state = State.failed;
                    break;
            }
            return state;
        }

        public override string ToString()
        {
            return Status + ": " + Message;
        }

        [Obsolete("Method obsolete, please use GetState() instead")]
        public bool WasSuccesfull()
        {
            return Status == 204;
        }

        #endregion Public Methods
    }
}