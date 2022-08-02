using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.ProgrammeDeclaration.Parsers;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Domain.OperationalMap.ProgrammeDeclarations;
using System;
using System.Collections.Generic;
using System.IO;

namespace Eumis.ApplicationServices.Services.ProgrammeDeclaration
{
    internal class ProgrammeDeclarationService : IProgrammeDeclarationService
    {
        private IUnitOfWork unitOfWork;
        private IProgrammeAppFormDeclarationsRepository programmeAppFormDeclarationsRepository;
        private IBlobServerCommunicator blobServerCommunicator;
        private IProgrammeDeclarationItemParser programmeDeclarationItemParser;

        public ProgrammeDeclarationService(
            IUnitOfWork unitOfWork,
            IProgrammeAppFormDeclarationsRepository programmeAppFormDeclarationsRepository,
            IBlobServerCommunicator blobServerCommunicator,
            IProgrammeDeclarationItemParser programmeDeclarationItemParser)
        {
            this.unitOfWork = unitOfWork;
            this.programmeAppFormDeclarationsRepository = programmeAppFormDeclarationsRepository;
            this.blobServerCommunicator = blobServerCommunicator;
            this.programmeDeclarationItemParser = programmeDeclarationItemParser;
        }

        public IList<string> LoadProgrammeDeclarationItems(int programmeDeclarationId, Guid blobKey)
        {
            var errors = new List<string>();
            var rows = this.ParseExcel(blobKey, out errors);

            if (errors.Count > 0)
            {
                return errors;
            }

            var programmeDeclarationItems = new List<ProgrammeDeclarationItem>();
            foreach (var row in rows)
            {
                var canCreateErrors = this.programmeAppFormDeclarationsRepository.CanAddProgrammeDeclarationItem(programmeDeclarationId, row.OrderNum.Value);

                if (canCreateErrors.Count > 0)
                {
                    errors.AddRange(canCreateErrors);
                }
                else
                {
                    programmeDeclarationItems.Add(new ProgrammeDeclarationItem(programmeDeclarationId, row.OrderNum.Value, row.Content));
                }
            }

            if (errors.Count == 0)
            {
                this.unitOfWork.BulkInsert<ProgrammeDeclarationItem>(programmeDeclarationItems);
                this.unitOfWork.Save();
            }

            return errors;
        }

        private IList<(int? OrderNum, string Content)> ParseExcel(Guid blobKey, out List<string> errors)
        {
            errors = new List<string>();

            IList<(int? OrderNum, string Content)> rows = new List<(int? OrderNum, string Content)>();

            try
            {
                using (var excelStream = this.blobServerCommunicator.GetBlobStream(blobKey, true))
                {
                    rows = this.programmeDeclarationItemParser.ParseExcel(excelStream, out errors);
                }
            }
            catch (FileFormatException)
            {
                errors.Add(ApplicationServicesTexts.Common_InvalidFileFormat);
            }

            return rows;
        }
    }
}
