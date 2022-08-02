using Eumis.Common.Validation;
using Eumis.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eumis.Portal.Web.Models.MessageSend
{
    public class EditVM : BaseVM, IEditVM<R_10020.Message>, IEngineValidatable
    {
        public string ProjectRegNumber { get; set; }
        public string Content { get; set; }

        public DateTime? EndingDate { get; set; }

        public Guid ProjectCommunicationGid { get; set; }

        public List<R_10018.AttachedDocument> ContentAttachedDocumentCollection { get; set; }
        public R_10019.Project Project { get; set; }

        #region Get Set

        public EditVM() { }

        public EditVM(R_10020.Message message)
        {
            if (message != null)
            {
                this.Content = message.Content;
                this.ContentAttachedDocumentCollection = message.ContentAttachedDocumentCollection;
                this.ProjectCommunicationGid = message.ProjectCommunicationGid;
                this.Project = message.Project;
                this.ProjectRegNumber = message.MessageHeader.regNumber;
                this.EndingDate = message.EndingDate;
            }
        }

        public R_10020.Message Set(R_10020.Message message)
        {
            message.modificationDate = DateTime.Now;
            message.Content = this.Content;
            message.EndingDate = this.EndingDate;

            message.Project.ProjectBasicData.isLocked = this.Project.ProjectBasicData.isLocked;
            message.Project.Candidate.isLocked = this.Project.Candidate.isLocked;


            message.Project.Partners.isLocked = this.Project.Partners?.isLocked ?? false;

            for (int i = 0; i < message.Project.DirectionsBudgetContractCollection.Count; i++)
            {
                message.Project.DirectionsBudgetContractCollection[i].isLocked = true;
            }

            for (int i = 0; i < this.Project.ProgrammeContractActivitiesCollection.Count; i++)
            {
                message.Project.ProgrammeContractActivitiesCollection[i].isLocked = this.Project.ProgrammeContractActivitiesCollection[i]?.isLocked ?? false;
            }

            for (int i = 0; i < this.Project.ProgrammeIndicatorsCollection.Count; i++)
            {
                message.Project.ProgrammeIndicatorsCollection[i].isLocked = this.Project.ProgrammeIndicatorsCollection[i]?.isLocked ?? false;
            }

            message.Project.ContractTeams.isLocked = this.Project.ContractTeams?.isLocked ?? false;
            message.Project.ProjectErrands.isLocked = this.Project.ProjectErrands?.isLocked ?? false;

            if (message.Project.ProjectSpecFields != null && this.Project.ProjectSpecFields != null)
                message.Project.ProjectSpecFields.isLocked = this.Project.ProjectSpecFields.isLocked;

            if (message.Project.ElectronicDeclarations != null && this.Project.ElectronicDeclarations != null)
                message.Project.ElectronicDeclarations.isLocked = this.Project.ElectronicDeclarations.isLocked;

            if (message.Project.AttachedDocuments != null && this.Project.AttachedDocuments != null)
                message.Project.AttachedDocuments.isLocked = this.Project.AttachedDocuments.isLocked;

            message.type = R_09990.MessageTypeNomenclature.Question;

            message.ContentAttachedDocumentCollection = new R_10020.AttachedDocumentCollection();
            if (this.ContentAttachedDocumentCollection != null)
                message.ContentAttachedDocumentCollection.AddRange(this.ContentAttachedDocumentCollection);

            return message;
        }

        #endregion
    }
}