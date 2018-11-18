using System.Runtime.Serialization;

namespace RaboOmniKassaApi.Net.Exceptions
{
    [DataContract]
    public class RaboOmniKassaError
    {
        [DataMember(Name = "errorCode", EmitDefaultValue = false, IsRequired = true)]
        public int ErrorCode { get; set; }

        [DataMember(Name = "errorMessage", EmitDefaultValue = false, IsRequired = false)]
        public string ErrorMessage { get; set; }

        [DataMember(Name = "consumerMessage", EmitDefaultValue = false, IsRequired = false)]
        public string ConsumerMessage { get; set; }
    }
}
