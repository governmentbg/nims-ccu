using System.Reflection;
using Eumis.Common.Validation;
using Eumis.Components.Web;
using Eumis.Documents.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eumis.Documents.Validation;
using Eumis.Portal.Web.Helpers;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Models.PaymentRequest
{
    public class EditVM : BaseVM, IEditVM<R_10045.PaymentRequest>, IEngineValidatable, IRemoteValidatable
    {
        public R_10049.PaymentRequestBasicData BasicData { get; set; }

        public R_10045.PaymentRequestAttachedDocuments AttachedDocuments { get; set; }

        public string contractGid { get; set; }
        public string packageGid { get; set; }

        public string contractNumber { get; set; }
        public string docNumber { get; set; }
        public string docSubNumber { get; set; }
        public DateTime activationDate { get; set; }

        public List<string> RemoteValidationErrors { get; set; }
        public List<string> RemoteValidationWarnings { get; set; }

        #region Get Set

        public EditVM() { }

        public EditVM(R_10045.PaymentRequest paymentRequest)
        {
            this.BasicData = paymentRequest.BasicData;

            this.AttachedDocuments = paymentRequest.AttachedDocuments;

            this.contractGid = paymentRequest.contractGid;
            this.packageGid = paymentRequest.packageGid;

            this.contractNumber = paymentRequest.contractNumber;
            this.docNumber = paymentRequest.docNumber;
            this.docSubNumber = paymentRequest.docSubNumber;
            this.activationDate = paymentRequest.modificationDate;
        }

        public R_10045.PaymentRequest Set(R_10045.PaymentRequest paymentRequest)
        {
            return paymentRequest;
        }

        public R_10045.PaymentRequest SetAsync()
        {
            var request = (R_10045.PaymentRequest)AppContext.Current.Document;

            if (!(request.BasicData != null && request.BasicData.isLocked))
            {
                if (this.BasicData != null && request.BasicData != null)
                {
                    this.BasicData.id = request.BasicData.id;
                    this.BasicData.isLocked = request.BasicData.isLocked;

                    if(request.BasicData.IsAdvanceType)
                    {
                        this.BasicData.FinanceReportAmount = 0;
                    }
                }

                request.BasicData = this.BasicData;
            }

            if (!(request.AttachedDocuments != null && request.AttachedDocuments.isLocked))
            {
                if (this.AttachedDocuments == null)
                    request.AttachedDocuments.AttachedDocumentCollection = new R_10045.AttachedDocumentCollection();
                else
                    request.AttachedDocuments.AttachedDocumentCollection = this.AttachedDocuments.AttachedDocumentCollection;
            }

            return request;
        }

        #endregion
    }
}