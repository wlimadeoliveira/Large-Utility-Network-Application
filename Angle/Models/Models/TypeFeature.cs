

namespace LUNA.Models.Models
{
    public class TypeFeature
    {

        public long DeviceTypeID { get; set; }
        public long FeatureID { get; set; }
        public PType DeviceType { get; set; }
        public Feature Feature {get;set;}

    }
}