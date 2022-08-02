using Eumis.Database.Configurator.Helpers;

namespace Eumis.Database.Configurator.DbRows
{
    internal class MapNodeDbRow : IDbRow
    {
        public const string DbTableName = "MapNodes";
        public const bool UseIdentityInsert = true;

        public static readonly DeterministicGuidGenerator GidGenerator = new DeterministicGuidGenerator();

        public int MapNodeId { get; set; }

        public string Type { get; set; }

        public string Code { get; set; }

        public string ShortName { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public int? PortalOrderNum { get; set; }

        public string PortalName { get; set; }

        public string PortalNameAlt { get; set; }

        public string PortalShortNameAlt { get; set; }

        public int? InvestmentPriorityId { get; set; }

        public bool? IsHidden { get; set; }

        public string IrregularityCode { get; set; }

        public string CreateRowInsert()
        {
            return string.Format(
                "INSERT INTO [{0}] ([MapNodeId], [Gid], [Type], [Status], [Code], [ShortName], [Name], [NameAlt], [CreateDate], [ModifyDate], [Description], [DescriptionAlt], [RegulationNumber], [RegulationDate], [InvestmentPriorityId], [IsHidden], [PortalOrderNum], [PortalName], [PortalNameAlt], [PortalShortNameAlt], [IrregularityCode]) VALUES ({1}, '{2}', {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21})",
                DbTableName,
                SqlScriptHelper.ToString(this.MapNodeId),
                GidGenerator.GetNextDeterministicGuid().ToString(),
                SqlScriptHelper.ToString(this.Type),
                "2",
                SqlScriptHelper.ToString(this.Code),
                SqlScriptHelper.ToString(this.ShortName),
                SqlScriptHelper.ToString(this.Name),
                SqlScriptHelper.ToString(this.NameAlt),
                "GETDATE()",
                "GETDATE()",
                "NULL",
                "NULL",
                "NULL",
                "NULL",
                SqlScriptHelper.ToString(this.InvestmentPriorityId),
                SqlScriptHelper.ToString(this.IsHidden),
                SqlScriptHelper.ToString(this.PortalOrderNum),
                SqlScriptHelper.ToString(this.PortalName),
                SqlScriptHelper.ToString(this.PortalNameAlt),
                SqlScriptHelper.ToString(this.PortalShortNameAlt),
                SqlScriptHelper.ToString(this.IrregularityCode));
        }
    }
}
