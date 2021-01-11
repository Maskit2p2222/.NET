using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinsUncoderLibrary.Models
{
    public class MaskModel
    {

        public string IdOfMark { set; get; }
        [KeyAttribute]
        public int IdOfMask { set; get; }
        public string MaskView { set; get; }
        public int CountOfDescriptions { set; get; }


    }
}
