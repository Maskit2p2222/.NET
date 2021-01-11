using VinsUncoderLibrary.Models;

namespace VinsUncoderLibrary.Services
{
    public class EnumService
    {
        public static TypeOfVinPartMeaning StringToEnum(string Enum)
        {
            return Enum switch
            {
                "Mark" => TypeOfVinPartMeaning.Mark,
                "Country" => TypeOfVinPartMeaning.Country,
                "VehicleProduction" => TypeOfVinPartMeaning.VehicleProduction,
                "WhoMadeCar" => TypeOfVinPartMeaning.WhoMadeCar,
                "Engine" => TypeOfVinPartMeaning.Engine,
                "EngineType" => TypeOfVinPartMeaning.EngineType,
                "PlaceOfAssembly" => TypeOfVinPartMeaning.PlaceOfAssembly,
                "TypeOfCar" => TypeOfVinPartMeaning.TypeOfCar,
                "Model" => TypeOfVinPartMeaning.Model,
                "SafetySystem" => TypeOfVinPartMeaning.SafetySystem,
                "Year" => TypeOfVinPartMeaning.Year,
                "NonValuePart" => TypeOfVinPartMeaning.NonValuePart,
                "TransmissionType" => TypeOfVinPartMeaning.TransmissionType,
                _ => TypeOfVinPartMeaning.SerialNumber,
            };
        }

    }
}
