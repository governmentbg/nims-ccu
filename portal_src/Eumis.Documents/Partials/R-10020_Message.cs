//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Eumis.Common.Linq;
using Eumis.Common.Validation;
using Eumis.Documents;
using Eumis.Documents.Contracts;
using Eumis.Documents.Interfaces;
using Eumis.Documents.Validation;
using R_10018;

namespace R_10020
{
    public partial class Message : IDocumentNomenclatures, IEumisDocument, IEumisDocumentWithFiles, IRemoteValidatable, ILocalValidatable
    {
        string IEumisDocument.Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        DateTime IEumisDocument.CreateDate
        {
            get
            {
                return this.createDate;
            }
            set
            {
                this.createDate = value;
            }
        }

        DateTime IEumisDocument.ModificationDate
        {
            get
            {
                return this.modificationDate;
            }
            set
            {
                this.modificationDate = value;
            }
        }

        public IEnumerable<AttachedDocument> Files
        {
            get
            {
                var files = EnumerableExtensions.Concat(
                    this.GetFiles(d => d.ContentAttachedDocumentCollection),
                    this.GetFiles(d => d.ReplyAttachedDocumentCollection));

                if (this.Project != null)
                {
                    files = EnumerableExtensions.Concat(
                        files,
                        this.GetFiles(d => d.Project.AttachedDocuments.AttachedDocumentCollection),
                        this.GetFiles(
                            d => d.Project.AttachedDocuments.AttachedDocumentCollection,
                            d => d.SignatureContentCollection.Select(adc => new AttachedDocument() { AttachedDocumentContent = adc })));
                }

                return files;
            }
        }

        public static Message InitQuestion(Message message)
        {
            message.type = R_09990.MessageTypeNomenclature.Question;

            if (message.ContentAttachedDocumentCollection == null)
                message.ContentAttachedDocumentCollection = new AttachedDocumentCollection();

            #region Metadata

            message.createDate = message.createDate == default(DateTime) ? DateTime.Now : message.createDate;

            if (String.IsNullOrWhiteSpace(message.id))
                message.id = Guid.NewGuid().ToString();

            #endregion

            return message;
        }

        public static Message InitAnswer(Message message)
        {
            message.type = R_09990.MessageTypeNomenclature.Answer;

            if (message.ReplyAttachedDocumentCollection == null)
            {
                message.ReplyAttachedDocumentCollection = new AttachedDocumentCollection();
            }

            #region Metadata

            message.createDate = message.createDate == default(DateTime) ? DateTime.Now : message.createDate;

            message.id = Guid.NewGuid().ToString();

            #endregion

            return message;
        }

        public static Message InitQuestion(Message message, ContractProjectMassCommunicationInitializer communicationInitializer)
        {
            message = Message.InitQuestion(message);

            message.id = Constants.ProjectMassCommunicationTemplateXmlKey;

            message.Subject = new R_09991.EnumNomenclature()
            {
                Value = communicationInitializer.subject.gid.ToString(),
                Description = communicationInitializer.subject.name,
                DescriptionEN = communicationInitializer.subject.nameAlt,
            };

            message.Content = communicationInitializer.message;

            communicationInitializer.files.ForEach(x =>
            {
                message.ContentAttachedDocumentCollection.Add(new AttachedDocument()
                {
                    Description = x.description,
                    AttachedDocumentContent = new R_09992.AttachedDocumentContent()
                    {
                        BlobContentId = x.fileKey.ToString(),
                        FileName = x.fileName,
                    }
                });
            });

            return message;
        }

        public static Message LoadQuestion(Message message, Guid registeredGid, string projectRegNumber)
        {
            if (message == null)
            {
                message = new Message();
            }

            message.IsManagingAuthority = true;
            message.RegisteredGid = registeredGid;
            message.ProjectRegNumber = projectRegNumber;
            message.type = R_09990.MessageTypeNomenclature.Question;

            if (message.ContentAttachedDocumentCollection == null)
            {
                message.ContentAttachedDocumentCollection = new AttachedDocumentCollection();
            }

            return message;
        }

        public static Message LoadReply(ContractProcedure procedure, Message message)
        {
            if (message == null)
                message = new Message();

            message.type = R_09990.MessageTypeNomenclature.Answer;

            message.Project = R_10019.Project.Load(procedure, message.Project);

            if (message.ReplyAttachedDocumentCollection == null)
                message.ReplyAttachedDocumentCollection = new AttachedDocumentCollection();

            return message;
        }

        public static Message LoadReply(Message message, Guid registeredGid, string projectRegNumber)
        {
            if (message == null)
            {
                message = new Message();
            }

            message.IsManagingAuthority = true;
            message.RegisteredGid = registeredGid;
            message.ProjectRegNumber = projectRegNumber;
            message.type = R_09990.MessageTypeNomenclature.Answer;

            if (message.ReplyAttachedDocumentCollection == null)
            {
                message.ReplyAttachedDocumentCollection = new AttachedDocumentCollection();
            }

            return message;
        }

        [XmlIgnore]
        public Dictionary<Eumis.Documents.Mappers.NomenclatureType, List<Eumis.Documents.Mappers.Nomenclature>> Nomenclatures
        {
            get
            {
                if (this.Project != null)
                    return this.Project.Nomenclatures;
                else
                    return new Dictionary<Eumis.Documents.Mappers.NomenclatureType, List<Eumis.Documents.Mappers.Nomenclature>>();
            }

            set
            {
                if (this.Project == null)
                    this.Project = new R_10019.Project();

                this.Nomenclatures = value;
            }
        }

        [XmlIgnore]
        public List<ModelValidationResultExtended> LocalValidationErrors { get; set; }

        [XmlIgnore]
        public List<string> RemoteValidationErrors { get; set; }

        [XmlIgnore]
        public List<string> RemoteValidationWarnings { get; set; }

        [XmlIgnore]
        public ContractMessageHeader MessageHeader { get; set; }

        [XmlIgnore]
        public DateTime? EndingDate { get; set; }
        
        [XmlIgnore]
        public string RegistrationNumber { get; set; }

        [XmlIgnore]
        public DateTime? MessageDate { get; set; }

        [XmlIgnore]
        public DateTime LastSendingDate { get; set; }

        [XmlIgnore]
        public string ProjectCommunicationType { get; set; }

        [XmlIgnore]
        public Guid ProjectCommunicationGid { get; set; }

        [XmlIgnore]
        public Guid RegisteredGid { get; set; }

        [XmlIgnore]
        public string ProjectRegNumber { get; set; }

        [XmlIgnore]
        public string CompanyName { get; set; }

        [XmlIgnore]
        public bool IsManagingAuthority { get; set; }
    }
}