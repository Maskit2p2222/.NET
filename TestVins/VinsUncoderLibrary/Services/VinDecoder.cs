using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VinsUncoderLibrary.DataBase;
using VinsUncoderLibrary.Models;

namespace VinsUncoderLibrary
{
    public class Uncoder
    {
        private List<int> _idOfExcludedMasks;
        public List<VinPartDecodingResult> listOfVinDecodingResult = new List<VinPartDecodingResult>();
        public Uncoder()
        {
            _idOfExcludedMasks = new List<int>();
        }
        public List<VinPartDecodingResult> UncodeVinWhithReturn(Vin vin)
        {
            try
            {
                if (GetListOfVinDecodingResult(vin) != null)
                {
                    if (VinDataBase.GetVinById(vin.VinTextValue).VinTextValue == vin.VinTextValue)
                    {
                        return listOfVinDecodingResult;
                    }
                    listOfVinDecodingResult = new List<VinPartDecodingResult>();

                }
                return null;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

        public void UncodeVin(Vin vin)
        {
            try
            {
                if (GetListOfVinDecodingResult(vin) != null)
                {
                    if (VinDataBase.GetVinById(vin.VinTextValue).VinTextValue != vin.VinTextValue)
                    {
                        VinDataBase.AddVin(vin);
                        VinDecodingResultDataBase.AddRangeOfResults(listOfVinDecodingResult);
                    }
                    listOfVinDecodingResult = new List<VinPartDecodingResult>();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private List<VinPartDecodingResult> GetListOfVinDecodingResult(Vin vin)
        {
            if (vin.VinTextValue.Length == 17)
            {
                
                Mask WorldManufacturerIdentifierMask = MaskDataBase.GetMasksByIdOfMask(12);
                string MarkId = "";
                List<VinDescription> descriptions = VinDescriptionsDataBase
                    .GetVinsDescriptionsByMaskId(WorldManufacturerIdentifierMask.IdOfMask);
                foreach (VinDescription description in descriptions) { 
                    if(description.VinPart == GetVinSubstringByMask(vin, WorldManufacturerIdentifierMask))
                    {
                        listOfVinDecodingResult.Add(new VinPartDecodingResult
                        {
                            EnumMeaning = description.EnumMeaningOfVinParts,
                            Vin = vin.VinTextValue,
                            VinPartDescription = description.VinPartDecription
                        });
                        MarkId = description.IdOfMark;
                        break;
                    }
                }
                if(String.IsNullOrEmpty(MarkId))
                {
                    throw new InvalidMarkException("Invalid Mark");
                }
                List<Mask> masks = MaskDataBase.GetMasksByIdOfMark(MarkId);
                foreach (Mask mask in masks)
                {
                    if (!IsMaskAlreadyExcluded(mask.IdOfMask, _idOfExcludedMasks))
                    {
                        descriptions = VinDescriptionsDataBase.GetVinsDescriptionsByMarkIdAndMaskId(mask.IdOfMark,mask.IdOfMask);
                        foreach (VinDescription description in descriptions)
                        {
                            if (description.VinPart == GetVinSubstringByMask(vin, mask))
                            {
                                UncoderHelper(mask, description, vin);
                                break;
                            }
                        }
                    }
                }
                listOfVinDecodingResult.Add(new VinPartDecodingResult
                {
                    EnumMeaning = TypeOfVinPartMeaning.SerialNumber,
                    VinPartDescription = vin.VinTextValue.Substring(11, 6),
                    Vin = vin.VinTextValue
                });
                return listOfVinDecodingResult;
            }
            else
            {
                throw new NonCorrectVinLenghtException("Vins lenght is not 17 symbols");
            }


        }

        private void UncoderHelper(Mask mask, VinDescription description, Vin vin)
        {
            listOfVinDecodingResult.Add(new VinPartDecodingResult
            {
                EnumMeaning = description.EnumMeaningOfVinParts,
                Vin = vin.VinTextValue,
                VinPartDescription = description.VinPartDecription
            });
            if (!LinkDataBase.GetLinkTablesByIdOfDescription(description.IdOfDescription).Any())
            {
                return;
            }
            else
            {
                List<VinDescriptionsLink> listOfLinks = LinkDataBase.GetLinkTablesByIdOfDescription(description.IdOfDescription);
                foreach(VinDescriptionsLink link in listOfLinks)
                {
                    VinDescription linkDescription = VinDescriptionsDataBase.GetVinsDescriptionsByDescriptionId(link.IdOfDescriptionSecond);
                    Mask desciptionMask = MaskDataBase.GetMasksByIdOfMask(linkDescription.IdOfMask);
                    if(linkDescription.VinPart == GetVinSubstringByMask(vin, desciptionMask))
                    {
                        _idOfExcludedMasks.Add(desciptionMask.IdOfMask);
                        UncoderHelper(desciptionMask, linkDescription, vin);
                        break;
                    }
                }
            }
        }

        private bool IsMaskAlreadyExcluded(int idOfMasks, List<int> ExcludedMasks)
        {
            foreach (int excludedMask in ExcludedMasks)
            {
                if (idOfMasks == excludedMask)
                {
                    return true;
                }
            }
            return false;
        }
        private string GetVinSubstringByMask(Vin vin, Mask mask)
        {
            string SubstringToReturn = "";
            for (int i = 0; i < mask.MaskView.Length; i++)
            {
                if (mask.MaskView.ElementAt(i) == 'X')
                {
                    SubstringToReturn += vin.VinTextValue.Substring(i, 1);
                }
            }
            return SubstringToReturn;

        }
    }
}
