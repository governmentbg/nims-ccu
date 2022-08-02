//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;
using Eumis.Common.Resources;
using Eumis.Documents.Contracts;
using Eumis.Documents.Enums;
using System.Collections.Generic;
using Eumis.Documents.Interfaces;
using System.Linq;
using Eumis.Documents;
using Eumis.Documents.Validation;
using Eumis.Common.Validation;
using Eumis.Common.Helpers;
using System.Globalization;
using R_10018;
using Eumis.Common.Linq;

namespace R_10080
{
    public partial class Offer : IDocumentNomenclatures, IEumisDocument, IEumisDocumentWithFiles, ILocalValidatable
    {
        string IEumisDocument.Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        DateTime IEumisDocument.CreateDate
        {
            get
            {
                return this.createDate;
            }
            set
            {
                this.createDate = value;
            }
        }

        DateTime IEumisDocument.ModificationDate
        {
            get
            {
                return this.modificationDate;
            }
            set
            {
                this.modificationDate = value;
            }
        }

        public IEnumerable<AttachedDocument> Files
        {
            get
            {
                return EnumerableExtensions.Concat(
                    this.GetFiles(d => d.AttachedDocuments.AttachedDocumentCollection),
                    this.GetFiles(
                        d => d.AttachedDocuments.AttachedDocumentCollection,
                        d => d.SignatureContentCollection.Select(adc => new AttachedDocument() { AttachedDocumentContent = adc })));
            }
        }

        public static Offer Init(R_10040.BFPContract contract, R_10041.Procurements procurements, string contractGid, string procurementsGid, string planGid, string positionGid)
        {
            Offer offer = new Offer();

            #region gids

            offer.contractGid = contractGid;
            offer.procurementsGid = procurementsGid;
            offer.planGid = planGid;
            offer.positionGid = positionGid;

            #endregion

            #region BasicData

            var selectedPlan = procurements.ProcurementPlans.ProcurementPlanCollection.Single(e => planGid.Equals(e.gid));
            var selectedPosition = selectedPlan.DifferentiatedPositionCollection.Single(e => positionGid.Equals(e.gid));

            offer.BasicData = new R_10079.OfferBasicData()
            {
                id = Guid.NewGuid().ToString(),

                BeneficiaryUinType = contract.Beneficiary.UinType,
                BeneficiaryUin = contract.Beneficiary.Uin,

                PlanName = selectedPlan.BFPContractPlan.Name,
                PlanErrandArea = selectedPlan.BFPContractPlan.ErrandArea,
                PlanErrandLegalAct = selectedPlan.BFPContractPlan.ErrandLegalAct,
                PlanErrandType = selectedPlan.BFPContractPlan.ErrandType,
                PlanExpectedAmount = selectedPlan.ExpectedAmount,
                PlanDescription = selectedPlan.BFPContractPlan.Description,
                DifferentiatedPosition = selectedPosition,

                BeneficiaryRegistrationVAT = new R_09991.EnumNomenclature(),
            };


            if (offer.BasicData.BeneficiaryRegistrationVAT == null || String.IsNullOrWhiteSpace(offer.BasicData.BeneficiaryRegistrationVAT.Value))
                offer.BasicData.BeneficiaryRegistrationVAT = new R_09991.EnumNomenclature
                {
                    Value = YesNoNotApplicableNomenclature.No.Id,
                    Description = YesNoNotApplicableNomenclature.No.Name
                };


            #endregion

            #region Candidate

            offer.Candidate = new R_10004.Company()
            {
                Seat = new R_10003.Address() { Country = new R_10001.PublicNomenclature() { Code = Constants.BulgariaId, Name = Constants.BulgariaName } },
                Correspondence = new R_10003.Address() { Country = new R_10001.PublicNomenclature() { Code = Constants.BulgariaId, Name = Constants.BulgariaName } }
            };
            offer.Candidate.id = Guid.NewGuid().ToString();

            #endregion

            #region AttachedDocuments

            offer.AttachedDocuments = new OfferAttachedDocuments()
            {
                id = Guid.NewGuid().ToString(),
                AttachedDocumentCollection = new AttachedDocumentCollection()
                {
                    new R_10018.AttachedDocument()
                        {
                            AttachedDocumentContent = new R_09992.AttachedDocumentContent(),
                            SignatureContentCollection = new R_10018.AttachedDocumentContentCollection()
                            {
                                new R_09992.AttachedDocumentContent()
                            }
                        }
                }
            };

            #endregion

            return offer;
        }

        [XmlIgnore]
        public List<ModelValidationResultExtended> LocalValidationErrors { get; set; }

        [XmlIgnore]
        public Dictionary<Eumis.Documents.Mappers.NomenclatureType, List<Eumis.Documents.Mappers.Nomenclature>> Nomenclatures { get; set; }

        [XmlIgnore]
        public List<string> RemoteValidationErrors { get; set; }

        [XmlIgnore]
        public List<string> RemoteValidationWarnings { get; set; }
    }
}
