

using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace LUNA.Models.Models
{
    public class TypeAttribute
    {
       
        public long DeviceTypeID { get; set; }
        public long AttributeID { get; set; }
        public string Value { get; set; }
        // Json Iginore for serializing (passing object to javascript array)
        [JsonIgnore]     
        public PType DeviceType { get; set; }
        public Attribute Attribute {get;set;}

  

    }
}