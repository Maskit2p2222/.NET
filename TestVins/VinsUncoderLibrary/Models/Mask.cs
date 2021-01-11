using System.ComponentModel.DataAnnotations;

namespace VinsUncoderLibrary.Models
{
    public class Mask
    {
        public string IdOfMark { set; get; }
        [KeyAttribute]
        public int IdOfMask { set; get; }
        public string MaskView { set; get; }
    }
}
