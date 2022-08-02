using Eumis.Common.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class TreeProcedure
    {
        public int number { get; set; }
        public Guid gid { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string nameAlt { get; set; }
        public string displayName { get { return SystemLocalization.GetDisplayName(name, nameAlt); } }
        public string statusText { get; set; }
        public string statusTextAlt { get; set; }
        public string displayStatus { get { return SystemLocalization.GetDisplayName(statusText, statusTextAlt); } }
        public bool isIntroducedByLAG { get; set; }
    }
    public class TreeProgrammePriority
    {
        public int number { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string nameAlt { get; set; }
        public string displayName { get { return SystemLocalization.GetDisplayName(name, nameAlt); } }
        public List<TreeProcedure> procedures { get; set; }
    }
    public class ContractProcedureProgrammesTree
    {
        public string code { get; set; }
        public string name { get; set; }
        public string nameAlt { get; set; }
        public string displayName { get { return SystemLocalization.GetDisplayName(name, nameAlt); } }
        public List<TreeProgrammePriority> programmePriorities { get; set; }
    }
}
