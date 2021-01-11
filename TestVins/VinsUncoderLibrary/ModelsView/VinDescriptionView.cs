using VinsUncoderLibrary.Models;

namespace VinsUncoderLibrary.ModelsView
{
    public class VinDescriptionView
    {
        public string VinPart { set; get; }
        public string VinPartDecription { set; get; }
        public TypeOfVinPartMeaning EnumMeaningOfVinParts { set; get; }
    }
}