using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Public.Domain.Entities.Umis.Users.PermissionAggregations
{
    /// <summary>
    /// An immutable representation of a group of permission
    /// </summary>
    public class PermissionAggregation
    {
        public static readonly string PermissionSeparator = ";";
        public static readonly string CommonPermissionsSeparator = "|";
        public static readonly string PermissionPartsSeparator = ",";

        public PermissionAggregation()
            : this(new int[0], (string)null)
        {
        }

        public PermissionAggregation(int[] programmeIds, string permissionsString)
        {
            List<Tuple<string, string>> setCommonPermissions = new List<Tuple<string, string>>();
            List<Tuple<int, string, string>> setProgrammePermissions = new List<Tuple<int, string, string>>();

            if (!string.IsNullOrEmpty(permissionsString) && permissionsString.Trim() != PermissionAggregation.CommonPermissionsSeparator)
            {
                var stringItems = permissionsString.Split(new string[] { PermissionAggregation.CommonPermissionsSeparator }, StringSplitOptions.None);
                if (!string.IsNullOrWhiteSpace(stringItems[0]))
                {
                    //CommonPermissions Format { permissionType, permission }
                    var items = stringItems[0].Split(new string[] { PermissionAggregation.PermissionSeparator }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in items)
                    {
                        var subitems = item.Split(new string[] { PermissionAggregation.PermissionPartsSeparator }, StringSplitOptions.RemoveEmptyEntries);
                        setCommonPermissions.Add(Tuple.Create(subitems[0], subitems[1]));
                    }
                }

                if (!string.IsNullOrWhiteSpace(stringItems[1]))
                {
                    //ProgrammePermissions Format { programmeId, permissionType, permission }
                    var items = stringItems[1].Split(new string[] { PermissionAggregation.PermissionSeparator }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in items)
                    {
                        var subitems = item.Split(new string[] { PermissionAggregation.PermissionPartsSeparator }, StringSplitOptions.RemoveEmptyEntries);
                        setProgrammePermissions.Add(Tuple.Create(int.Parse(subitems[0]), subitems[1], subitems[2]));
                    }
                }
            }

            this.InitCommonPermissions(setCommonPermissions);
            this.InitProgrammePermissions(programmeIds, setProgrammePermissions);
        }

        public PermissionAggregation(int[] programmeIds, IList<CommonPermissionAggregationItem> commonPermissions, IList<ProgrammePermissionAggregationItem> programmePermissions)
        {
            //Init CommonPermissions { permissionType, permission }
            List<Tuple<string, string>> setCommonPermissions = commonPermissions
                .Where(p => p.IsSet)
                .Select(p => Tuple.Create(p.PermissionType.Name, p.Permission.ToString()))
                .ToList();

            this.InitCommonPermissions(setCommonPermissions);

            //Init ProgrammePermissions { programmeId, permissionType, permission }
            List<Tuple<int, string, string>> setProgrammePermissions = programmePermissions
                .Where(p => p.IsSet)
                .Select(p => Tuple.Create(p.ProgrammeId, p.PermissionType.Name, p.Permission.ToString()))
                .ToList();

            this.InitProgrammePermissions(programmeIds, setProgrammePermissions);
        }

        public IList<ProgrammePermissionAggregationItem> ProgrammePermissions { get; private set; }

        public IList<CommonPermissionAggregationItem> CommonPermissions { get; private set; }

        public string ToPermissionsString()
        {
            var setCommonPermissions = this.CommonPermissions
                .Where(p => p.IsSet)
                .Select(p => string.Format("{0}{1}{2}", p.PermissionType.Name, PermissionAggregation.PermissionPartsSeparator, p.Permission.ToString()))
                .ToArray();

            string commonPermissionsString = string.Join(PermissionAggregation.PermissionSeparator, setCommonPermissions);

            var setProgrammePermissions = this.ProgrammePermissions
                .Where(p => p.IsSet)
                .Select(p => string.Format("{0}{1}{2}{1}{3}", p.ProgrammeId, PermissionAggregation.PermissionPartsSeparator, p.PermissionType.Name, p.Permission.ToString()))
                .ToArray();

            string programmePermissionsString = string.Join(PermissionAggregation.PermissionSeparator, setProgrammePermissions);

            return commonPermissionsString + PermissionAggregation.CommonPermissionsSeparator + programmePermissionsString;
        }

        #region Private

        private void InitCommonPermissions(List<Tuple<string, string>> set)
        {
            List<CommonPermissionAggregationItem> commonPermissions = new List<CommonPermissionAggregationItem>();

            foreach (Type pt in User.CommonPermissionTypes.Values)
            {
                foreach (object p in Enum.GetValues(pt))
                {
                    bool isSet = set.Contains(Tuple.Create(pt.Name, p.ToString()));
                    commonPermissions.Add(new CommonPermissionAggregationItem(pt, p, isSet));
                }
            }


            this.CommonPermissions = commonPermissions.AsReadOnly();
        }

        private void InitProgrammePermissions(int[] programmeIds, List<Tuple<int, string, string>> set)
        {
            List<ProgrammePermissionAggregationItem> programmePermissions = new List<ProgrammePermissionAggregationItem>();
            foreach (int programmeId in programmeIds)
            {
                foreach (Type pt in User.ProgrammePermissionTypes.Values)
                {
                    foreach (object p in Enum.GetValues(pt))
                    {
                        bool isSet = set.Contains(Tuple.Create(programmeId, pt.Name, p.ToString()));
                        programmePermissions.Add(new ProgrammePermissionAggregationItem(programmeId, pt, p, isSet));
                    }
                }
            }

            this.ProgrammePermissions = programmePermissions.AsReadOnly();
        }

        #endregion //Private
    }
}
