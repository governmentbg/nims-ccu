using Autofac.Features.OwnedInstances;
using Eumis.Common;
using Eumis.Common.Auth;
using Eumis.Common.Config;
using Eumis.Common.Db;
using Eumis.Common.Localization;
using Eumis.Data.Declarations.Repositories;
using Eumis.Data.Declarations.ViewObjects;
using Eumis.Data.Users.Repositories;
using Eumis.Data.Users.ViewObjects;
using Eumis.Domain.Users;
using Eumis.Web.Host.Nancy.Models;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Eumis.Web.Host.Nancy.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            this.RequiresOwinAuthentication(AuthenticationTypes.Cookie, "/login");
            this.Get("/", this.Index);
            this.Get("/gdprdeclaration", this.GetGDPRDeclaration);
            this.Get("/gdprdeclarationView", this.GetGDPRDeclarationViewOnly);
            this.Post("/gdprdeclaration", this.PostGDPRDeclarationData);
            this.Get("/declaration", this.GetUnacceptedDeclaration);
            this.Post("/declaration", this.PostUnacceptedDeclarationData);
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Required by Nancy")]
        private dynamic Index(dynamic parameters)
        {
            int userId = this.GetLoggedInUserId();
            var declarationInfo = this.GetGDPRDeclarationInfo(userId);
            if (!declarationInfo.HasAcceptedGDPRDeclaration)
            {
                return this.Context.GetRedirect("~/gdprdeclaration");
            }

            var hasDeclarationsToAccept = this.HasDeclarationsToAccept(userId);

            if (hasDeclarationsToAccept)
            {
                return this.Context.GetRedirect("~/declaration");
            }

            var portalLocation = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Api:PortalLocationForIFrame");

            portalLocation += $"/{SystemLocalization.GetPortalLanguageRoute()}";

            var model = new HomeModel
            {
                BlobServerLocation = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:BlobServerLocation"),
                MaxBlobSize = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:MaxBlobSize"),

                PortalProcedureEvalTableViewUrl = portalLocation + "/private/session/evalTable/preview?gid={{xmlGid}}",
                PortalProcedureEvalTableEditUrl = portalLocation + "/private/session/evalTable/edit?gid={{xmlGid}}",
                PortalApplicationViewUrl = portalLocation + "/private/session/project/draft?gid={{xmlGid}}",
                PortalEvalSessionSheetViewUrl = portalLocation + "/private/session/evalSheet/preview?gid={{xmlGid}}",
                PortalEvalSessionSheetEditUrl = portalLocation + "/private/session/evalSheet/edit?gid={{xmlGid}}",
                PortalEvalSessionStandpointViewUrl = portalLocation + "/private/session/standpoint/preview?gid={{xmlGid}}",
                PortalEvalSessionStandpointEditUrl = portalLocation + "/private/session/standpoint/edit?gid={{xmlGid}}",
                PortalProjectViewUrl = portalLocation + "/private/session/project/preview?gid={{xmlGid}}",
                PortalProjectEditUrl = portalLocation + "/private/session/project/edit?gid={{xmlGid}}",
                PortalProjectCommunicationViewUrl = portalLocation + "/private/session/messageSend/preview?gid={{xmlGid}}",
                PortalProjectCommunicationAnswerViewUrl = portalLocation + "/private/session/messageSend/answerPreview?communicationGid={{parentGid}}",
                PortalProjectCommunicationEditUrl = portalLocation + "/private/session/messageSend/edit?gid={{xmlGid}}",
                PortalProjectManagingAuthorityCommunicationViewUrl = portalLocation + "/private/session/projectCommunication/preview?gid={{xmlGid}}",
                PortalProjectManagingAuthorityCommunicationEditUrl = portalLocation + "/private/session/projectCommunication/edit?gid={{xmlGid}}",
                PortalProjectManagingAuthorityCommunicationAnswerViewUrl = portalLocation + @"/private/session/projectCommunicationAnswer/preview?communicationGid={{parentGid}}",
                PortalProjectManagingAuthorityCommunicationAnswerEditUrl = portalLocation + @"/private/session/projectCommunicationAnswer/edit?communicationGid={{parentGid}}",
                PortalContractViewUrl = portalLocation + "/private/session/bfpContract/preview?gid={{xmlGid}}",
                PortalContractEditUrl = portalLocation + "/private/session/bfpContract/edit?gid={{xmlGid}}",
                PortalContractEditPartialUrl = portalLocation + "/private/session/bfpContract/editpartial?gid={{xmlGid}}",
                PortalContractOfferViewUrl = portalLocation + "/private/session/offers/preview?gid={{xmlGid}}",
                PortalContractProcurementViewUrl = portalLocation + "/private/session/procurements/preview?gid={{xmlGid}}",
                PortalContractProcurementEditUrl = portalLocation + "/private/session/procurements/edit?gid={{xmlGid}}",
                PortalContractCommunicationViewUrl = portalLocation + "/private/session/communication/preview?gid={{xmlGid}}",
                PortalContractCommunicationEditUrl = portalLocation + "/private/session/communication/edit?gid={{xmlGid}}",
                PortalContractSpendingPlanViewUrl = portalLocation + "/private/session/spendingPlan/preview?gid={{xmlGid}}",
                PortalContractSpendingPlanEditUrl = portalLocation + "/private/session/spendingPlan/edit?gid={{xmlGid}}",
                PortalContractReportTechnicalViewUrl = portalLocation + "/private/session/technicalReport/preview?gid={{xmlGid}}",
                PortalContractReportTechnicalEditUrl = portalLocation + "/private/session/technicalReport/edit?gid={{xmlGid}}",
                PortalContractReportFinancialViewUrl = portalLocation + "/private/session/financeReport/preview?gid={{xmlGid}}",
                PortalContractReportFinancialEditUrl = portalLocation + "/private/session/financeReport/edit?gid={{xmlGid}}",
                PortalContractReportPaymentViewUrl = portalLocation + "/private/session/paymentRequest/preview?gid={{xmlGid}}",
                PortalContractReportPaymentEditUrl = portalLocation + "/private/session/paymentRequest/edit?gid={{xmlGid}}",
                PortalContractReportMicroType1ViewUrl = portalLocation + "/private/session/microData/type1?gid={{xmlGid}}",
                PortalContractReportMicroType2ViewUrl = portalLocation + "/private/session/microData/type2?gid={{xmlGid}}",
                PortalContractReportMicroType3ViewUrl = portalLocation + "/private/session/microData/type3?gid={{xmlGid}}",
                PortalContractReportMicroType4ViewUrl = portalLocation + "/private/session/microData/type4?gid={{xmlGid}}",
                PortalProgrammeCheckListViewUrl = portalLocation + "/private/session/checkList/preview?gid={{xmlGid}}",
                PortalProgrammeCheckListEditUrl = portalLocation + "/private/session/checkList/edit?gid={{xmlGid}}",
                PortalCheckSheetViewUrl = portalLocation + "/private/session/checkSheet/preview?gid={{xmlGid}}",
                PortalCheckSheetEditUrl = portalLocation + "/private/session/checkSheet/edit?gid={{xmlGid}}",

                PasswordRegex = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:PasswordRegex"),
                PasswordInvalidFormatMessage = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:PasswordInvalidFormatMessage"),
            };

            this.Context.NegotiationContext.WithNoCacheHeaders();

            string view;
            switch (System.Threading.Thread.CurrentThread.CurrentCulture.Name)
            {
                case SystemLocalization.Bg_BG:
                    view = "indexbg";
                    break;
                case SystemLocalization.En_GB:
                    view = "indexen";
                    break;
                default:
                    throw new System.Exception("Unsupported UICulture");
            }

            return this.View[view, model];
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Required by Nancy")]
        private dynamic GetGDPRDeclaration(dynamic parameters)
        {
            int userId = this.GetLoggedInUserId();
            var declarationInfo = this.GetGDPRDeclarationInfo(userId);
            if (declarationInfo.HasAcceptedGDPRDeclaration)
            {
                return this.Context.GetRedirect("~/");
            }

            this.Context.NegotiationContext.WithNoCacheHeaders();
            return this.View["gdprDeclaration", new GDPRDeclarationModel(declarationInfo)];
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Required by Nancy")]
        private dynamic GetGDPRDeclarationViewOnly(dynamic parameters)
        {
            int userId = this.GetLoggedInUserId();
            var declarationInfo = this.GetGDPRDeclarationInfo(userId);

            this.Context.NegotiationContext.WithNoCacheHeaders();
            return this.View["gdprDeclaration", new GDPRDeclarationModel(declarationInfo, true)];
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Required by Nancy")]
        private dynamic PostGDPRDeclarationData(dynamic parameters)
        {
            int userId = this.GetLoggedInUserId();
            var declarationInfo = this.GetGDPRDeclarationInfo(userId);
            if (declarationInfo.HasAcceptedGDPRDeclaration)
            {
                return this.Context.GetRedirect("~/");
            }

            var model = this.Bind<GDPRAcceptedModel>();
            if (model.Accepted)
            {
                using (var dependencies = this.Context.Resolve<Owned<DisposableTuple<IUnitOfWork, IUsersRepository>>>())
                {
                    (var unitOfWork, var usersRepository) = dependencies.Value;

                    User user = usersRepository.Find(userId);

                    user.AcceptGDPRDeclaration();

                    unitOfWork.Save();

                    return this.Context.GetRedirect("~/");
                }
            }

            this.Context.NegotiationContext.WithNoCacheHeaders();
            return this.View["gdprDeclaration", new GDPRDeclarationModel(declarationInfo)];
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Required by Nancy")]
        private dynamic GetUnacceptedDeclaration(dynamic parameters)
        {
            int userId = this.GetLoggedInUserId();
            var declarationInfo = this.GetDeclarationInfo(userId);

            if (declarationInfo == null)
            {
                return this.Context.GetRedirect("~/");
            }

            var accessToken = this.Request
                .Cookies
                .Where(t => t.Key == "authCookie")
                .Select(t => t.Value)
                .Single();

            this.Context.NegotiationContext.WithNoCacheHeaders();
            return this.View["declaration", new DeclarationModel(declarationInfo, accessToken)];
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Required by Nancy")]
        private dynamic PostUnacceptedDeclarationData(dynamic parameters)
        {
            int userId = this.GetLoggedInUserId();

            var model = this.Bind<DeclarationAcceptedModel>();
            if (model.Accepted)
            {
                using (var dependencies = this.Context.Resolve<Owned<DisposableTuple<IUnitOfWork, IUsersRepository>>>())
                {
                    (var unitOfWork, var usersRepository) = dependencies.Value;

                    var user = usersRepository.Find(userId);

                    var declarationInfo = this.GetDeclarationInfo(userId);

                    if (declarationInfo == null)
                    {
                        return this.Context.GetRedirect("~/");
                    }

                    if (model.DeclarationId != declarationInfo.DeclarationId)
                    {
                        return this.Context.GetRedirect("~/declaration");
                    }

                    user.AcceptDeclaration(declarationInfo.DeclarationId);

                    unitOfWork.Save();

                    var hasDeclarationsToAccept = this.HasDeclarationsToAccept(userId);

                    if (hasDeclarationsToAccept)
                    {
                        return this.Context.GetRedirect("~/declaration");
                    }
                    else
                    {
                        return this.Context.GetRedirect("~/");
                    }
                }
            }

            this.Context.NegotiationContext.WithNoCacheHeaders();
            return this.Context.GetRedirect("~/declaration");
        }

        private int GetLoggedInUserId()
        {
            return this.GetAccessContext(AuthenticationTypes.Cookie).UserId;
        }

        private UserGDPRDeclarationInfoVO GetGDPRDeclarationInfo(int userId)
        {
            using (var usersRepository = this.Context.Resolve<Owned<IUsersRepository>>())
            {
                return usersRepository.Value.GetGDPRDeclarationInfo(userId);
            }
        }

        private bool HasDeclarationsToAccept(int userId)
        {
            using (var usersRepository = this.Context.Resolve<Owned<IUsersRepository>>())
            {
                return usersRepository.Value.GetUserUnacceptedDeclarations(userId).Any();
            }
        }

        private UserUnacceptedDeclarationVO GetDeclarationInfo(int userId)
        {
            using (var usersRepository = this.Context.Resolve<Owned<IUsersRepository>>())
            {
                return usersRepository.Value.GetUserUnacceptedDeclarations(userId).FirstOrDefault();
            }
        }
    }
}
