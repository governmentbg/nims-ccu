using System.Collections.Generic;

namespace Eumis.Public.Data.UmisVOs
{
    public class UserStatisticsVO
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public int DraftProjectsCount { get; set; }

        public List<string> DraftOperationalProgrammes { get; set; }

        public int RegisteredProjectsCount { get; set; }

        public List<string> RegisteredOperationalProgrammes { get; set; }

        public string GetFullName()
        {
            return this.FirstName + " " + this.LastName;
        }

        public string GetDraftOperationalProgrammes()
        {
            if (this.DraftOperationalProgrammes != null && this.DraftOperationalProgrammes.Count > 0)
            {
                return string.Join(", ", this.DraftOperationalProgrammes);
            }

            return string.Empty;
        }

        public string GetRegisteredOperationalProgrammes()
        {
            if (this.RegisteredOperationalProgrammes != null && this.RegisteredOperationalProgrammes.Count > 0)
            {
                return string.Join(", ", this.RegisteredOperationalProgrammes);
            }

            return string.Empty;
        }
    }
}
