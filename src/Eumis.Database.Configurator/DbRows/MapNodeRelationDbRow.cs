using Eumis.Database.Configurator.Helpers;

namespace Eumis.Database.Configurator.DbRows
{
    internal class MapNodeRelationDbRow : IDbRow
    {
        public const string DbTableName = "MapNodeRelations";
        public const bool UseIdentityInsert = false;

        public int MapNodeId { get; set; }

        public int? ParentMapNodeId { get; set; }

        public int? ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public string CreateRowInsert()
        {
            return string.Format(
                "INSERT INTO [{0}] ([MapNodeId], [ParentMapNodeId], [ProgrammeId], [ProgrammePriorityId]) VALUES ({1}, {2}, {3}, {4})",
                DbTableName,
                SqlScriptHelper.ToString(this.MapNodeId),
                SqlScriptHelper.ToString(this.ParentMapNodeId),
                SqlScriptHelper.ToString(this.ProgrammeId),
                SqlScriptHelper.ToString(this.ProgrammePriorityId));
        }
    }
}
