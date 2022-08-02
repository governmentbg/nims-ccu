using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.ContractReportMicro;
using Eumis.Common.Db;
using Eumis.Domain.Contracts.ContractReportMicros;

namespace Eumis.Cli
{
    public class ContractReportMicroType2Rebuild : IMigration
    {
        private IUnitOfWork unitOfWork;
        private IContractReportMicroType2Parser contractReportMicroType2Parser;
        private IBlobServerCommunicator blobServerCommunicator;

        public ContractReportMicroType2Rebuild(
            IUnitOfWork unitOfWork,
            IContractReportMicroType2Parser contractReportMicroType2Parser,
            IBlobServerCommunicator blobServerCommunicator)
        {
            this.unitOfWork = unitOfWork;
            this.contractReportMicroType2Parser = contractReportMicroType2Parser;
            this.blobServerCommunicator = blobServerCommunicator;
        }

        public string Name => "ContractReportMicroType2Rebuild";

        public void Migrate()
        {
            var createStagingTableSql = this.ReadEmbeddedResource(@"Eumis.Cli.Migrations.ContractReportMicroType2Rebuild.ContractReportMicrosType2Items_Staging.sql");
            ((IUnitOfWorkHidden)this.unitOfWork).DbContext.Database.ExecuteSqlCommand(createStagingTableSql);

            Console.WriteLine("Created table ContractReportMicrosType2Items_Staging without transaction to prevent holding SCH-M lock.");
            Console.WriteLine("Drop the table manually if an error occurs!");

            var type2Micros = ((IUnitOfWorkHidden)this.unitOfWork).DbContext.Set<ContractReportMicro>()
                .Where(m => m.Type == ContractReportMicroType.Type2 && m.ExcelBlobKey.HasValue)
                .OrderBy(m => m.ContractReportMicroId)
                .ToList();

            foreach (var micro in type2Micros)
            {
                Stopwatch sw = Stopwatch.StartNew();

                using (var excelStream = this.blobServerCommunicator.GetBlobStream(micro.ExcelBlobKey.Value, false))
                {
                    var items = this.contractReportMicroType2Parser.ParseExcel(micro.ContractReportMicroId, excelStream, out IList<string> errors, out IList<string> warnings);
                    if (errors.Count != 0)
                    {
                        Console.WriteLine($"Errors in ContractReportMicroId: {micro.ContractReportMicroId}");

                        foreach (var error in errors)
                        {
                            Console.WriteLine(error);
                        }
                    }

                    using (var transaction = this.unitOfWork.BeginTransaction())
                    {
                        ((IUnitOfWorkHidden)this.unitOfWork).BulkInsert<ContractReportMicrosType2Item>(items, "ContractReportMicrosType2Items_Staging");
                        transaction.Commit();
                    }

                    Console.WriteLine($"Processed micro {micro.ContractReportMicroId} with {items.Count} items in {sw.Elapsed.TotalSeconds} seconds.");
                }
            }
        }

        private string ReadEmbeddedResource(string resourceName)
        {
            using (var streamReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
