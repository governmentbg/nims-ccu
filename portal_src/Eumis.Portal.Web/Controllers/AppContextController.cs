using Eumis.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Eumis.Portal.Web.Helpers;
using Eumis.Common.Helpers;
using NLog;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Controllers
{
    //[Authorize]
    //[RoutePrefix("api/appcontext")]
    public class AppContextController : ApiController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region Public

        #region Project

        [HttpPost]
        public bool UpdateBudget(int index, R_10010.Budget budget)
        {
            var project = GetProject();

            if (!project.IsPreliminary)
            {
                try
                {
                    if (project.DirectionsBudgetContractCollection[index].Budget != null && budget != null)
                    {
                        project.DirectionsBudgetContractCollection[index].Budget.Load(budget);
                    }

                    SetProject(project);
                }
                catch (Exception ex)
                {
                    Logger.Error(Helper.GetDetailedExceptionInfo(ex));
                    return false;
                }
            }

            return true;
        }

        [HttpPost]
        public bool UpdatePartners(R_10019.CompanyCollection partners)
        {
            var project = GetProject();

            if (!project.IsPreliminary)
            {
                try
                {
                    if (!(project.Partners != null && project.Partners.isLocked))
                    {
                        if (partners == null)
                            project.Partners.PartnerCollection = new R_10019.CompanyCollection();
                        else
                            project.Partners.PartnerCollection = partners;
                    }

                    SetProject(project);
                }
                catch (Exception ex)
                {
                    Logger.Error(Helper.GetDetailedExceptionInfo(ex));
                    return false;
                }
            }

            return true;
        }

        [HttpPost]
        public bool UpdateProgrammes(int index, R_09995.ContractActivityCollection programmes)
        {
            var project = GetProject();

            if (!project.IsPreliminary)
            {
                try
                {
                    if (project.ProgrammeContractActivitiesCollection != null
                            && project.ProgrammeContractActivitiesCollection[index].ContractActivityCollection != null
                            && programmes != null)
                    {
                        project.ProgrammeContractActivitiesCollection[index].ContractActivityCollection = programmes;
                    }

                    SetProject(project);
                }
                catch (Exception ex)
                {
                    Logger.Error(Helper.GetDetailedExceptionInfo(ex));
                    return false;
                }
            }

            return true;
        }

        [HttpPost]
        public bool UpdateTeams(R_10019.ContractTeamCollection teams)
        {
            var project = GetProject();

            if (!project.IsPreliminary)
            {
                try
                {
                    if (!(project.ContractTeams != null && project.ContractTeams.isLocked))
                    {
                        project.ContractTeams.ContractTeamCollection = teams;
                    }

                    SetProject(project);
                }
                catch (Exception ex)
                {
                    Logger.Error(Helper.GetDetailedExceptionInfo(ex));
                    return false;
                }
            }

            return true;
        }

        [HttpPost]
        public bool UpdateErands(R_10019.ProjectErrandCollection erands)
        {
            var project = GetProject();

            if (!project.IsPreliminary)
            {
                try
                {
                    if (!(project.ProjectErrands != null && project.ProjectErrands.isLocked))
                    {
                        project.ProjectErrands.ProjectErrandCollection = erands;
                    }

                    SetProject(project);
                }
                catch (Exception ex)
                {
                    Logger.Error(Helper.GetDetailedExceptionInfo(ex));
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region BFPContract

        [HttpPost]
        public bool SaveBFPContract(Eumis.Portal.Web.Models.BFPContract.EditVM model)
        {
            try
            {
                AppContext.Current.Document = model.SetAsync();
            }
            catch(Exception ex)
            {
                Logger.Error(Helper.GetDetailedExceptionInfo(ex));
                return false;
            }

            return true;
        }

        #endregion

        #region Procurements

        [HttpPost]
        public bool SaveProcurements(Eumis.Portal.Web.Models.Procurements.EditVM model)
        {
            try
            {
                AppContext.Current.Document = model.SetAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(Helper.GetDetailedExceptionInfo(ex));
                return false;
            }

            return true;
        }

        #endregion

        #region PaymentRequest

        [HttpPost]
        public bool SavePaymentRequest(Eumis.Portal.Web.Models.PaymentRequest.EditVM model)
        {
            try
            {
                AppContext.Current.Document = model.SetAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(Helper.GetDetailedExceptionInfo(ex));
                return false;
            }

            return true;
        }

        #endregion

        #region TechnicalReport

        [HttpPost]
        public bool SaveTechnicalReport(Eumis.Portal.Web.Models.TechnicalReport.EditVM model)
        {
            try
            {
                AppContext.Current.Document = model.SetAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(Helper.GetDetailedExceptionInfo(ex));
                return false;
            }

            return true;
        }

        #endregion

        #region FinanceReport

        [HttpPost]
        public bool SaveFinanceReport(Eumis.Portal.Web.Models.FinanceReport.EditVM model)
        {
            try
            {
                AppContext.Current.Document = model.SetAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(Helper.GetDetailedExceptionInfo(ex));
                return false;
            }

            return true;
        }

        #endregion

        #region SpendingPlan

        [HttpPost]
        public bool SaveSpendingPlan(Eumis.Portal.Web.Models.SpendingPlan.EditVM model)
        {
            try
            {
                AppContext.Current.Document = model.SetAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(Helper.GetDetailedExceptionInfo(ex));
                return false;
            }

            return true;
        }

        #endregion

        #region Offer

        [HttpPost]
        public bool SaveOffer(Eumis.Portal.Web.Models.Offer.EditVM model)
        {
            try
            {
                AppContext.Current.Document = model.SetAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(Helper.GetDetailedExceptionInfo(ex));
                return false;
            }

            return true;
        }

        #endregion

        #endregion

        #region Private

        private R_10019.Project GetProject()
        {
            if (AppContext.Current == null)
            {
                return null;
            }

            if (AppContext.Current.Document is R_10019.Project)
            {
                return (R_10019.Project)AppContext.Current.Document;
            }
            else if (AppContext.Current.Document is R_10020.Message)
            {
                return ((R_10020.Message)AppContext.Current.Document).Project;
            }
            else
            {
                return null;
            }
        }

        private void SetProject(R_10019.Project project)
        {
            if (AppContext.Current.Document is R_10019.Project)
            {
                AppContext.Current.Document = project;
            }
            else if (AppContext.Current.Document is R_10020.Message)
            {
                ((R_10020.Message)AppContext.Current.Document).Project = project;
            }
        }
        #endregion
    }
}
