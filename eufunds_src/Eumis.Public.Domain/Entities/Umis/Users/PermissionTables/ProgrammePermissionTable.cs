using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Public.Domain.Entities.Umis.Users.PermissionAggregations;

namespace Eumis.Public.Domain.Entities.Umis.Users.PermissionTables
{
    public class ProgrammePermissionTable
    {
        public IDictionary<int, string> Programmes { get; set; }

        public IList<PermissionType> PermissionTypes { get; set; }

        public IDictionary<int, IDictionary<string, IDictionary<string, bool>>> Permissions { get; set; }

        public ProgrammePermissionTable()
        {
            this.Programmes = new Dictionary<int, string>();
            this.PermissionTypes = new List<PermissionType>();
            this.Permissions = new Dictionary<int, IDictionary<string, IDictionary<string, bool>>>();
        }

        //empty template
        public ProgrammePermissionTable(Dictionary<int, string> programmes)
            :this()
        {
            this.CreateProgrammePermissionsDO(programmes, new List<ProgrammePermissionAggregationItem>());
        }

        //permission template
        public ProgrammePermissionTable(Dictionary<int, string> programmes, IList<ProgrammePermissionAggregationItem> programmePermissions)
            : this()
        {
            this.CreateProgrammePermissionsDO(programmes, programmePermissions);
        }

        //user permission trimmed by template
        public ProgrammePermissionTable(
            Dictionary<int, string> programmes,
            IList<ProgrammePermissionAggregationItem> programmePermissions,
            IList<ProgrammePermissionAggregationItem> templatePermissions)
            : this()
        {
            this.CreateProgrammePermissionsDO(programmes, programmePermissions, templatePermissions);
        }

        //user permissions only
        public ProgrammePermissionTable(Dictionary<int, string> programmes, IList<ProgrammePermissionAggregationItem> programmePermissions, bool permissionsOnly)
            : this()
        {
            if (permissionsOnly)
            {
                this.CreatePermissionsOnlyProgrammePermissionsDO(programmes, programmePermissions);
            }
            else
            {
                this.CreateProgrammePermissionsDO(programmes, programmePermissions);
            }
        }

        public IList<ProgrammePermissionAggregationItem> GetProgrammePermissionAggregationItems()
        {
            var aggregationItems = new List<ProgrammePermissionAggregationItem>();

            foreach(var programmeKvp in this.Permissions)
            {
                foreach (var permissionTypeKvp in programmeKvp.Value)
                {
                    foreach (var permissionKvp in permissionTypeKvp.Value)
                    {
                        if (permissionKvp.Value)
                        {
                            var aggregateItem = new ProgrammePermissionAggregationItem(
                                programmeKvp.Key,
                                User.ProgrammePermissionTypes[permissionTypeKvp.Key],
                                Enum.Parse(User.ProgrammePermissionTypes[permissionTypeKvp.Key], permissionKvp.Key, true),
                                true);

                            aggregationItems.Add(aggregateItem);
                        }
                    }
                }
            }

            return aggregationItems;
        }

        private void CreateProgrammePermissionsDO(Dictionary<int, string> programmes, IList<ProgrammePermissionAggregationItem> programmePermissions)
        {
            this.Programmes = programmes;

            foreach (var permissionType in User.ProgrammePermissionTypes.Values)
            {
                this.PermissionTypes.Add(new PermissionType(permissionType));
            }

            foreach (var programme in programmes)
            {
                var permissionTypesDictionary = new Dictionary<string, IDictionary<string, bool>>();

                foreach (var permissionType in User.ProgrammePermissionTypes.Values)
                {
                    var permissionsDictionary = new Dictionary<string, bool>();

                    foreach (Enum permission in Enum.GetValues(permissionType))
                    {
                        bool isSet = programmePermissions.Any(e =>
                            e.ProgrammeId == programme.Key &&
                            e.PermissionType.Name == permissionType.Name && 
                            e.Permission.ToString() == permission.ToString() &&
                            e.IsSet);

                        permissionsDictionary.Add(permission.ToString(), isSet);
                    }

                    permissionTypesDictionary.Add(permissionType.Name, permissionsDictionary);
                }

                this.Permissions.Add(programme.Key, permissionTypesDictionary);
            }
        }

        private void CreatePermissionsOnlyProgrammePermissionsDO(Dictionary<int, string> programmes, IList<ProgrammePermissionAggregationItem> programmePermissions)
        {
            foreach (var programme in programmes)
            {
                bool includeType = false;
                bool includeProgramme = false;
                var permissionTypesDictionary = new Dictionary<string, IDictionary<string, bool>>();

                foreach (var permissionType in User.ProgrammePermissionTypes.Values)
                {
                    var permissionsDictionary = new Dictionary<string, bool>();

                    foreach (Enum permission in Enum.GetValues(permissionType))
                    {
                        bool isSet = programmePermissions.Any(e =>
                            e.ProgrammeId == programme.Key &&
                            e.PermissionType.Name == permissionType.Name &&
                            e.Permission.ToString() == permission.ToString() &&
                            e.IsSet);

                        if(isSet)
                        {
                            permissionsDictionary.Add(permission.ToString(), isSet);
                            includeProgramme = true;
                            includeType = true;
                        }
                    }

                    if (includeType && !this.PermissionTypes.Where(t => t.DisplayName == permissionType).Any())
                    {
                        this.PermissionTypes.Add(new PermissionType(permissionType));
                    }

                    includeType = false;

                    if (permissionsDictionary.Count != 0)
                    {
                        permissionTypesDictionary.Add(permissionType.Name, permissionsDictionary);
                    }
                }

                if (includeProgramme)
                {
                    this.Programmes.Add(programme);
                    this.Permissions.Add(programme.Key, permissionTypesDictionary);
                }
            }
        }

        private void CreateProgrammePermissionsDO(Dictionary<int, string> programmes, IList<ProgrammePermissionAggregationItem> programmePermissions, IList<ProgrammePermissionAggregationItem> templatePermissions)
        {
            bool includeProgramme = false;
            bool includeType = false;

            foreach (var programme in programmes)
            {
                includeProgramme = false;
                var permissionTypesDictionary = new Dictionary<string, IDictionary<string, bool>>();

                foreach (var permissionType in User.ProgrammePermissionTypes.Values)
                {
                    var permissionsDictionary = new Dictionary<string, bool>();

                    foreach (Enum permission in Enum.GetValues(permissionType))
                    {
                        bool setInTemplate = templatePermissions.Any(e =>
                            e.ProgrammeId == programme.Key &&
                            e.PermissionType.Name == permissionType.Name &&
                            e.Permission.ToString() == permission.ToString() &&
                            e.IsSet);

                        if (setInTemplate)
                        {
                            bool isSet = programmePermissions.Any(e =>
                                e.ProgrammeId == programme.Key &&
                                e.PermissionType.Name == permissionType.Name &&
                                e.Permission.ToString() == permission.ToString() &&
                                e.IsSet);

                            permissionsDictionary.Add(permission.ToString(), isSet);
                            includeProgramme = true;
                            includeType = true;
                        }
                    }

                    if (includeType && !this.PermissionTypes.Where(t => t.DisplayName == permissionType).Any())
                    {
                        this.PermissionTypes.Add(new PermissionType(permissionType));
                    }

                    includeType = false;

                    if (permissionsDictionary.Count != 0)
                    {
                        permissionTypesDictionary.Add(permissionType.Name, permissionsDictionary);
                    }
                }

                if (includeProgramme)
                {
                    this.Programmes.Add(programme);
                    this.Permissions.Add(programme.Key, permissionTypesDictionary);
                }

            }
        }
    }
}
