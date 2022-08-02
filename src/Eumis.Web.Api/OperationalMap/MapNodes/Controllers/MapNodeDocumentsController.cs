using System;
using System.Collections.Generic;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.MapNodes.Repositories;
using Eumis.Data.OperationalMap.MapNodes.ViewObjects;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Web.Api.OperationalMap.MapNodes.DataObjects;

namespace Eumis.Web.Api.OperationalMap.MapNodes.Controllers
{
    public abstract class MapNodeDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IMapNodesRepository mapNodesRepository;
        private IAuthorizer authorizer;

        protected MapNodeDocumentsController(IUnitOfWork unitOfWork, IMapNodesRepository mapNodesRepository, IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.mapNodesRepository = mapNodesRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public virtual IList<MapNodeDocumentVO> GetMapNodeDocuments(int mapNodeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, mapNodeId);

            return this.mapNodesRepository.GetMapNodeDocuments(mapNodeId);
        }

        [Route("{documentId:int}")]
        public virtual MapNodeDocumentDO GetMapNodeDocument(int mapNodeId, int documentId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, mapNodeId);

            var mapNode = this.mapNodesRepository.Find(mapNodeId);

            var mapNodeDocument = mapNode.FindMapNodeDocument(documentId);

            return new MapNodeDocumentDO(mapNodeDocument, mapNode.Version);
        }

        [HttpGet]
        [Route("new")]
        public virtual MapNodeDocumentDO NewMapNodeDocument(int mapNodeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, mapNodeId);

            var mapNode = this.mapNodesRepository.Find(mapNodeId);

            return new MapNodeDocumentDO(mapNodeId, mapNode.Version);
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLogSuffix(SuffixAction = "Edit", IdParam = "mapNodeId", ChildIdParam = "documentId")]
        public virtual void UpdateMapNodeDocument(int mapNodeId, int documentId, MapNodeDocumentDO mapNodeDocument)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, mapNodeId);

            var mapNode = this.mapNodesRepository.FindForUpdate(mapNodeId, mapNodeDocument.Version);

            mapNode.UpdateMapNodeDocument(
                documentId,
                mapNodeDocument.Name,
                mapNodeDocument.Description,
                mapNodeDocument.File != null ? mapNodeDocument.File.Key : (Guid?)null);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLogSuffix(SuffixAction = "Create", IdParam = "mapNodeId")]
        public virtual void AddMapNodeDocument(int mapNodeId, MapNodeDocumentDO mapNodeDocument)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, mapNodeId);

            var mapNode = this.mapNodesRepository.FindForUpdate(mapNodeId, mapNodeDocument.Version);

            mapNode.AddMapNodeDocument(
                mapNodeDocument.Name,
                mapNodeDocument.Description,
                mapNodeDocument.File != null ? mapNodeDocument.File.Key : (Guid?)null);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLogSuffix(SuffixAction = "Delete", IdParam = "mapNodeId", ChildIdParam = "documentId")]
        public virtual void DeleteMapNodeDocument(int mapNodeId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, mapNodeId);

            byte[] vers = System.Convert.FromBase64String(version);
            MapNode mapNode = this.mapNodesRepository.FindForUpdate(mapNodeId, vers);

            mapNode.RemoveMapNodeDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
