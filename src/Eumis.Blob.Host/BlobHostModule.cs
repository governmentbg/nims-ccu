using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Eumis.Blob.Host.Controllers;

namespace Eumis.Blob.Host
{
    public class BlobHostModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            moduleBuilder.RegisterHttpRequestMessage(new HttpConfiguration());

            moduleBuilder.RegisterType<BlobsController>();
        }
    }
}
