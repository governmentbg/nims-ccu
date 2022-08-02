using Eumis.Domain;
using Eumis.Domain.OperationalMap.Programmes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace Eumis.Domain.OperationalMap.ProgrammeDeclarations
{
    public abstract partial class ProgrammeDeclaration : IAggregateRoot
    {
        public void UpdateAttributes(string content, string contentAlt, FieldType fieldType, bool isConsentForNSIDataProviding)
        {
            this.Content = content;
            this.ContentAlt = contentAlt;
            this.FieldType = fieldType;
            this.IsConsentForNSIDataProviding = isConsentForNSIDataProviding;

            this.ModifyDate = DateTime.Now;
        }

        public void Activate()
        {
            this.IsActive = true;
            this.ModifyDate = DateTime.Now;
        }

        public void Deactivate()
        {
            this.IsActive = false;
            this.ModifyDate = DateTime.Now;
        }

        #region ProgrammedeclarationItem

        public ProgrammeDeclarationItem FindProgrammeDeclarationItem(int programmeDeclarationItemId)
        {
            var programmeDeclarationItem = this.ProgrammeDeclarationItems
                .Where(di => di.ProgrammeDeclarationItemId == programmeDeclarationItemId)
                .SingleOrDefault();

            if (programmeDeclarationItem == null)
            {
                throw new DomainObjectNotFoundException($"Cannot find ProgrammeDeclarationItem with id {programmeDeclarationItemId}.");
            }

            return programmeDeclarationItem;
        }

        public void AddProgrammeDeclarationItem(int orderNum, string content)
        {
            if (this.FieldType != FieldType.Nomenclature)
            {
                throw new InvalidOperationException("Cannot add ProgrammeDeclarationItem.");
            }

            this.ProgrammeDeclarationItems.Add(new ProgrammeDeclarationItem(orderNum, content));
        }

        public void RemoveProgrammeDeclarationItem(int programmeDeclarationItemId)
        {
            var programmeDeclarationItem = this.FindProgrammeDeclarationItem(programmeDeclarationItemId);

            this.ProgrammeDeclarationItems.Remove(programmeDeclarationItem);
        }

        public void ActivateProgrammeDeclarationItem(int programmeDeclarationItemId)
        {
            var programmeDeclarationItem = this.FindProgrammeDeclarationItem(programmeDeclarationItemId);
            programmeDeclarationItem.IsActive = true;
        }

        public void DeactivateProgrammeDeclarationItem(int programmeDeclarationItemId)
        {
            var programmeDeclarationItem = this.FindProgrammeDeclarationItem(programmeDeclarationItemId);
            programmeDeclarationItem.IsActive = false;
        }

        #endregion ProgrammedeclarationItem
    }
}
