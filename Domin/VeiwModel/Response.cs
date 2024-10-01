using System.Runtime.Serialization;

namespace Domin.VeiwModel
{
    [DataContract]
    public class Response<T>
    {
        public Response()
        {
        }
        [DataMember]
        public new T Data { get; set; }
        public string ErrorMassage { get; set; }
        public string SuccessMassage { get; set; }
        public bool Status { get; set; } = true;
    }
}
