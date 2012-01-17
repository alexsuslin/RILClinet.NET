﻿using Newtonsoft.Json;
using RIL.Constants;
using RestSharp;

namespace RIL.Objects
{
    public class RILResponse
    {
        #region Fields

        public readonly Status Status;
        public readonly string Error;
        public readonly bool isOk;

        #endregion

        #region Properties (Stats)

        public string KeyLimitReset { get; set; }

        public string KeyLimitRemaining { get; set; }

        public string KeyLimit { get; set; }

        public string UserLimitReset { get; set; }

        public string UserLimitRemaining { get; set; }

        public string UserLimit { get; set; }

        protected internal IRestResponse Response { get; private set; }

        #endregion

        #region Constructors

        internal RILResponse(IRestResponse response)
        {
            Response = response;
            Status = (Status) response.StatusCode;
            isOk = Status == Status.Success;

            Error = response.Headers.GetValueByName(Header.Error);
            UserLimit = response.Headers.GetValueByName(Header.UserLimit);
            UserLimitRemaining = response.Headers.GetValueByName(Header.UserLimitRemaining);
            UserLimitReset = response.Headers.GetValueByName(Header.UserLimitReset);
            KeyLimit = response.Headers.GetValueByName(Header.KeyLimit);
            KeyLimitRemaining = response.Headers.GetValueByName(Header.KeyLimitRemaining);
            KeyLimitReset = response.Headers.GetValueByName(Header.KeyLimitReset);
        }

        #endregion
    }


    public class RILResponse<T> : RILResponse
    {
        #region Properties

        public T Data { get; protected internal set; }

        #endregion

        #region Constructors

        internal RILResponse(IRestResponse<T> response) : base(response)
        {
            // There is a bug in RestSharp
            //UserStatsWrapper wrapper = restResponse.Data;
            Data = JsonConvert.DeserializeObject<T>(response.Content);
        }

        protected internal RILResponse(IRestResponse response) : base(response)
        {
        }

        #endregion
    }
}

