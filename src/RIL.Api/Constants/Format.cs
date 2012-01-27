using System.Runtime.Serialization;

namespace RIL.Constants
{
    public enum Format
    {
        [EnumMember(Value = Methods.Params.JsonFormat)]
        Json,
        [EnumMember(Value = Methods.Params.XmlFormat)]
        Xml
    }
}
