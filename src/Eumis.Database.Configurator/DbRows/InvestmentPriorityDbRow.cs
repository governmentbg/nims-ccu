using Eumis.Database.Configurator.Helpers;

namespace Eumis.Database.Configurator.DbRows
{
    internal class InvestmentPriorityDbRow : IDbRow
    {
        public const string DbTableName = "InvestmentPriorities";
        public const bool UseIdentityInsert = true;

        public int InvestmentPriorityId { get; set; }

        public int InterventionCategoryId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string CreateRowInsert()
        {
            return string.Format(
                "INSERT INTO [{0}] ([InvestmentPriorityId], [InterventionCategoryId], [Code], [Name], [NameAlt]) VALUES ({1}, {2}, {3}, {4}, {5})",
                DbTableName,
                SqlScriptHelper.ToString(this.InvestmentPriorityId),
                SqlScriptHelper.ToString(this.InterventionCategoryId),
                SqlScriptHelper.ToString(this.Code),
                SqlScriptHelper.ToString(this.Name),
                SqlScriptHelper.ToString(this.NameAlt));
        }
    }
}
