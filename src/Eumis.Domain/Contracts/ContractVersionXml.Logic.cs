using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Eumis.Rio;
using KellermanSoftware.CompareNetObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Eumis.Domain.Contracts
{
    public partial class ContractVersionXml
    {
        public override void SetXml(string xml)
        {
            if (this.Status != ContractVersionStatus.Draft)
            {
                throw new DomainValidationException("Cannot update non-draft contract's xml");
            }

            var isPartialModification = false;
            BFPContract oldVersion = null;

            if (this.VersionType == ContractVersionType.PartialAnnex || this.VersionType == ContractVersionType.PartialChange)
            {
                isPartialModification = true;
                oldVersion = this.GetDocument();
            }

            this.ModifyDate = DateTime.Now;
            base.SetXml(xml);

            var versionDoc = this.GetDocument();

            if (isPartialModification && !PartialModificationsAsserted(oldVersion, versionDoc))
            {
                throw new DomainValidationException("Unexpected differences in contract's xml");
            }

            this.Name = versionDoc.BFPContractBasicData.Name;
            this.ExecutionStatus = versionDoc.GetEnum<Rio.BFPContract, ContractExecutionStatus>(c => c.BFPContractBasicData.CompletionStatus?.Id);
            this.ContractDate = versionDoc.BFPContractBasicData.ContractDate;
            this.StartDate = versionDoc.BFPContractBasicData.StartDate;
            this.StartConditions = versionDoc.BFPContractBasicData.StartCondition;
            this.CompletionDate = versionDoc.BFPContractBasicData.CompletionDate;
            this.TerminationDate = versionDoc.BFPContractBasicData.TerminationDate;
            this.TerminationReason = versionDoc.BFPContractBasicData.TerminationReason;

            this.TotalEuAmount = versionDoc.BFPContractDirectionsBudgetContract.BFPContractBudget.EUAmount;
            this.TotalBgAmount = versionDoc.BFPContractDirectionsBudgetContract.BFPContractBudget.NationalAmount;
            this.TotalBfpAmount = versionDoc.BFPContractDirectionsBudgetContract.BFPContractBudget.GrandAmount;
            this.TotalSelfAmount = versionDoc.BFPContractDirectionsBudgetContract.BFPContractBudget.SelfAmount;
            this.TotalAmount = versionDoc.BFPContractDirectionsBudgetContract.BFPContractBudget.TotalAmount;
        }

        public void SetAttributes(string createNote)
        {
            this.CreateNote = createNote;

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeContractDate(DateTime contractDate)
        {
            var currentDate = DateTime.Now;
            this.ContractDate = contractDate;
            this.SetDocumentAttributes(this.GetDocument(), currentDate);
            ((IEventEmitter)this).Events.Add(new ContractVersionContractDateChangedEvent() { ContractVersionId = this.ContractVersionXmlId });

            this.ModifyDate = currentDate;
        }

        public void ChangeStatusToDraft()
        {
            if (this.Status != ContractVersionStatus.Entered)
            {
                throw new DomainValidationException("Status transition not allowed.");
            }

            this.Status = ContractVersionStatus.Draft;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToEntered()
        {
            if (this.Status != ContractVersionStatus.Draft)
            {
                throw new DomainValidationException("Status transition not allowed.");
            }

            this.Status = ContractVersionStatus.Entered;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToActive()
        {
            if (this.Status != ContractVersionStatus.Entered)
            {
                throw new DomainValidationException("Status transition not allowed.");
            }

            var versionDoc = this.GetDocument();
            this.ContractDate = versionDoc.BFPContractBasicData.ContractDate;

            this.Status = ContractVersionStatus.Active;
            this.ModifyDate = DateTime.Now;

            ((IEventEmitter)this).Events.Add(new ContractVersionActivatedEvent() { ContractVersionId = this.ContractVersionXmlId });
        }

        public void ChangeStatusToArchived()
        {
            if (this.Status != ContractVersionStatus.Active)
            {
                throw new DomainValidationException("Status transition not allowed.");
            }

            this.Status = ContractVersionStatus.Archived;
            this.ModifyDate = DateTime.Now;
        }

        private void SetDocumentAttributes(Rio.BFPContract versionDoc, DateTime currentDate)
        {
            // change the document modificationDate so that a unique hash is produced
            versionDoc.modificationDate = currentDate;
            versionDoc.BFPContractBasicData.RegistrationNumber = this.RegNumber;
            versionDoc.BFPContractBasicData.Version = this.VersionNum.ToString();
            versionDoc.BFPContractBasicData.SubVersion = this.VersionSubNum.ToString();
            versionDoc.BFPContractBasicData.ContractDate = this.ContractDate;

            this.SetXml(versionDoc);
        }

        public static string CreateRegNumber(ContractDataDO contractData, int versionNum)
        {
            if (contractData.IsPrimaryProgramme)
            {
                return string.Format("{0}-C{1:00}", contractData.ProjectRegNumber, versionNum);
            }
            else
            {
                return string.Format("{0}-{1}-C{2:00}", contractData.ProjectRegNumber, contractData.ProgrammeCode, versionNum);
            }
        }

        public void AddOrUpdateAmount(
            int contractId,
            int procedureBudgetLevel2Id,
            int orderNum,
            Guid gid,
            bool isActive,
            string name,
            decimal contractBfpAmount,
            decimal contractSelfAmount,
            string nutsCode,
            string nutsName,
            string nutsFullPath,
            string nutsFullPathName)
        {
            var item = this.ContractVersionXmlAmounts.FirstOrDefault(i => i.Gid == gid);
            if (item != null)
            {
                item.OrderNum = orderNum;
                item.IsActive = isActive;

                item.Name = name;

                item.ContractEuAmount = 0;
                item.ContractBgAmount = contractBfpAmount;
                item.ContractSelfAmount = contractSelfAmount;
                item.CurrentEuAmount = 0;
                item.CurrentBgAmount = contractBfpAmount;
                item.CurrentSelfAmount = contractSelfAmount;

                item.NutsCode = nutsCode;
                item.NutsName = nutsName;
                item.NutsFullPath = nutsFullPath;
                item.NutsFullPathName = nutsFullPathName;
            }
            else
            {
                this.ContractVersionXmlAmounts.Add(
                    new ContractVersionXmlAmount(
                        contractId,
                        procedureBudgetLevel2Id,
                        orderNum,
                        gid,
                        isActive,
                        name,
                        0,
                        contractBfpAmount,
                        contractSelfAmount,
                        0,
                        contractBfpAmount,
                        contractSelfAmount,
                        nutsCode,
                        nutsName,
                        nutsFullPath,
                        nutsFullPathName));
            }
        }

        private static bool PartialModificationsAsserted(BFPContract oldVersion, BFPContract newVersion)
        {
            if (oldVersion.BFPContractBasicData.NutsAddress.NutsAddressContentCollection.Count != newVersion.BFPContractBasicData.NutsAddress.NutsAddressContentCollection.Count)
            {
                return false;
            }

            if (oldVersion.Beneficiary.Uin != newVersion.Beneficiary.Uin ||
                oldVersion.Beneficiary.Name != newVersion.Beneficiary.Name ||
                oldVersion.Beneficiary.NameEN != newVersion.Beneficiary.NameEN)
            {
                return false;
            }

            var compareLogic = new CompareLogic();

            if (!compareLogic.Compare(oldVersion.Partners, newVersion.Partners).AreEqual ||
                !compareLogic.Compare(oldVersion.BFPContractContractActivities, newVersion.BFPContractContractActivities).AreEqual ||
                !compareLogic.Compare(oldVersion.BFPContractIndicators, newVersion.BFPContractIndicators).AreEqual)
            {
                return false;
            }

            compareLogic.Config.MembersToIgnore.Add("BFPContractProgrammeDetailsExpenseBudget.Nuts");
            compareLogic.Config.MembersToIgnore.Add("BFPContractProgrammeDetailsExpenseBudget.__Nuts");
            if (!compareLogic.Compare(oldVersion.BFPContractDirectionsBudgetContract, newVersion.BFPContractDirectionsBudgetContract).AreEqual)
            {
                return false;
            }

            for (int i = 0; i < oldVersion.BFPContractBasicData.NutsAddress.NutsAddressContentCollection.Count; i++)
            {
                var oldLocation = GetLocationFromNutsAddressContent(oldVersion.BFPContractBasicData.NutsAddress.NutsAddressContentCollection[i], oldVersion.BFPContractBasicData.NutsAddress.NutsLevel.Id);
                var newLocation = GetLocationFromNutsAddressContent(newVersion.BFPContractBasicData.NutsAddress.NutsAddressContentCollection[i], newVersion.BFPContractBasicData.NutsAddress.NutsLevel.Id);
                var comparisonFailed = false;

                Parallel.For(0, oldVersion.BFPContractDirectionsBudgetContract.BFPContractBudget.BFPContractProgrammeBudgetCollection.Count, j =>
                {
                    Parallel.For(0, oldVersion.BFPContractDirectionsBudgetContract.BFPContractBudget.BFPContractProgrammeBudgetCollection[j].BFPContractProgrammeExpenseBudgetCollection.Count, k =>
                    {
                        Parallel.For(0, oldVersion.BFPContractDirectionsBudgetContract.BFPContractBudget.BFPContractProgrammeBudgetCollection[j].BFPContractProgrammeExpenseBudgetCollection[k].BFPContractProgrammeDetailsExpenseBudgetCollection.Count, l =>
                        {
                            if (oldVersion.BFPContractDirectionsBudgetContract.BFPContractBudget.BFPContractProgrammeBudgetCollection[j].BFPContractProgrammeExpenseBudgetCollection[k].BFPContractProgrammeDetailsExpenseBudgetCollection[l].Nuts.Code == oldLocation.Code &&
                                newVersion.BFPContractDirectionsBudgetContract.BFPContractBudget.BFPContractProgrammeBudgetCollection[j].BFPContractProgrammeExpenseBudgetCollection[k].BFPContractProgrammeDetailsExpenseBudgetCollection[l].Nuts.Code != newLocation.Code)
                            {
                                comparisonFailed = true;
                            }
                        });
                    });
                });

                if (comparisonFailed)
                {
                    return false;
                }
            }

            return true;
        }

        private static Location GetLocationFromNutsAddressContent(NutsAddressContent nutsAddressContent, string id)
        {
            Location location;

            switch (id)
            {
                case "country":
                    location = nutsAddressContent.Country;
                    break;

                case "protectedZone":
                    location = nutsAddressContent.ProtectedZone;
                    break;

                case "regionNUTS1":
                    location = nutsAddressContent.Nuts1;
                    break;

                case "regionNUTS2":
                    location = nutsAddressContent.Nuts2;
                    break;

                case "district":
                    location = nutsAddressContent.District;
                    break;

                case "municipality":
                    location = nutsAddressContent.Municipality;
                    break;

                case "settlement":
                    location = nutsAddressContent.Settlement;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(id), "Invalid level id");
            }

            return location;
        }
    }
}
