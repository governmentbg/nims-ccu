using System;
using System.Linq;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.OperationalMap.MapNodes
{
    public abstract partial class MapNode
    {
        #region MapNode
        protected void AssertIsDraft()
        {
            if (this.Status != MapNodeStatus.Draft)
            {
                throw new DomainValidationException("MapNode status must be 'Draft'");
            }
        }

        protected void AssertIsNotCanceled()
        {
            if (this.Status == MapNodeStatus.Canceled)
            {
                throw new DomainValidationException("MapNode status must not be 'Canceled'");
            }
        }

        public void ChangeStatusToDraft()
        {
            if (this.Status == MapNodeStatus.Draft)
            {
                throw new DomainValidationException("Status transition not allowed.");
            }

            this.Status = MapNodeStatus.Draft;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToEntered()
        {
            this.AssertIsDraft();

            this.Status = MapNodeStatus.Entered;
            this.ModifyDate = DateTime.Now;
        }

        #endregion //MapNode

        #region MapNodeDocument

        public MapNodeDocument FindMapNodeDocument(int documentId)
        {
            var document = this.MapNodeDocuments.Where(d => d.MapNodeDocumentId == documentId).SingleOrDefault();

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find MapNodeDocument with documentId " + documentId);
            }

            return document;
        }

        public void UpdateMapNodeDocument(
            int documentId,
            string name,
            string description,
            Guid? blobKey)
        {
            this.AssertIsNotCanceled();

            var document = this.FindMapNodeDocument(documentId);

            document.SetAttributes(name, description, blobKey);

            this.ModifyDate = DateTime.Now;
        }

        public void AddMapNodeDocument(
            string name,
            string description,
            Guid? blobKey)
        {
            this.AssertIsNotCanceled();

            this.MapNodeDocuments.Add(new MapNodeDocument()
            {
                MapNodeId = this.MapNodeId,
                Name = name,
                Description = description,
                BlobKey = blobKey,
            });

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveMapNodeDocument(int documentId)
        {
            this.AssertIsNotCanceled();

            var document = this.FindMapNodeDocument(documentId);

            this.MapNodeDocuments.Remove(document);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //MapNodeDocument
    }
}
