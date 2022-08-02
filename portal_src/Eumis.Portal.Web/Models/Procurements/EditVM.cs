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

namespace Eumis.Portal.Web.Models.Procurements
{
    public class EditVM : BaseVM, IEditVM<R_10041.Procurements>, IEngineValidatable, IRemoteValidatable
    {
        public R_10041.Contractors Contractors { get; set; }
        public R_10041.ContractContractors ContractContractors { get; set; }
        public R_10041.ProcurementPlans ProcurementPlans { get; set; }

        public string orderNum { get; set; }
        public DateTime activationDate { get; set; }

        public List<R_10000.PrivateNomenclature> ContractActivityItemCollection { get; set; }

        public List<R_10000.PrivateNomenclature> BudgetLevel3ItemCollection { get; set; }

        public List<string> RemoteValidationErrors { get; set; }
        public List<string> RemoteValidationWarnings { get; set; }

        public IList<ContractCentralProcurement> CentralProcurements { get; set; }

        #region Get Set

        public EditVM() { }

        public EditVM(R_10041.Procurements procurements)
        {
            this.Contractors = procurements.Contractors;
            this.ContractContractors = procurements.ContractContractors;
            this.ProcurementPlans = procurements.ProcurementPlans;

            this.ContractActivityItemCollection = procurements.ContractActivityItemCollection;
            this.BudgetLevel3ItemCollection = procurements.BudgetLevel3ItemCollection;

            this.orderNum = procurements.orderNum;
            this.activationDate = procurements.modificationDate;
            this.CentralProcurements = procurements.CentralProcurements;

        }

        public R_10041.Procurements Set(R_10041.Procurements procurements)
        {
            return procurements;
        }

