using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.Companies;
using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.Contracts
{
    public partial class Contract
    {
        public void UpdateContractData(
            string regNumber,
            int companyId,
            string companyName,
            string companyNameAlt,
            UinType companyUinType,
            string companyUin,
            int companyTypeId,
            int? companyLegalTypeId,
            string companyEmail,
            string name,
            string nameEN,
            string description,
            string descriptionEN,
            ContractExecutionStatus? executionStatus,
            DateTime? contractDate,
            DateTime? startDate,
            string startConditions,
            DateTime? completionDate,
            DateTime? terminationDate,
            string terminationReason,
            NutsLevel? nutsLevel,
            int? duration,
            int? projectKidCodeId,
            IList<Tuple<string, string, string, string, string, string>> locations,
            int? beneficiarySeatCountryId,
            int? beneficiarySeatSettlementId,
            string beneficiarySeatPostCode,
            string beneficiarySeatStreet,
            string beneficiarySeatAddress,
            int? beneficiaryCorrespondenceCountryId,
            int? beneficiaryCorrespondenceSettlementId,
            string beneficiaryCorrespondencePostCode,
            string beneficiaryCorrespondenceStreet,
            string beneficiaryCorrespondenceAddress,
            decimal? totalEuAmount,
            decimal? totalBgAmount,
            decimal? totalBfpAmount,
            decimal? totalSelfAmount,
            decimal? totalAmount)
        {
            this.RegNumber = regNumber;
            this.CompanyId = companyId;
            this.CompanyName = companyName;
            this.CompanyNameAlt = companyNameAlt;
            this.CompanyUinType = companyUinType;
            this.CompanyUin = companyUin;
            this.CompanyTypeId = companyTypeId;
            this.CompanyLegalTypeId = companyLegalTypeId;
            this.CompanyEmail = companyEmail;
            this.Name = name;
            this.NameEN = nameEN;
            this.Description = description;
            this.DescriptionEN = descriptionEN;
            this.ExecutionStatus = executionStatus;
            this.ContractDate = contractDate;
            this.StartDate = startDate;
            this.StartConditions = startConditions;
            this.CompletionDate = completionDate;
            this.TerminationDate = terminationDate;
            this.TerminationReason = terminationReason;

            this.NutsLevel = nutsLevel;
            this.Duration = duration;
            this.ProjectKidCodeId = projectKidCodeId;

            this.BeneficiarySeatCountryId = beneficiarySeatCountryId;
            this.BeneficiarySeatSettlementId = beneficiarySeatSettlementId;
            this.BeneficiarySeatPostCode = beneficiarySeatPostCode;
            this.BeneficiarySeatStreet = beneficiarySeatStreet;
            this.BeneficiarySeatAddress = beneficiarySeatAddress;

            this.BeneficiaryCorrespondenceCountryId = beneficiaryCorrespondenceCountryId;
            this.BeneficiaryCorrespondenceSettlementId = beneficiaryCorrespondenceSettlementId;
            this.BeneficiaryCorrespondencePostCode = beneficiaryCorrespondencePostCode;
            this.BeneficiaryCorrespondenceStreet = beneficiaryCorrespondenceStreet;
            this.BeneficiaryCorrespondenceAddress = beneficiaryCorrespondenceAddress;

            this.TotalEuAmount = totalEuAmount;
            this.TotalBgAmount = totalBgAmount;
            this.TotalBfpAmount = totalBfpAmount;
            this.TotalSelfAmount = totalSelfAmount;
            this.TotalAmount = totalAmount;

            this.ContractLocations.Clear();
            foreach (var l in locations)
            {
                this.ContractLocations.Add(new ContractLocation(l.Item1, l.Item2, l.Item3, l.Item4, l.Item5, l.Item6));
            }

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateContractDate(DateTime? contractDate)
        {
            this.ContractDate = contractDate;

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToEntered()
        {
            if (this.ContractStatus != ContractStatus.Draft)
            {
                throw new DomainValidationException("Status transition not allowed.");
            }

            this.ContractStatus = ContractStatus.Entered;
            this.ModifyDate = DateTime.Now;
        }

        public void AddContractRegistration(
            int contractRegistrationId,
            int createUserId,
            Guid blobKey)
        {
            this.ContractRegistrations.Add(new ContractsContractRegistration()
            {
                ContractRegistrationId = contractRegistrationId,
                CreatedByUserId = createUserId,
                CreateDate = DateTime.Now,
                BlobKey = blobKey,
                IsActive = true,
            });

            this.ModifyDate = DateTime.Now;

            ((IEventEmitter)this).Events.Add(new ContractContractRegistrationActivatedEvent()
            {
                ContractId = this.ContractId,
                ContractRegistrationId = contractRegistrationId,
            });
        }

        public void UpdateContractRegistration(
            int contractsContractRegistrationId,
            Guid blobKey)
        {
            var contractRegistration = this.FindContractContractRegistration(contractsContractRegistrationId);

            contractRegistration.BlobKey = blobKey;

            this.ModifyDate = DateTime.Now;
        }

        public void ActivateContractRegistration(int contractsContractRegistrationId)
        {
            var contractRegistration = this.FindContractContractRegistration(contractsContractRegistrationId);

            this.AssertIsContractRegistrationNotActive(contractRegistration);

            contractRegistration.IsActive = true;

            this.ModifyDate = DateTime.Now;

            ((IEventEmitter)this).Events.Add(new ContractContractRegistrationActivatedEvent()
            {
                ContractId = this.ContractId,
                ContractRegistrationId = contractRegistration.ContractRegistrationId,
            });
        }

        public void DeactivateContractRegistration(int contractsContractRegistrationId)
        {
            var contractRegistration = this.FindContractContractRegistration(contractsContractRegistrationId);

            this.AssertIsContractRegistrationActive(contractRegistration);

            contractRegistration.IsActive = false;

            this.ModifyDate = DateTime.Now;

            ((IEventEmitter)this).Events.Add(new ContractContractRegistrationDeactivatedEvent()
            {
                ContractId = this.ContractId,
                ContractRegistrationId = contractRegistration.ContractRegistrationId,
            });
        }

        public ContractsContractRegistration FindContractContractRegistration(int contractsContractRegistrationId)
        {
            return this.ContractRegistrations.SingleOrDefault(e => e.ContractsContractRegistrationId == contractsContractRegistrationId);
        }

        public void AddOrUpdateBudgetItem(
            int procedureBudgetLevel2Id,
            int orderNum,
            Guid gid,
            bool isActive,
            string code,
            string name,
            decimal contractBfpAmount,
            decimal contractSelfAmount,
            string nutsCode,
            string nutsName,
            string nutsFullPath,
            string nutsFullPathName,
            int? directionId,
            int? subDirectionId)
        {
            var item = this.ContractBudgetLevel3Amounts.FirstOrDefault(i => i.Gid == gid);
            if (item != null)
            {
                item.OrderNum = orderNum;
                item.IsActive = isActive;

                item.Code = code;
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
                item.DirectionId = directionId;
                item.SubDirectionId = subDirectionId;
            }
            else
            {
                this.ContractBudgetLevel3Amounts.Add(
                    new ContractBudgetLevel3Amount(
                        procedureBudgetLevel2Id,
                        orderNum,
                        gid,
                        isActive,
                        code,
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
                        nutsFullPathName,
                        directionId,
                        subDirectionId));
            }
        }

        public void AddOrUpdatePartner(
            Guid gid,
            bool isActive,
            decimal financialContribution,
            string uin,
            UinType uinType,
            int companyTypeId,
            int companyLegalTypeId,
            string name,
            string nameAlt,
            int? seatCountryId,
            int? seatSettlementId,
            string seatPostCode,
            string seatStreet,
            string seatAddress,
            int? corrCountryId,
            int? corrSettlementId,
            string corrPostCode,
            string corrStreet,
            string corrAddress,
            string representative,
            string phone1,
            string phone2,
            string fax,
            string email,
            string contactName,
            string contactPhone,
            string contactEmail)
        {
            var partner = this.ContractPartners.FirstOrDefault(i => i.Gid == gid);
            if (partner != null)
            {
                partner.Gid = gid;
                partner.IsActive = isActive;
                partner.FinancialContribution = financialContribution;
                partner.Uin = uin;
                partner.UinType = uinType;
                partner.CompanyLegalTypeId = companyLegalTypeId;
                partner.Name = name;
                partner.NameAlt = nameAlt;
                partner.CompanyTypeId = companyTypeId;
                partner.SeatCountryId = seatCountryId;
                partner.SeatSettlementId = seatSettlementId;
                partner.SeatPostCode = seatPostCode;
                partner.SeatStreet = seatStreet;
                partner.SeatAddress = seatAddress;
                partner.CorrCountryId = corrCountryId;
                partner.CorrSettlementId = corrSettlementId;
                partner.CorrPostCode = corrPostCode;
                partner.CorrStreet = corrStreet;
                partner.CorrAddress = corrAddress;
                partner.Representative = representative;
                partner.Phone1 = phone1;
                partner.Phone2 = phone2;
                partner.Fax = fax;
                partner.Email = email;
                partner.ContactName = contactName;
                partner.ContactPhone = contactPhone;
                partner.ContactEmail = contactEmail;
            }
            else
            {
                this.ContractPartners.Add(
                    new ContractPartner(
                        gid,
                        isActive,
                        financialContribution,
                        uin,
                        uinType,
                        companyTypeId,
                        companyLegalTypeId,
                        name,
                        nameAlt,
                        seatCountryId,
                        seatSettlementId,
                        seatPostCode,
                        seatStreet,
                        seatAddress,
                        corrCountryId,
                        corrSettlementId,
                        corrPostCode,
                        corrStreet,
                        corrAddress,
                        representative,
                        phone1,
                        phone2,
                        fax,
                        email,
                        contactName,
                        contactPhone,
                        contactEmail));
            }
        }

        public void AddOrUpdateActivity(
            Guid gid,
            bool isActive,
            string code,
            string name,
            string executionMethod,
            string result,
            int startMonth,
            int duration,
            decimal amount,
            IList<Tuple<string, UinType, string>> companies)
        {
            var activity = this.ContractActivities.FirstOrDefault(i => i.Gid == gid);
            if (activity != null)
            {
                activity.IsActive = isActive;
                activity.Code = code;
                activity.Name = name;
                activity.ExecutionMethod = executionMethod;
                activity.Result = result;
                activity.StartMonth = startMonth;
                activity.Duration = duration;
                activity.Amount = amount;

                activity.ContractActivityCompanies.Clear();
                foreach (var c in companies)
                {
                    activity.ContractActivityCompanies.Add(
                        new ContractActivityCompany(
                            c.Item1,
                            c.Item2,
                            c.Item3));
                }
            }
            else
            {
                this.ContractActivities.Add(
                    new ContractActivity(
                        gid,
                        isActive,
                        code,
                        name,
                        executionMethod,
                        result,
                        startMonth,
                        duration,
                        amount,
                        companies));
            }
        }

        public void AddOrUpdateContractor(
            Guid gid,
            bool isActive,
            string uin,
            UinType uinType,
            string name,
            string nameAlt,
            int? seatCountryId,
            int? seatSettlementId,
            string seatPostCode,
            string seatStreet,
            string seatAddress,
            YesNoNonApplicable vatRegistration)
        {
            var contractor = this.ContractContractors.FirstOrDefault(i => i.Gid == gid);
            if (contractor != null)
            {
                contractor.IsActive = isActive;
                contractor.Uin = uin;
                contractor.UinType = uinType;
                contractor.Name = name;
                contractor.NameAlt = nameAlt;
                contractor.SeatCountryId = seatCountryId;
                contractor.SeatSettlementId = seatSettlementId;
                contractor.SeatPostCode = seatPostCode;
                contractor.SeatStreet = seatStreet;
                contractor.SeatAddress = seatAddress;
                contractor.VATRegistration = vatRegistration;
            }
            else
            {
                this.ContractContractors.Add(
                    new ContractContractor(
                        gid,
                        isActive,
                        uin,
                        uinType,
                        name,
                        nameAlt,
                        seatCountryId,
                        seatSettlementId,
                        seatPostCode,
                        seatStreet,
                        seatAddress,
                        vatRegistration));
            }
        }

        public void AddOrUpdateContract(
            Guid gid,
            bool isActive,
            DateTime signDate,
            string number,
            decimal totalAmountExcludingVAT,
            decimal vatAmountIfEligible,
            decimal totalFundedValue,
            int numberAnnexes,
            decimal currentAnnexTotalAmount,
            string comment,
            DateTime startDate,
            DateTime endDate,
            bool hasSubcontractorMember,
            Guid contractorGid,
            IList<Tuple<Guid, ContractSubcontractType, DateTime, string, decimal>> subcontracts,
            IList<Tuple<Guid?, Guid>> activities)
        {
            var contract = this.ContractContracts.FirstOrDefault(i => i.Gid == gid);
            if (contract != null)
            {
                contract.IsActive = isActive;
                contract.SignDate = signDate;
                contract.Number = number;
                contract.TotalAmountExcludingVAT = totalAmountExcludingVAT;
                contract.VATAmountIfEligible = vatAmountIfEligible;
                contract.TotalFundedValue = totalFundedValue;
                contract.NumberAnnexes = numberAnnexes;
                contract.CurrentAnnexTotalAmount = currentAnnexTotalAmount;
                contract.Comment = comment;
                contract.StartDate = startDate;
                contract.EndDate = endDate;
                contract.HasSubcontractorMember = hasSubcontractorMember;
            }
            else
            {
                contract = new ContractContract(
                    gid,
                    isActive,
                    signDate,
                    number,
                    totalAmountExcludingVAT,
                    vatAmountIfEligible,
                    totalFundedValue,
                    numberAnnexes,
                    currentAnnexTotalAmount,
                    comment,
                    startDate,
                    endDate,
                    hasSubcontractorMember);
                this.ContractContracts.Add(contract);
            }

            contract.ContractContractor = this.ContractContractors.Single(cc => cc.Gid == contractorGid);

            contract.ContractSubcontracts.Clear();
            foreach (var sb in subcontracts)
            {
                var subcontract = new ContractSubcontract(sb.Item2, sb.Item3, sb.Item4, sb.Item5);
                subcontract.ContractContractor = this.ContractContractors.Single(cc => cc.Gid == sb.Item1);
                contract.ContractSubcontracts.Add(subcontract);
            }

            contract.ContractContractActivities.Clear();
            foreach (var a in activities)
            {
                var activity = new ContractContractActivity();
                if (a.Item1.HasValue)
                {
                    activity.ContractActivity = this.ContractActivities.Single(ca => ca.Gid == a.Item1);
                }
                else
                {
                    activity.ContractActivity = null;
                }

                activity.ContractBudgetLevel3Amount = this.ContractBudgetLevel3Amounts.Single(bi => bi.Gid == a.Item2);
                contract.ContractContractActivities.Add(activity);
            }
        }

        public void ClearMissingPlansAndPositions(Dictionary<Guid, List<Guid>> existingPlansAndPositions)
        {
            var removedPlans = new List<ContractProcurementPlan>();
            foreach (var plan in this.ContractProcurementPlans)
            {
                if (!existingPlansAndPositions.ContainsKey(plan.Gid))
                {
                    removedPlans.Add(plan);
                }
                else
                {
                    var removedPos = new List<ContractDifferentiatedPosition>();
                    foreach (var pos in plan.ContractDifferentiatedPositions)
                    {
                        if (!existingPlansAndPositions[plan.Gid].Contains(pos.Gid))
                        {
                            removedPos.Add(pos);
                        }
                    }

                    foreach (var pos in removedPos)
                    {
                        plan.ContractDifferentiatedPositions.Remove(pos);
                    }
                }
            }

            foreach (var plan in removedPlans)
            {
                plan.ContractDifferentiatedPositions.Clear();
                this.ContractProcurementPlans.Remove(plan);
            }
        }

        public void AddOrUpdateContractProcurementPlan(
            Guid gid,
            string name,
            int errandAreaId,
            int errandLegalActId,
            int errandTypeId,
            string description,
            decimal expectedAmount,
            string ppaNumber,
            DateTime? noticeDate,
            DateTime? offersDeadlineDate,
            DateTime? announcedDate,
            DateTime? terminatedDate)
        {
            var plan = this.ContractProcurementPlans.FirstOrDefault(i => i.Gid == gid);
            if (plan != null)
            {
                plan.Name = name;
                plan.ErrandAreaId = errandAreaId;
                plan.ErrandLegalActId = errandLegalActId;
                plan.ErrandTypeId = errandTypeId;
                plan.Description = description;
                plan.ExpectedAmount = expectedAmount;
                plan.PPANumber = ppaNumber;
                plan.NoticeDate = noticeDate;
                plan.OffersDeadlineDate = offersDeadlineDate;
                plan.AnnouncedDate = announcedDate;
                plan.TerminatedDate = terminatedDate;
            }
            else
            {
                plan = new ContractProcurementPlan(
                    gid,
                    name,
                    errandAreaId,
                    errandLegalActId,
                    errandTypeId,
                    description,
                    expectedAmount,
                    ppaNumber,
                    noticeDate,
                    offersDeadlineDate,
                    announcedDate,
                    terminatedDate);

                this.ContractProcurementPlans.Add(plan);
            }
        }

        public void AddOrUpdateContractDifferentiatedPosition(
            Guid contractProcurementPlanGid,
            Guid gid,
            Guid? contractContractGid,
            string name,
            int? submittedOffersCount,
            int? rankedOffersCount,
            string comment)
        {
            var plan = this.ContractProcurementPlans.Single(i => i.Gid == contractProcurementPlanGid);
            var pos = plan.ContractDifferentiatedPositions.FirstOrDefault(i => i.Gid == gid);
            if (pos != null)
            {
                pos.Name = name;

                if (!plan.IsErrandLegalActPms)
                {
                    pos.SubmittedOffersCount = submittedOffersCount;
                }

                pos.RankedOffersCount = rankedOffersCount;
                pos.Comment = comment;
                pos.ContractContract = this.ContractContracts.FirstOrDefault(i => i.Gid == contractContractGid);
            }
            else
            {
                pos = new ContractDifferentiatedPosition(
                        gid,
                        name,
                        submittedOffersCount,
                        rankedOffersCount,
                        comment);
                pos.ContractContract = this.ContractContracts.FirstOrDefault(i => i.Gid == contractContractGid);
                plan.ContractDifferentiatedPositions.Add(pos);
            }
        }

        public void AddOrUpdateIndicator(
            Guid gid,
            int indicatorId,
            bool isActive,
            decimal? baseTotalValue,
            decimal? baseMenValue,
            decimal? baseWomenValue,
            decimal? targetTotalValue,
            decimal? targetMenValue,
            decimal? targetWomenValue,
            string description,
            int? programmePriorityId,
            int? investmentPriorityId,
            int? specificTargetId)
        {
            var indicator = this.ContractIndicators.FirstOrDefault(i => i.Gid == gid);
            if (indicator != null)
            {
                indicator.IndicatorId = indicatorId;
                indicator.Gid = gid;
                indicator.IsActive = isActive;
                indicator.BaseTotalValue = baseTotalValue;
                indicator.BaseMenValue = baseMenValue;
                indicator.BaseWomenValue = baseWomenValue;
                indicator.TargetTotalValue = targetTotalValue;
                indicator.TargetMenValue = targetMenValue;
                indicator.TargetWomenValue = targetWomenValue;
                indicator.Description = description;
                indicator.ProgrammePriorityId = programmePriorityId;
                indicator.InvestmentPriorityId = investmentPriorityId;
                indicator.SpecificTargetId = specificTargetId;
            }
            else
            {
                indicator = new ContractIndicator(
                    gid,
                    indicatorId,
                    isActive,
                    baseTotalValue,
                    baseMenValue,
                    baseWomenValue,
                    targetTotalValue,
                    targetMenValue,
                    targetWomenValue,
                    description,
                    programmePriorityId,
                    investmentPriorityId,
                    specificTargetId);

                this.ContractIndicators.Add(indicator);
            }
        }

        private void AssertIsContractRegistrationNotActive(ContractsContractRegistration contractsContractRegistration)
        {
            if (contractsContractRegistration.IsActive)
            {
                throw new DomainValidationException("Cannot activate active ContractsContractRegistration");
            }
        }

        private void AssertIsContractRegistrationActive(ContractsContractRegistration contractsContractRegistration)
        {
            if (!contractsContractRegistration.IsActive)
            {
                throw new DomainValidationException("Cannot deactivate not active ContractsContractRegistration");
            }
        }

        #region contractUser

        public ContractUser FindContractUser(int contractUserId)
        {
            var contractUser = this.ContractUsers.Where(e => e.ContractUserId == contractUserId).SingleOrDefault();

            if (contractUser == null)
            {
                throw new DomainObjectNotFoundException("Cannot find Contract user with id " + contractUserId);
            }

            return contractUser;
        }

        public void AddContractUser(int userId)
        {
            ContractUser cu = new ContractUser(userId);

            this.ContractUsers.Add(cu);
            this.ModifyDate = DateTime.Now;
        }

        public void RemoveContractUser(int contractUserId)
        {
            var user = this.FindContractUser(contractUserId);

            this.ContractUsers.Remove(user);

            this.ModifyDate = DateTime.Now;
        }

        public int GetUserId(int contractUserId)
        {
            return this.FindContractUser(contractUserId).UserId;
        }
        #endregion

        #region contractProcurementDocument

        public ContractProcurementDocument FindContractProcurementDocument(int documentId)
        {
            var document = this.ContractProcurementDocuments.Single(d => d.ContractProcurementDocumentId == documentId);

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ContractProcurementDocument with id " + documentId);
            }

            return document;
        }

        public ContractProcurementDocument CreateContractProcurementDocument(
            string name,
            string description,
            Guid? blobKey)
        {
            var newContractProcurementDocument = new ContractProcurementDocument()
            {
                ContractId = this.ContractId,
                Name = name,
                Description = description,
                BlobKey = blobKey,
            };

            this.ContractProcurementDocuments.Add(newContractProcurementDocument);
            this.ModifyDate = DateTime.Now;

            return newContractProcurementDocument;
        }

        public void UpdateContractProcurementDocument(
            int documentId,
            string name,
            string description,
            Guid? blobKey)
        {
            var contractProcurementDocument = this.FindContractProcurementDocument(documentId);

            contractProcurementDocument.SetAttributes(
                name,
                description,
                blobKey);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveContractProcurementDocument(int documentId)
        {
            var contractProcurementDocument = this.FindContractProcurementDocument(documentId);

            this.ContractProcurementDocuments.Remove(contractProcurementDocument);
            this.ModifyDate = DateTime.Now;
        }

        #endregion contractProcurementDocument

        #region ContractGrantDocument

        public ContractGrantDocument FindContractGrantDocument(int documentId)
        {
            var document = this.ContractGrantDocuments.Single(d => d.ContractGrantDocumentId == documentId);

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ContractGrantDocument with id " + documentId);
            }

            return document;
        }

        public ContractGrantDocument CreateContractGrantDocument(
            string name,
            string description,
            Guid? blobKey)
        {
            var newContractGrantDocument = new ContractGrantDocument()
            {
                ContractId = this.ContractId,
                Name = name,
                Description = description,
                BlobKey = blobKey,
            };

            this.ContractGrantDocuments.Add(newContractGrantDocument);
            this.ModifyDate = DateTime.Now;

            return newContractGrantDocument;
        }

        public void UpdateContractGrantDocument(
            int documentId,
            string name,
            string description,
            Guid? blobKey)
        {
            var contractGrantDocument = this.FindContractGrantDocument(documentId);

            contractGrantDocument.SetAttributes(
                name,
                description,
                blobKey);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveContractGrantDocument(int documentId)
        {
            var contractGrantDocument = this.FindContractGrantDocument(documentId);

            this.ContractGrantDocuments.Remove(contractGrantDocument);
            this.ModifyDate = DateTime.Now;
        }

        #endregion ContractGrantDocument

    }
}
