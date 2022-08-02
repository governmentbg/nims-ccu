using Eumis.Database.Configurator.Helpers;

namespace Eumis.Database.Configurator.DbRows
{
    internal class MapNodeInstitutionDbRow : IDbRow
    {
        public const string DbTableName = "MapNodeInstitutions";
        public const bool UseIdentityInsert = false;

        public int MapNodeId { get; set; }

        public int InstitutionId { get; set; }

        public int InstitutionTypeId { get; set; }

        public string ContactName { get; set; }

        public string ContactPosition { get; set; }

        public string ContactPhone { get; set; }

        public string ContactFax { get; set; }

        public string ContactEmail { get; set; }

        public string CreateRowInsert()
        {
            return string.Format(
                "INSERT INTO [{0}] ([MapNodeId], [InstitutionId], [InstitutionTypeId], [ContactName], [ContactPosition], [ContactPhone], [ContactFax], [ContactEmail]) VALUES ({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})",
                DbTableName,
                SqlScriptHelper.ToString(this.MapNodeId),
                SqlScriptHelper.ToString(this.InstitutionId),
                SqlScriptHelper.ToString(this.InstitutionTypeId),
                SqlScriptHelper.ToString(this.ContactName),
                SqlScriptHelper.ToString(this.ContactPosition),
                SqlScriptHelper.ToString(this.ContactPhone),
                SqlScriptHelper.ToString(this.ContactFax),
                SqlScriptHelper.ToString(this.ContactEmail));
        }
    }
}
