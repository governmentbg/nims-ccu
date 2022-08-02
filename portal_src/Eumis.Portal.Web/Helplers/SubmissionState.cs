using System;
using System.Linq;
using Eumis.Documents.Enums;
using System.Collections.Generic;
using Eumis.Portal.Web.Helplers;

namespace Eumis.Portal.Web.Helpers
{
    [Serializable]
    public class SubmissionState
    {
        #region Current

        public static SubmissionState Current
        {
            get
            {
                return (SubmissionState)System.Web.HttpContext.Current.Session[SubmissionStateSessionKey];
            }
            set
            {
                System.Web.HttpContext.Current.Session[SubmissionStateSessionKey] = value;
            }
        }

        #endregion

        #region Properties

        public SubmissionStateStep CurrentStep { get; set; }

        public bool IsMessageSubmission { get; set; }

        public bool IsAcceptedDisclaimer { get; set; }
        public bool IsIsunFileLoaded { get; set; }
        public bool IsElectronicSubmission { get; set; }
        public bool IsProjectValid { get; set; }

        public Guid ProjectGid { get; set; }
        public string ProjectName { get; set; }
        public string PrecedureCode { get; set; }
        public string PrecedureName { get; set; }
        public string ProgrammeName { get; set; }
        public RegistrationTypeNomenclature RegistrationType { get; set; }

        public string Hash { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; }

        public object Document { get; set; }
        public string Xml { get; set; }

        public IsunFile isunFile { get; set; }

        public Dictionary<string, KeyValuePair<string, byte[]>> signatureFiles
        {
            get
            {
                if (_signatureFiles == null)
                    _signatureFiles = new Dictionary<string, KeyValuePair<string, byte[]>>();

                return _signatureFiles;
            }
            set
            {
                _signatureFiles = new Dictionary<string, KeyValuePair<string, byte[]>>();
            }
        }

        #region ProjectCommunicationAnswer

        public Guid CommunicationGid { get; set; }
        public Guid AnswerGid { get; set; }
        public byte[] Version { get; set; }

        #endregion

        #endregion

        #region Methods

        public static void Clear()
        {
            if (SubmissionState.Current != null)
            {
                System.Web.HttpContext.Current.Session.Remove(SubmissionStateSessionKey);
            }
        }

        public static List<SubmissionStateBreadcrumb> GetBreadcrumbs()
        {
            if (SubmissionState.Current.IsMessageSubmission)
            {
                return GetMessageBreadcrumbs();
            }
            else
            {
                return GetProjectBreadcrumbs();
            }
        }

        public static List<SubmissionStateBreadcrumb> GetProjectBreadcrumbs()
        {
            var result = new List<SubmissionStateBreadcrumb>();

            result.Add(GetBreadcrumb(SubmissionStateStep.Disclaimer));

            if (!Current.IsIsunFileLoaded)
                result.Add(GetBreadcrumb(SubmissionStateStep.Finalized));

            result.Add(GetBreadcrumb(SubmissionStateStep.Preview));

            if (Current.RegistrationType == null || RegistrationTypeNomenclature.DigitalOrPaper.Code.Equals(Current.RegistrationType.Code))
                result.Add(GetBreadcrumb(SubmissionStateStep.HowToSubmit));

            if (Current.IsElectronicSubmission)
            {
                result.Add(GetBreadcrumb(SubmissionStateStep.Signature));
                result.Add(GetBreadcrumb(SubmissionStateStep.Finish));
            }
            else
            {
                result.Add(GetBreadcrumb(SubmissionStateStep.Paper));
            }

            return result;
        }

        public static List<SubmissionStateBreadcrumb> GetMessageBreadcrumbs()
        {
            var result = new List<SubmissionStateBreadcrumb>();

            result.Add(GetBreadcrumb(SubmissionStateStep.Preview));

            result.Add(GetBreadcrumb(SubmissionStateStep.HowToSubmit));

            if (Current.IsElectronicSubmission)
            {
                result.Add(GetBreadcrumb(SubmissionStateStep.Signature));
                result.Add(GetBreadcrumb(SubmissionStateStep.Finish));
            }
            else
            {
                result.Add(GetBreadcrumb(SubmissionStateStep.Paper));
            }

            return result;
        }

        private static SubmissionStateBreadcrumb GetBreadcrumb(SubmissionStateStep step)
        {
            return new SubmissionStateBreadcrumb(step, Current.CurrentStep.Equals(step));
        }

        #endregion

        #region Private

        private Dictionary<string, KeyValuePair<string, byte[]>> _signatureFiles;
        private static readonly string SubmissionStateSessionKey = "SubmissionStateSessionKey";

        #endregion
    }

    [Serializable]
    public enum SubmissionStateStep
    {
        Disclaimer = 1,
        Finalized = 2,
        Preview = 3,
        HowToSubmit = 4,
        SaveForPaper = 5,
        Paper = 6,
        Signature = 7,
        Finish = 8
    }

    [Serializable]
    public class SubmissionStateBreadcrumb
    {
        public SubmissionStateStep Step { get; set; }
        public bool IsActive { get; set; }

        public string Header
        {
            get
            {
                switch (this.Step)
                {
                    case SubmissionStateStep.Disclaimer: return "Подаване на проектно предложение";
                    case SubmissionStateStep.Finalized: return Eumis.Portal.Web.Views.Shared.App_LocalResources.Draft.Finalized;
                    case SubmissionStateStep.Preview: return Eumis.Common.Resources.Global.BreadcrumbsElectronicForm;
                    case SubmissionStateStep.HowToSubmit: return "Изберете начин на подаване на проектно предложение";
                    case SubmissionStateStep.Paper: return "Подаване на хартия";
                    case SubmissionStateStep.Signature: return "Електронно подаване";
                    case SubmissionStateStep.Finish: return "Успешно подадено проектно предложение";
                    default: return String.Empty;
                }
            }
        }

        public SubmissionStateBreadcrumb() { }

        public SubmissionStateBreadcrumb(SubmissionStateStep step, bool isActive)
        {
            this.Step = step;
            this.IsActive = isActive;
        }
    }

}