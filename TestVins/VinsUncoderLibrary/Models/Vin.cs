using System.ComponentModel.DataAnnotations;

namespace VinsUncoderLibrary.Models
{
    public class Vin
    {

        [StringLengthAttribute(17)]
        [KeyAttribute]
        public string VinTextValue { set; get; }

    }
}
