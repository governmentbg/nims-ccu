using System;
using System.Collections.Generic;
using System.Linq;
using Monitorstat.IntegrationEumis.Host.Communicators;
using Monitorstat.IntegrationEumis.Host.Models;

namespace Monitorstat.IntegrationEumis.Host
{
    public class IsunService : IIsunService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IEumisRestApiCommunicator eumis;
        private readonly IBlobCommunicator blob;

        public IsunService()
        {
            this.eumis = new EumisRestApiCommunicator();
            this.blob = new BlobCommunicator();
        }

        public void RegisterMonitorstatResult(RegisterMonitorstatResultRequest request, Guid? subjectRequestGuid)
        {
            Logger.Info($"{nameof(this.RegisterMonitorstatResult)}({Newtonsoft.Json.JsonConvert.SerializeObject(new RegisterMonitorstatResultRequestLogMapper(request))})");

            request.Validate();

            var blobInfo = this.blob.UploadBlob(request.File);
            Logger.Info($"Uploaded blob key is: {blobInfo.FileKey}");

            var eumisRequest = new RegisterEumisResultRequest(request, blobInfo, subjectRequestGuid);

            try
            {
                List<string> errors = this.eumis.RegisterMonitorstatDocument(eumisRequest).Errors;

                errors.ForEach(e => Logger.Error(e));

                if (errors.Any())
                {
                    Logger.Error(string.Join(";", errors));
                    throw new Exception($"Errors occurred: {string.Join(";", errors)}");
                }
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
            {
                Logger.Error(e, "Error in communication with Integration app");
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }
    }
}
