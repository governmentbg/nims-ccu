using Eumis.Database.Configurator.Helpers;

namespace Eumis.Database.Configurator.DbRows
{
    internal class InstitutionDbRow : IDbRow
    {
        public const string DbTableName = "Institutions";
        public const bool UseIdentityInsert = true;

        public int InstitutionId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CreateRowInsert()
        {
            return string.Format(
                "INSERT INTO [{0}] ([InstitutionId], [Name], [Description]) VALUES ({1}, {2}, {3})",
                DbTableName,
                SqlScriptHelper.ToString(this.InstitutionId),
                SqlScriptHelper.ToString(this.Name),
                SqlScriptHelper.ToString(this.Description));
        }
    }
}
