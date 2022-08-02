using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Procedures;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Procedures.DataObjects;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedures/{procedureId}/questions")]
    public class ProcedureQuestionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProceduresRepository proceduresRepository;
        private IAuthorizer authorizer;
        private IUserClaimsContext currentUserClaimsContext;
        private IAccessContext accessContext;
        private UserClaimsContextFactory userClaimsContextFactory;

        public ProcedureQuestionsController(
            IUnitOfWork unitOfWork,
            IProceduresRepository proceduresRepository,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            UserClaimsContextFactory userClaimsContextFactory)
        {
            this.unitOfWork = unitOfWork;
            this.proceduresRepository = proceduresRepository;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.userClaimsContextFactory = userClaimsContextFactory;

            if (accessContext.IsUser)
            {
                this.currentUserClaimsContext = this.userClaimsContextFactory(accessContext.UserId);
            }
        }

        [Route("")]
        public IList<ProcedureQuestionsVO> GetProcedureQuestions(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetProcedureQuestions(procedureId);
        }

        [Route("{questionId:int}")]
        public ProcedureQuestionDO GetProcedureQuestion(int procedureId, int questionId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var procedureQuestion = procedure.FindProcedureQuestion(questionId);

            var userClaimsContext = this.userClaimsContextFactory(procedureQuestion.CreatedByUserId);
            var username = userClaimsContext.Fullname + "(" + userClaimsContext.Username + ")";

            return new ProcedureQuestionDO(procedureQuestion, username, procedure.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureQuestionDO NewProcedureQuestion(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var username = this.currentUserClaimsContext.Fullname + "(" + this.currentUserClaimsContext.Username + ")";

            return new ProcedureQuestionDO(procedureId, username, procedure.Version)
            {
                CreateDate = DateTime.Now,
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Questions.Create), IdParam = "procedureId")]
        public void AddProcedureQuestion(int procedureId, ProcedureQuestionDO question)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, question.Version);

            procedure.AddProcedureQuestion(
                question.File.Key,
                this.accessContext.UserId);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{questionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Questions.Delete), IdParam = "procedureId", ChildIdParam = "questionId")]
        public void DeleteProcedureQuestion(int procedureId, int questionId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureQuestion(questionId);

            this.unitOfWork.Save();
        }
    }
}
