using System.Runtime.Serialization;

namespace RIL.Constants
{
    public enum State
    {
        [EnumMember(Value = Methods.Params.Read)]
        Read,
        [EnumMember(Value = Methods.Params.Unread)]
        Unread,
        [EnumMember(Value = "")]
        All
    }
}
