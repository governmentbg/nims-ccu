using System.Reflection;
using Eumis.Common.Validation;
using Eumis.Components.Web;
using Eumis.Documents.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eumis.Documents.Contracts;
using Eumis.Documents.Validation;
using Eumis.Portal.Web.Helpers;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Models.TechnicalReport
{
    public class EditVM : BaseVM, IEditVM<R_10044.TechnicalReport>, IEngineValidatable, IRemoteValidatable
    {
        public R_10052.TechnicalReportBasicData BasicData { get; set; }
        public R_10044.Activities Activities { get; set; }
        public R_10044.Indicators Indicators { get; set; }
        public R_10044.Team Team { get; set; }
        public R_10044.TechnicalReportAttachedDocuments AttachedDocuments { get; set; }

        public string contractGid { get; set; }
        public string packageGid { get; set; }

        public string contractNumber { get; set; }
        public string docNumber { get; set; }
        public string docSubNumber { get; set; }
        public DateTime activationDate { get; set; }

        public List<string> RemoteValidationErrors { get; set; }
        public List<string> RemoteValidationWarnings { get; set; }

        public List<ApplicationSection> ApplicationSections { get; private set; }

        #region Get Set

        public EditVM() { }

        public EditVM(R_10044.TechnicalReport technicalReport)
        {
            this.BasicData = technicalReport.BasicData;
            this.Activities = technicalReport.Activities;
            this.Indicators = technicalReport.Indicators;
            this.Team = technicalReport.Team;
            this.AttachedDocuments = technicalReport.AttachedDocuments;
            this.ApplicationSections = technicalReport.ApplicationSections;

            this.contractGid = technicalReport.contractGid;
            this.packageGid = technicalReport.packageGid;

            this.contractNumber = technicalReport.contractNumber;
            this.docNumber = technicalReport.docNumber;
            this.docSubNumber = technicalReport.docSubNumber;
            this.activationDate = technicalReport.modificationDate;
        }

        public R_10044.TechnicalReport Set(R_10044.TechnicalReport technicalReport)
        {
            return technicalReport;
        }

        public R_10044.TechnicalReport SetAsync()
        {
            var report = (R_10044.TechnicalReport)AppContext.Current.Document;

            #region BasicData

            if (!(report.BasicData != null && report.BasicData.isLocked))
            {
                if (this.BasicData != null && report.BasicData != null)
                {
                    this.BasicData.Procedure = report.BasicData.Procedure;
                    this.BasicData.ProjectName = report.BasicData.ProjectName;

                    this.BasicData.Beneficiary = report.BasicData.Beneficiary;

                    this.BasicData.PartnerCollection = report.BasicData.PartnerCollection;
                    this.BasicData.ExecutionPeriodStartDate = report.BasicData.ExecutionPeriodStartDate;
                    this.BasicData.ExecutionPeriodEndDate = report.BasicData.ExecutionPeriodEndDate;

                    this.BasicData.id = report.BasicData.id;
                    this.BasicData.isLocked = report.BasicData.isLocked;
                }

                report.BasicData = this.BasicData;
            }

            #endregion

            #region Activities

            if (!(report.Activities != null && report.Activities.isLocked))
            {
                if(report.Activities.ActivityCollection != null && report.Activities.ActivityCollection.Count > 0)
                {
                    // Check if count is equal
                    if (report.Activities.ActivityCollection.Count != this.Activities.ActivityCollection.Count)
                        throw new Exception("Different count of activity collections");

                    for (int i = 0; i < report.Activities.ActivityCollection.Count;i++ )
                    {
                        if(report.Activities.ActivityCollection[i].BFPContractActivity == null
                            || String.IsNullOrWhiteSpace(report.Activities.ActivityCollection[i].BFPContractActivity.gid)
                            || this.Activities.ActivityCollection[i].BFPContractActivity == null
                            || String.IsNullOrWhiteSpace(this.Activities.ActivityCollection[i].BFPContractActivity.gid)
                            || !report.Activities.ActivityCollection[i].BFPContractActivity.gid.Equals(this.Activities.ActivityCollection[i].BFPContractActivity.gid))
                        {
                            throw new Exception("Invalid gid in activity collections");
                        }
                        this.Activities.ActivityCollection[i].BFPContractActivity = report.Activities.ActivityCollection[i].BFPContractActivity;
                        this.Activities.ActivityCollection[i].ContractContractorCollection = report.Activities.ActivityCollection[i].ContractContractorCollection;
                    }

                    report.Activities.ActivityCollection = this.Activities.ActivityCollection;
                }
            }

            #endregion

            #region Indicators

            if (!(report.Indicators != null && report.Indicators.isLocked))
            {
                if (report.Indicators.IndicatorCollection != null && report.Indicators.IndicatorCollection.Count > 0)
                {
                    // Check if count is equal
                    if (report.Indicators.IndicatorCollection.Count != this.Indicators.IndicatorCollection.Count)
                        throw new Exception("Different count of indicator collections");

                    for (int i = 0; i < report.Indicators.IndicatorCollection.Count; i++)
                    {
                        if (report.Indicators.IndicatorCollection[i].BFPContractIndicator == null
                            || String.IsNullOrWhiteSpace(report.Indicators.IndicatorCollection[i].BFPContractIndicator.gid)
                            || this.Indicators.IndicatorCollection[i].BFPContractIndicator == null
                            || String.IsNullOrWhiteSpace(this.Indicators.IndicatorCollection[i].BFPContractIndicator.gid)
                            || !report.Indicators.IndicatorCollection[i].BFPContractIndicator.gid.Equals(this.Indicators.IndicatorCollection[i].BFPContractIndicator.gid))
                        {
                            throw new Exception("Invalid gid in indicator collections");
                        }

                        report.Indicators.IndicatorCollection[i].BFPContractIndicator.Description = this.Indicators.IndicatorCollection[i].BFPContractIndicator.Description;

                        this.Indicators.IndicatorCollection[i].BFPContractIndicator = report.Indicators.IndicatorCollection[i].BFPContractIndicator;
                    }

                    report.Indicators.IndicatorCollection = this.Indicators.IndicatorCollection;
                }
            }

            #endregion

            #region Team

            if (!(report.Team != null && report.Team.isLocked))
            {
                if (this.Team == null)
                    report.Team.TeamMemberCollection = new R_10044.TechnicalReportTeamMemberCollection();
                else
                    report.Team.TeamMemberCollection = this.Team.TeamMemberCollection;
            }

            #endregion

            #region AttachedDocuments

            if (!(report.AttachedDocuments != null && report.AttachedDocuments.isLocked))
            {
                if (this.AttachedDocuments == null)
                    report.AttachedDocuments.AttachedDocumentCollection = new R_10044.AttachedDocumentCollection();
                else
                    report.AttachedDocuments.AttachedDocumentCollection = this.AttachedDocuments.AttachedDocumentCollection;
            }

            #endregion

            return report;
        }

        #endregion
    }
}