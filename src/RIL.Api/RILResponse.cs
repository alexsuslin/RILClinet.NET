using RestSharp;

namespace RIL
{
    public class RILResponse
    {
        #region Fields

        public readonly Status Status;
        public readonly string Error;
        public readonly bool isOk;

        #endregion

        #region Properties (Stats)

        protected string KeyLimitReset { get; set; }

        protected string KeyLimitRemaining { get; set; }

        protected string KeyLimit { get; set; }

        protected string UserLimitReset { get; set; }

        protected string UserLimitRemaining { get; set; }

        protected string UserLimit { get; set; }

        #endregion


        public RILResponse(RestResponse response)
        {
            Status = (Status)response.StatusCode;
            isOk = Status == Status.Success;
           
            Error = response.Headers.GetValueByName(Header.Error);
            UserLimit = response.Headers.GetValueByName(Header.UserLimit);
            UserLimitRemaining = response.Headers.GetValueByName(Header.UserLimitRemaining);
            UserLimitReset = response.Headers.GetValueByName(Header.UserLimitReset);
            KeyLimit = response.Headers.GetValueByName(Header.KeyLimit);
            KeyLimitRemaining = response.Headers.GetValueByName(Header.KeyLimitRemaining);
            KeyLimitReset = response.Headers.GetValueByName(Header.KeyLimitReset);
        }
    }
}