        public R_10041.Procurements SetAsync()
        {
            var procurements = (R_10041.Procurements)AppContext.Current.Document;

            #region Contractors

            if (!(procurements.Contractors != null && procurements.Contractors.isLocked))
            {
                if (this.Contractors == null || this.Contractors.ContractorCollection == null)
                    procurements.Contractors.ContractorCollection = new R_10041.ContractorCollection();
                else
                {
                    // Check if all activated elements are still existing
                    foreach (var activatedContractActivity in this.Contractors.ContractorCollection.Where(e => e.isActivated))
                    {
                        var foundActivatedContractActivity = procurements.Contractors.ContractorCollection.FirstOrDefault(e => e.gid == activatedContractActivity.gid && e.isActivated);
                        if (foundActivatedContractActivity == null)
                            throw new Exception("Activated Contractor removed");
                    }

                    for (int i = 0; i < this.Contractors.ContractorCollection.Count; i++)
                    {
                        if (String.IsNullOrWhiteSpace(this.Contractors.ContractorCollection[i].gid))
                        {
                            throw new Exception("Contractor does not have gid");
                        }
                    }

                    var activatedGids = this.Contractors.ContractorCollection.Where(e => e.isActivated).Select(e => e.gid);
                    if (procurements.Contractors.ContractorCollection.Any(e => !activatedGids.Contains(e.gid)
                        && (e.isActivated || !e.isActive)))
                        throw new Exception("Contractor set to Activated or not active irregularly");

                    procurements.Contractors.ContractorCollection = this.Contractors.ContractorCollection;
                }
            }

            #endregion

            #region ContractContractors

            if (!(procurements.ContractContractors != null && procurements.ContractContractors.isLocked))
            {
                if (this.ContractContractors == null || this.ContractContractors.ContractContractorCollection == null)
                    procurements.ContractContractors.ContractContractorCollection = new R_10041.ContractContractorCollection();
                else
                {
                    // Check if all activated elements are still existing
                    foreach (var activatedContractActivity in this.ContractContractors.ContractContractorCollection.Where(e => e.isActivated))
                    {
                        var foundActivatedContractActivity = procurements.ContractContractors.ContractContractorCollection.FirstOrDefault(e => e.gid == activatedContractActivity.gid && e.isActivated);
                        if (foundActivatedContractActivity == null)
                            throw new Exception("Activated ContractContractor removed");
                    }

                    for (int i = 0; i < this.ContractContractors.ContractContractorCollection.Count; i++)
                    {
                        if (String.IsNullOrWhiteSpace(this.ContractContractors.ContractContractorCollection[i].gid))
                        {
                            throw new Exception("ContractContractor does not have gid");
                        }
                    }

                    var activatedGids = this.ContractContractors.ContractContractorCollection.Where(e => e.isActivated).Select(e => e.gid);
                    if (procurements.ContractContractors.ContractContractorCollection.Any(e => !activatedGids.Contains(e.gid)
                        && (e.isActivated || !e.isActive)))
                        throw new Exception("ContractContractor set to Activated or not active irregularly");

                    procurements.ContractContractors.ContractContractorCollection = this.ContractContractors.ContractContractorCollection;
                }
            }

            #endregion

            #region ProcurementPlans

            if (this.ProcurementPlans != null && this.ProcurementPlans.ProcurementPlanCollection != null)
            {
                for (int i = 0; i < this.ProcurementPlans.ProcurementPlanCollection.Count; i++)
                {
                    if (this.ProcurementPlans.ProcurementPlanCollection[i].BFPContractPlan.ErrandLegalAct == null || this.ProcurementPlans.ProcurementPlanCollection[i].BFPContractPlan.ErrandLegalAct.Id != Eumis.Documents.Constants.ProcurementPlansErrandLegalActPmsGid)
                    {
                        this.ProcurementPlans.ProcurementPlanCollection[i].IsAnnounced = false;
                        this.ProcurementPlans.ProcurementPlanCollection[i].IsTerminated = false;
                    }

                    if (String.IsNullOrWhiteSpace(this.ProcurementPlans.ProcurementPlanCollection[i].gid))
                    {
                        this.ProcurementPlans.ProcurementPlanCollection[i].gid = Guid.NewGuid().ToString();
                    }

                    if (this.ProcurementPlans.ProcurementPlanCollection[i].DifferentiatedPositionCollection != null)
                    {
                        for (int j = 0; j < this.ProcurementPlans.ProcurementPlanCollection[i].DifferentiatedPositionCollection.Count; j++)
                        {
                            if (String.IsNullOrWhiteSpace(this.ProcurementPlans.ProcurementPlanCollection[i].DifferentiatedPositionCollection[j].gid))
                            {
                                this.ProcurementPlans.ProcurementPlanCollection[i].DifferentiatedPositionCollection[j].gid = Guid.NewGuid().ToString();
                            }
                        }
                    }
                }
            }

            if (!(procurements.ProcurementPlans != null && procurements.ProcurementPlans.isLocked))
            {
                if (this.ProcurementPlans == null || this.ProcurementPlans.ProcurementPlanCollection == null)
                {
                    procurements.ProcurementPlans.ProcurementPlanCollection = new R_10041.ProcurementPlanCollection();
                }
                else
                {
                    if (procurements.ProcurementPlans.ProcurementPlanCollection
                        .Where(pp => pp.AnnouncedDate.HasValue && !this.ProcurementPlans.ProcurementPlanCollection.Where(x => x.gid == pp.gid).Any())
                        .Any())
                    {
                        throw new Exception("Announced ProcurementPlan removed");
                    }

                    // copy new procurement plans
                    var procurementPlans = new R_10041.ProcurementPlanCollection();
                    foreach (var procurementPlan in this.ProcurementPlans.ProcurementPlanCollection)
                    {
                        var existingProcurementPlan = procurements.ProcurementPlans.ProcurementPlanCollection.Where(pp => pp.gid == procurementPlan.gid).SingleOrDefault();

                        // if the procurement plan has been announced copy only the fields allowed to change
                        if (existingProcurementPlan != null && existingProcurementPlan.AnnouncedDate.HasValue)
                        {
                            existingProcurementPlan.IsTerminated = procurementPlan.IsTerminated;
                            existingProcurementPlan.AttachedDocumentCollection = procurementPlan.AttachedDocumentCollection;

                            if (existingProcurementPlan.DifferentiatedPositionCollection.Count != procurementPlan.DifferentiatedPositionCollection.Count)
                            {
                                throw new Exception("DifferentiatedPositionCollection has changed");
                            }

                            foreach (var differentiatedPosition in procurementPlan.DifferentiatedPositionCollection)
                            {
                                var existingDifferentiatedPosition = existingProcurementPlan.DifferentiatedPositionCollection.Where(dp => dp.gid == differentiatedPosition.gid).Single();

                                existingDifferentiatedPosition.RankedOffersCount = differentiatedPosition.RankedOffersCount;
                                existingDifferentiatedPosition.SubmittedOffersCount = differentiatedPosition.SubmittedOffersCount;
                                existingDifferentiatedPosition.Comment = differentiatedPosition.Comment;
                                existingDifferentiatedPosition.ContractContractor = differentiatedPosition.ContractContractor;
                            }

                            procurementPlans.Add(existingProcurementPlan);
                        }
                        // copy entire procurement plan
                        else
                        {
                            procurementPlans.Add(procurementPlan);
                        }
                    }

                    procurements.ProcurementPlans.ProcurementPlanCollection = procurementPlans;
                }
            }

            #endregion

            procurements.UpdateContractContractors();

            return procurements;
        }

        #endregion
    }
}