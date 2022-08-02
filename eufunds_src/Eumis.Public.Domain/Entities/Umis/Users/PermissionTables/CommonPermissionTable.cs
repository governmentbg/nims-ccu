using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Public.Domain.Entities.Umis.Users.PermissionAggregations;

namespace Eumis.Public.Domain.Entities.Umis.Users.PermissionTables
{
    public class CommonPermissionTable
    {
        public IList<PermissionType> PermissionTypes { get; set; }
        public IDictionary<string, IDictionary<string, bool>> Permissions { get; set; }

        //empty template
        public CommonPermissionTable()
        {
            this.CreateCommonPermissionsDO(new List<CommonPermissionAggregationItem>());
        }

        //permission template
        public CommonPermissionTable(IList<CommonPermissionAggregationItem> commonPermissions)
        {
            this.CreateCommonPermissionsDO(commonPermissions);
        }

        //user permission trimmed by template
        public CommonPermissionTable(IList<CommonPermissionAggregationItem> commonPermissions, IList<CommonPermissionAggregationItem> templatePermissions)
        {
            this.CreateCommonPermissionsDO(commonPermissions, templatePermissions);
        }

        //user permissions only
        public CommonPermissionTable(IList<CommonPermissionAggregationItem> commonPermissions, bool permissionsOnly)
        {
            if (permissionsOnly)
            {
                this.CreatePermissionsOnlyCommonPermissionsDO(commonPermissions);
            }
            else
            {
                this.CreateCommonPermissionsDO(commonPermissions);
            }
        }

        public IList<CommonPermissionAggregationItem> GetCommonPermissionAggregationItems()
        {
            var aggregationItems = new List<CommonPermissionAggregationItem>();

            foreach (var permissionTypeKvp in this.Permissions)
            {
                foreach (var permissionKvp in permissionTypeKvp.Value)
                {
                    if (permissionKvp.Value)
                    {
                        var aggregateItem = new CommonPermissionAggregationItem(
                            User.CommonPermissionTypes[permissionTypeKvp.Key],
                            Enum.Parse(User.CommonPermissionTypes[permissionTypeKvp.Key], permissionKvp.Key, true),
                            true);

                        aggregationItems.Add(aggregateItem);
                    }
                }
            }

            return aggregationItems;
        }

        private void CreateCommonPermissionsDO(IList<CommonPermissionAggregationItem> commonPermissions)
        {
            this.PermissionTypes = new List<PermissionType>();
            this.Permissions = new Dictionary<string, IDictionary<string, bool>>();

            foreach (var permissionType in User.CommonPermissionTypes.Values)
            {
                this.PermissionTypes.Add(new PermissionType(permissionType));
            }

            foreach (var permissionType in User.CommonPermissionTypes.Values)
            {
                var permissionsDictionary = new Dictionary<string, bool>();

                foreach (Enum permission in Enum.GetValues(permissionType))
                {
                    bool isSet = commonPermissions.Any(e =>
                        e.PermissionType.Name == permissionType.Name && 
                        e.Permission.ToString() == permission.ToString() && 
                        e.IsSet);

                    permissionsDictionary.Add(permission.ToString(), isSet);
                }

                this.Permissions.Add(permissionType.Name, permissionsDictionary);
            }
        }

        private void CreatePermissionsOnlyCommonPermissionsDO(IList<CommonPermissionAggregationItem> commonPermissions)
        {
            this.PermissionTypes = new List<PermissionType>();
            this.Permissions = new Dictionary<string, IDictionary<string, bool>>();
            bool includeType = false;

            foreach (var permissionType in User.CommonPermissionTypes.Values)
            {
                var permissionsDictionary = new Dictionary<string, bool>();

                foreach (Enum permission in Enum.GetValues(permissionType))
                {
                    bool isSet = commonPermissions.Any(e =>
                        e.PermissionType.Name == permissionType.Name &&
                        e.Permission.ToString() == permission.ToString() &&
                        e.IsSet);

                    if (isSet)
                    {
                        permissionsDictionary.Add(permission.ToString(), isSet);
                        includeType = true;
                    }
                }

                if (includeType)
                {
                    this.PermissionTypes.Add(new PermissionType(permissionType));
                    this.Permissions.Add(permissionType.Name, permissionsDictionary);
                }

                includeType = false;
            }
        }

        private void CreateCommonPermissionsDO(IList<CommonPermissionAggregationItem> commonPermissions, IList<CommonPermissionAggregationItem>  templatePermissions)
        {
            this.PermissionTypes = new List<PermissionType>();
            this.Permissions = new Dictionary<string, IDictionary<string, bool>>();
            bool includeType = false;

            foreach (var permissionType in User.CommonPermissionTypes.Values)
            {
                var permissionsDictionary = new Dictionary<string, bool>();

                foreach (Enum permission in Enum.GetValues(permissionType))
                {
                    bool setInTemplate = templatePermissions.Any(e =>
                        e.PermissionType.Name == permissionType.Name &&
                        e.Permission.ToString() == permission.ToString() &&
                        e.IsSet);

                    if (setInTemplate)
                    {
                        bool isSet = commonPermissions.Any(e =>
                            e.PermissionType.Name == permissionType.Name &&
                            e.Permission.ToString() == permission.ToString() &&
                            e.IsSet);

                        permissionsDictionary.Add(permission.ToString(), isSet);
                        includeType = true;
                    }
                }

                if (includeType)
                {
                    this.PermissionTypes.Add(new PermissionType(permissionType));
                    this.Permissions.Add(permissionType.Name, permissionsDictionary);
                }

                includeType = false;
            }
        }
    }
}
