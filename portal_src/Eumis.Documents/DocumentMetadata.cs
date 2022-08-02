using Eumis.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents
{
    public class DocumentMetadata
    {
        #region Static

        public static readonly DocumentMetadata ProjectMetadata =
            new DocumentMetadata(
                "R10019",
                typeof(R_10019.Project),
                "/doc:Project/s:Signature",
                new Dictionary<string, string>
                {
                    {"doc", "http://ereg.egov.bg/segment/R-10019" },
                    {"s", "http://www.w3.org/2000/09/xmldsig#" },
                });

        public static readonly DocumentMetadata MessageMetadata =
            new DocumentMetadata(
                "R10020",
                typeof(R_10020.Message),
                "/doc:Message/s:Signature",
                new Dictionary<string, string>
                {
                    {"doc", "http://ereg.egov.bg/segment/R-10020" },
                    {"s", "http://www.w3.org/2000/09/xmldsig#" },
                });

        public static readonly DocumentMetadata EvalTableMetadata =
            new DocumentMetadata(
                "R10023",
                typeof(R_10023.EvalTable),
                "/doc:EvalTable/s:Signature",
                new Dictionary<string, string>
                {
                    {"doc", "http://ereg.egov.bg/segment/R-10023" },
                    {"s", "http://www.w3.org/2000/09/xmldsig#" },
                });

        public static readonly DocumentMetadata EvalSheetMetadata =
            new DocumentMetadata(
                "R10026",
                typeof(R_10026.EvalSheet),
                "/doc:EvalSheet/s:Signature",
                new Dictionary<string, string>
                {
                    {"doc", "http://ereg.egov.bg/segment/R-10026" },
                    {"s", "http://www.w3.org/2000/09/xmldsig#" },
                });

        public static readonly DocumentMetadata StandpointMetadata =
            new DocumentMetadata(
                "R10027",
                typeof(R_10027.Standpoint),
                "/doc:Standpoint/s:Signature",
                new Dictionary<string, string>
                {
                    {"doc", "http://ereg.egov.bg/segment/R-10027" },
                    {"s", "http://www.w3.org/2000/09/xmldsig#" },
                });

        public static readonly DocumentMetadata BFPContractMetadata =
            new DocumentMetadata(
                "R10040",
                typeof(R_10040.BFPContract),
                "/doc:BFPContract/s:Signature",
                new Dictionary<string, string>
                {
                    {"doc", "http://ereg.egov.bg/segment/R-10040" },
                    {"s", "http://www.w3.org/2000/09/xmldsig#" },
                });

        public static readonly DocumentMetadata ProcurementsMetadata =
            new DocumentMetadata(
                "R10041",
                typeof(R_10041.Procurements),
                "/doc:Procurements/s:Signature",
                new Dictionary<string, string>
                {
                    {"doc", "http://ereg.egov.bg/segment/R-10041" },
                    {"s", "http://www.w3.org/2000/09/xmldsig#" },
                });

        public static readonly DocumentMetadata CommunicationMetadata =
            new DocumentMetadata(
                "R10042",
                typeof(R_10042.Communication),
                "/doc:Communication/s:Signature",
                new Dictionary<string, string>
                {
                    {"doc", "http://ereg.egov.bg/segment/R-10042" },
                    {"s", "http://www.w3.org/2000/09/xmldsig#" },
                });

        public static readonly DocumentMetadata FinanceReportMetadata =
            new DocumentMetadata(
                "R10043",
                typeof(R_10043.FinanceReport),
                "/doc:FinanceReport/s:Signature",
                new Dictionary<string, string>
                {
                    {"doc", "http://ereg.egov.bg/segment/R-10043" },
                    {"s", "http://www.w3.org/2000/09/xmldsig#" },
                });

        public static readonly DocumentMetadata TechnicalReportMetadata =
            new DocumentMetadata(
                "R10044",
                typeof(R_10044.TechnicalReport),
                "/doc:TechnicalReport/s:Signature",
                new Dictionary<string, string>
                {
                    {"doc", "http://ereg.egov.bg/segment/R-10044" },
                    {"s", "http://www.w3.org/2000/09/xmldsig#" },
                });

        public static readonly DocumentMetadata PaymentRequestMetadata =
            new DocumentMetadata(
                "R10045",
                typeof(R_10045.PaymentRequest),
                "/doc:PaymentRequest/s:Signature",
                new Dictionary<string, string>
                {
                    {"doc", "http://ereg.egov.bg/segment/R-10045" },
                    {"s", "http://www.w3.org/2000/09/xmldsig#" },
                });

        public static readonly DocumentMetadata SpendingPlanMetadata =
            new DocumentMetadata(
                "R10077",
                typeof(R_10077.SpendingPlan),
                "/doc:SpendingPlan/s:Signature",
                new Dictionary<string, string>
                {
                    {"doc", "http://ereg.egov.bg/segment/R-10077" },
                    {"s", "http://www.w3.org/2000/09/xmldsig#" },
                });

        public static readonly DocumentMetadata OfferMetadata =
            new DocumentMetadata(
                "R10080",
                typeof(R_10080.Offer),
                "/doc:Offer/s:Signature",
                new Dictionary<string, string>
                {
                    {"doc", "http://ereg.egov.bg/segment/R-10080" },
                    {"s", "http://www.w3.org/2000/09/xmldsig#" },
                });

        public static readonly IEnumerable<DocumentMetadata> Values = new List<DocumentMetadata>
        {
            ProjectMetadata,
            MessageMetadata,
            EvalTableMetadata,
            EvalSheetMetadata,
            StandpointMetadata,
            BFPContractMetadata,
            ProcurementsMetadata,
            CommunicationMetadata,
            FinanceReportMetadata,
            TechnicalReportMetadata,
            PaymentRequestMetadata,
            SpendingPlanMetadata,
            OfferMetadata
        };

        public static DocumentMetadata GetMetadataByDocumentCode(string documentCode)
        {
            return Values.Where(m => m.Code == documentCode).Single();
        }

        #endregion

        #region Public

        public string Code
        {
            get
            {
                return _code;
            }
        }

        public Type DocumentType
        {
            get
            {
                return _documentType;
            }
        }

        public string SignatureXPath
        {
            get
            {
                return _signatureXPath;
            }
        }

        public IDictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return _signatureXPathNamespaces;
            }
        }

        #endregion

        #region Private

        private string _code;
        private string _signatureXPath;
        private IDictionary<string, string> _signatureXPathNamespaces;
        private Type _documentType;

        private DocumentMetadata(
            string code,
            Type documentType,
            string signatureXPath,
            IDictionary<string, string> signatureXPathNamespaces)
        {
            _code =code;
            _documentType =documentType;
            _signatureXPath = signatureXPath;
            _signatureXPathNamespaces = new ReadOnlyDictionary<string, string>(signatureXPathNamespaces);
        }

        #endregion
    }
}
