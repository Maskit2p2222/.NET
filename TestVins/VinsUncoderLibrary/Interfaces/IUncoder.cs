using System.Collections.Generic;
using VinsUncoderLibrary.Models;

namespace VinsUncoderLibrary.Interfaces
{
    public interface IUncoder
    {
        public void Check(Vin vin);

        public List<VinPartDecodingResult> VinsUncoder(Vin vin);

        public string Substring(Vin vin, Mask mask);

        public bool ExcludedMasksCheck(int idOfMasks, List<int> ExcludedMasks);


    }
}
