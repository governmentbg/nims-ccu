using System.Collections.Generic;

namespace Eumis.Common.Validation
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Интерфейсът е поставен в това assembly за да може да се използва
    /// едновременно от Components и RioObjects
    /// </remarks>
    public interface ICSValidationEngine
    {
        string AppRioCode { get; }
        object RioApplication { get; }

        void Validate(
            string complexTypeName,
            object complexType,
            string modelPath,
            IList<ValidationOption> errors);
        
        //// address
        //bool ValidateDistrict(string code, string name);
        //bool ValidateMunicipality(string code, string name);
        //bool ValidateSettlement(string code, string name);
        //
        //bool ValidateEKATTEDistrict(string code, string name);
        //bool ValidateEKATTEMunicipality(string code, string name);
        //bool ValidateEKATTESettlement(string code, string name);
        //bool ValidateEKATTEArea(string code, string name);        
        //
        ////nomenclatures
        //bool ValidateResidencePeriodNomenclature(string code);
        //bool ValidateResidencePurposeNomenclature(string code);
        //bool ValidateServiceResultReceiptMethodNomenclature(string uri);        
        //bool ValidateServiceTermTypeNomenclature(string uri);
        //bool ValidateElectronicServiceProviderTypeNomenclature(string uri);
        //bool ValidateApplicationTypeNomenclature(string uri);
        //bool ValidateDiningAndEntertainmentSiteTypeNomenclature(string code, string name);
        //bool ValidateShelterAndAccommodationTypeNomenclature(string code, string name);
        //bool ValidateTouristSiteCategoryNomenclature(string code, string name);
        //bool ValidateDocumentElectronicTransportTypeNomenclature(string uri);
        //bool ValidateElectronicDocumentDiscrepancyTypeNomenclature(string uri);
    }
}
