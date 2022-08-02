using DocumentFormat.OpenXml.Packaging;
using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.ProgrammeApplicationDocuments.Parsers;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Domain.OperationalMap.Programmes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Eumis.ApplicationServices.Services.ProgrammeApplicationDocuments
{
    public class ProgrammeApplicationDocumentService : IProgrammeApplicationDocumentService
    {
        private IUnitOfWork unitOfWork;
        private IBlobServerCommunicator blobServerCommunicator;
        private IProgrammesRepository programmesRepository;
        private IProgrammeApplicationDocumentParser programmeApplicationDocumentParser;

        public ProgrammeApplicationDocumentService(
            IUnitOfWork unitOfWork,
            IBlobServerCommunicator blobServerCommunicator,
            IProgrammesRepository programmesRepository,
            IProgrammeApplicationDocumentParser programmeApplicationDocumentParser)
        {
            this.unitOfWork = unitOfWork;
            this.blobServerCommunicator = blobServerCommunicator;
            this.programmesRepository = programmesRepository;
            this.programmeApplicationDocumentParser = programmeApplicationDocumentParser;
        }

        public IList<string> CanAddProgrammeApplicationDocuments(int programmeId, string name)
        {
            IList<string> errors = new List<string>();

            var programme = this.programmesRepository.Find(programmeId);

            var documentExists = programme.ProgrammeApplicationDocuments.Any(d => d.Name == name);

            if (documentExists)
            {
                errors.Add($"Не можете да създадете документа \"{name}\" защото документ от този тип вече съществува.");
            }

            return errors;
        }

        public IList<string> CanDeleteProgrammeApplicationDocument(int programmeApplicationDocumentId)
        {
            IList<string> errors = new List<string>();

            var isDocumentAttached = this.programmesRepository.IsProgrammeApplicationDocumentAttachedToProcedure(programmeApplicationDocumentId);

            if (isDocumentAttached)
            {
                errors.Add("Документът не може да бъде изтрит, защото е избран в Документи за подаване в процедура.");
            }

            return errors;
        }

        public IList<string> CanLoadProgrammeApplicationDocuments(int programmeId, Guid blobKey)
        {
            IList<string> errors = new List<string>();

            this.ParseExcel(programmeId, blobKey, out errors);

            return errors;
        }

        public void LoadProgrammeApplicationDocuments(int programmeId, Guid blobKey)
        {
            IList<string> errors = new List<string>();

            var documents = this.ParseExcel(programmeId, blobKey, out errors);

            if (errors.Count == 0)
            {
                this.unitOfWork.BulkInsert<ProgrammeApplicationDocument>(documents);

                this.unitOfWork.Save();
            }
        }

        private IList<ProgrammeApplicationDocument> ParseExcel(int programmeId, Guid blobKey, out IList<string> errors)
        {
            IList<ProgrammeApplicationDocument> documents = new List<ProgrammeApplicationDocument>();
            errors = new List<string>();

            var programme = this.programmesRepository.Find(programmeId);
            var programmeApplicationDocuments = programme.ProgrammeApplicationDocuments
                .Select(d => d.Name)
                .ToList();

            try
            {
                using (var excelStream = this.blobServerCommunicator.GetBlobStream(blobKey, true))
                {
                    documents = this.programmeApplicationDocumentParser.ParseExcel(programmeId, excelStream, programmeApplicationDocuments, out errors);
                }
            }
            catch (OpenXmlPackageException e)
            {
                if (e.Message.Contains("Invalid Hyperlink: Malformed URI is embedded as a hyperlink in the document"))
                {
                    errors.Add("Файлът съдържа невалиден/и имейл адрес/и!");
                }

                throw;
            }
            catch (FileFormatException)
            {
                errors.Add("Невалиден формат на файла.");
            }

            return documents;
        }
    }
}
