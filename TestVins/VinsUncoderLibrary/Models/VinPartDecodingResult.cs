using System.ComponentModel.DataAnnotations;

namespace VinsUncoderLibrary.Models
{
    public class VinPartDecodingResult
    {
        public int Id { set; get; }

        [StringLengthAttribute(17)]
        public string Vin { get; set; }
        public TypeOfVinPartMeaning EnumMeaning { set; get; }
        public string VinPartDescription { set; get; }
    }
}
