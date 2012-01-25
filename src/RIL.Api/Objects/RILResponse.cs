using RIL.Constants;
using RestSharp;

namespace RIL.Objects
{
    public interface IRILResponse
    {
        string KeyLimitReset { get; set; }
        string KeyLimitRemaining { get; set; }
        string KeyLimit { get; set; }
        string UserLimitReset { get; set; }
        string UserLimitRemaining { get; set; }
        string UserLimit { get; set; }
        bool IsOk { get; }
        Status Status { get; set; }
        string Error { get; set; }
    }

    public interface IRILResponse<T> : IRILResponse
    {
        T Data { get; set; }
    }

    public abstract class RILResponseBase
    {
        #region Properties (Stats)

        public string KeyLimitReset { get; set; }

        public string KeyLimitRemaining { get; set; }

        public string KeyLimit { get; set; }

        public string UserLimitReset { get; set; }

        public string UserLimitRemaining { get; set; }

        public string UserLimit { get; set; }

        protected internal IRestResponse Response { get; set; }

        public bool IsOk { get { return Status == Status.Success; } }

        public Status Status { get; set; }
        public string Error { get; set; }

        #endregion

        protected internal RILResponseBase(IRestResponse response)
        {
            Response = response;
            Status = (Status) response.StatusCode;
            Error = response.Headers.GetValueByName(Header.Error);
            UserLimit = response.Headers.GetValueByName(Header.UserLimit);
            UserLimitRemaining = response.Headers.GetValueByName(Header.UserLimitRemaining);
            UserLimitReset = response.Headers.GetValueByName(Header.UserLimitReset);
            KeyLimit = response.Headers.GetValueByName(Header.KeyLimit);
            KeyLimitRemaining = response.Headers.GetValueByName(Header.KeyLimitRemaining);
            KeyLimitReset = response.Headers.GetValueByName(Header.KeyLimitReset);
        }

        protected RILResponseBase()
        {
        }
    }

    public class RILResponse<T> : RILResponseBase, IRILResponse<T>
    {

        protected internal RILResponse(IRestResponse<T> response) : base(response)
        {
            Data = response.Data;
        }

        protected internal RILResponse(IRestResponse response): base(response)
        {
        }


        public static implicit operator RILResponse<T>(RestResponse response)
        {
            return new RILResponse<T>(response);
        }

        public static implicit operator RILResponse<T>(RestResponse<T> response)
        {
            return new RILResponse<T>(response);
        }

        #region Properties

        public T Data { get; set; }

        #endregion

    }

    public class RILResponse : RILResponseBase, IRILResponse
    {
        protected internal RILResponse(IRestResponse response):base(response)
        {
        }

        public static implicit operator RILResponse(RestResponse response)
        {
            return new RILResponse(response);
        }
    }
}