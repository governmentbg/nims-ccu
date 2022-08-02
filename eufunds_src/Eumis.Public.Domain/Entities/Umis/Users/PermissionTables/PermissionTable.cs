using System.Collections.Generic;
using Eumis.Public.Domain.Entities.Umis.Users.PermissionAggregations;

namespace Eumis.Public.Domain.Entities.Umis.Users.PermissionTables
{
    public class PermissionTable
    {
        public ProgrammePermissionTable ProgrammePermissions { get; set; }

        public CommonPermissionTable CommonPermissions { get; set; }

        public byte[] Version { get; set; }

        public PermissionTable()
        {
        }

        //empty template
        public PermissionTable(Dictionary<int, string> programmes)
        {
            this.CreatePermissionTable(programmes, new PermissionAggregation(), null);
        }

        //permission template
        public PermissionTable(Dictionary<int, string> programmes, PermissionAggregation permissions, byte[] version)
        {
            this.CreatePermissionTable(programmes, permissions, version);
        }

        //user permission trimmed by template
        public PermissionTable(Dictionary<int, string> programmes, PermissionAggregation permissions, PermissionAggregation templatePermissions, byte[] version)
        {
            this.CreatePermissionTable(programmes, permissions, templatePermissions, version);
        }

        //user permissions only
        public PermissionTable(Dictionary<int, string> programmes, PermissionAggregation permissions, bool permissionsOnly, byte[] version)
        {
            this.CreatePermissionTable(programmes, permissions, version, permissionsOnly);
        }

        private void CreatePermissionTable(Dictionary<int, string> programmes, PermissionAggregation permissions, byte[] version)
        {
            this.ProgrammePermissions = new ProgrammePermissionTable(programmes, permissions.ProgrammePermissions);
            this.CommonPermissions = new CommonPermissionTable(permissions.CommonPermissions);

            this.Version = version;
        }

        private void CreatePermissionTable(Dictionary<int, string> programmes, PermissionAggregation permissions, PermissionAggregation templatePermissions, byte[] version)
        {
            this.ProgrammePermissions = new ProgrammePermissionTable(programmes, permissions.ProgrammePermissions, templatePermissions.ProgrammePermissions);
            this.CommonPermissions = new CommonPermissionTable(permissions.CommonPermissions, templatePermissions.CommonPermissions);

            this.Version = version;
        }

        private void CreatePermissionTable(Dictionary<int, string> programmes, PermissionAggregation permissions, byte[] version, bool permissionsOnly)
        {
            this.ProgrammePermissions = new ProgrammePermissionTable(programmes, permissions.ProgrammePermissions, permissionsOnly);
            this.CommonPermissions = new CommonPermissionTable(permissions.CommonPermissions, permissionsOnly);

            this.Version = version;
        }

        public PermissionAggregation GetPermissions(int[] programmeIds)
        {
            IList<ProgrammePermissionAggregationItem> programmePermissions = this.ProgrammePermissions.GetProgrammePermissionAggregationItems();
            IList<CommonPermissionAggregationItem> commonPermissions = this.CommonPermissions.GetCommonPermissionAggregationItems();

            return new PermissionAggregation(programmeIds, commonPermissions, programmePermissions);
        }
    }
}
