using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.Emails
{
    public class EmailsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmailMap());
        }
    }
}
