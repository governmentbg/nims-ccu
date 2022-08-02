using Eumis.Common.Config;
using Eumis.IntegrationRegiX.Host.Auth;
using Eumis.IntegrationRegiX.Host.Helpers;
using Eumis.IntegrationRegiX.Host.RegixService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Eumis.IntegrationRegiX.Host.Communicators
{
    public class BaseCommunicator : IDisposable
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private bool disposed;
        private RegiXEntryPointClient client;
        private ServiceRequestData requestContext;

        public BaseCommunicator(IRegixCallContext regixCallContext)
        {
            this.requestContext = this.CreateRequestContext(regixCallContext);

            Logger.Info("BaseCommunicator ctor");
            this.disposed = false;
            this.client = new RegiXEntryPointClient("WSHttpBinding_IRegiXEntryPoint");
        }

        public XmlElement ExecuteRequest<T>(string operation, T request, string procedureCode)
        {
            if (this.disposed)
            {
                Logger.Log(NLog.LogLevel.Error, "Base has been disposed");
                return null;
            }

            if (!string.IsNullOrEmpty(procedureCode))
            {
                this.requestContext.CallContext.ServiceURI = procedureCode;
            }

            this.requestContext.Operation = operation;
            this.requestContext.Argument = XmlHelper.SerializeToXmlElement<T>(request);

            var result = this.client.ExecuteSynchronous(this.requestContext);
            if (result.HasError)
            {
                Logger.Log(NLog.LogLevel.Error, result.Error);
                throw new Exception("Something went wrong!");
            }

            Logger.Info("Successfully executed request");
            return result.Data.Response.Any;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.disposed)
            {
                this.disposed = true;

                Logger.Log(NLog.LogLevel.Info, "Base communicator disposed");
            }
        }

        public ServiceRequestData CreateRequestContext(IRegixCallContext regixCallContext)
        {
            var request = new ServiceRequestData();
            request.CallContext = new CallContext();
            request.CallContext.AdministrationName = ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationRegiX.Host:AdministrationName");
            request.CallContext.AdministrationOId = ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationRegiX.Host:AdministrationOId");

            request.CallContext.EmployeeIdentifier = regixCallContext.Email ?? ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationRegiX.Host:EmployeeIdentifier");
            request.CallContext.EmployeeNames = regixCallContext.Name ?? ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationRegiX.Host:EmployeeNames");
            request.CallContext.EmployeePosition = regixCallContext.Position ?? ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationRegiX.Host:EmployeePosition");
            request.CallContext.LawReason = ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationRegiX.Host:LawReason");
            request.CallContext.Remark = ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationRegiX.Host:Remark");
            request.CallContext.ServiceType = ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationRegiX.Host:ServiceType");
            request.CallContext.ServiceURI = ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationRegiX.Host:ServiceURI");

            request.ReturnAccessMatrix = false;
            request.SignResult = false;

            return request;
        }
    }
}
