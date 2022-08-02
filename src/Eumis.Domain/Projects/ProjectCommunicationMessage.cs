using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Eumis.Common;
using Eumis.Domain.Core;
using Eumis.Rio;

namespace Eumis.Domain.Projects
{
    public class ProjectCommunicationMessage : RioXmlDocumentWithFiles<Message, ProjectCommunicationMessageFile>
    {
        public DateTime? MessageDate { get; set; }

        public string Content { get; set; }

        public override IList<ProjectCommunicationMessageFile> XmlFiles
        {
            get
            {
                var projectCommunicationMessageDocument = this.GetDocument();

                var xmlFiles = EnumerableExtensions.Concat(
                    projectCommunicationMessageDocument.GetFiles(d => d.ContentAttachedDocumentCollection)
                    .Select(ad =>
                        new ProjectCommunicationMessageFile(ad)
                        {
                            Type = ProjectCommunicationMessageFileType.ContentAttachedDoc,
                        }),
                    projectCommunicationMessageDocument.GetFiles(d => d.ReplyAttachedDocumentCollection)
                    .Select(ad =>
                        new ProjectCommunicationMessageFile(ad)
                        {
                            Type = ProjectCommunicationMessageFileType.ReplyAttachedDoc,
                        }))
                    .ToList();

                if (projectCommunicationMessageDocument.Project != null)
                {
                    xmlFiles = xmlFiles.Concat(EnumerableExtensions.Concat(
                    projectCommunicationMessageDocument.GetFiles(d => d.Project.AttachedDocuments.AttachedDocumentCollection)
                    .Select(ad =>
                        new ProjectCommunicationMessageFile(ad)
                        {
                            Type = ProjectCommunicationMessageFileType.ProjectDoc,
                        }),
                    projectCommunicationMessageDocument.GetFiles(
                        d => d.Project.AttachedDocuments.AttachedDocumentCollection,
                        d => d.SignatureContentCollection.Select(adc => new AttachedDocument() { AttachedDocumentContent = adc }))
                    .Select(ad =>
                        new ProjectCommunicationMessageFile(ad)
                        {
                            Type = ProjectCommunicationMessageFileType.ProjectSignature,
                        })))
                        .ToList();
                }

                return xmlFiles;
            }
        }

        public override void SetXml(string xml)
        {
            base.SetXml(xml);

            this.Content = this.GetDocument().Content;
        }

        public void SetAnswerXml(string xml)
        {
            base.SetXml(xml);

            var reply = this.GetDocument().Reply;

            if (!string.IsNullOrWhiteSpace(reply))
            {
                this.Content = reply;
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProjectCommunicationMessageMap : ComplexTypeConfiguration<ProjectCommunicationMessage>
    {
        public ProjectCommunicationMessageMap()
        {
            this.Property(t => t.Hash)
                .IsFixedLength()
                .HasMaxLength(10);
        }
    }
}
