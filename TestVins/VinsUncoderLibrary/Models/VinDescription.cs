

using System.ComponentModel.DataAnnotations;

namespace VinsUncoderLibrary.Models
{
    public class VinDescription
    {
        [Key]
        public int IdOfDescription { set; get; }
        public string IdOfMark { set; get; }
        public int IdOfMask { set; get; }
        public string VinPart{ set; get; }
        public string VinPartDecription { set; get; }
        public TypeOfVinPartMeaning EnumMeaningOfVinParts { set; get; }

        public string Mask { set; get; }


    }
}
