using Eumis.Data.OperationalMap.Programmes.Repositories;

namespace Eumis.Authentication.Authorization
{
    public class ProgrammeCacheManager : IProgrammeCacheManager
    {
        private object syncObj = new object();
        private int[] programmeIds;
        private IProgrammesRepository programmesRepository;

        public ProgrammeCacheManager(IProgrammesRepository programmesRepository)
        {
            this.programmesRepository = programmesRepository;
        }

        public int[] ProgrammeIds
        {
            get
            {
                if (this.programmeIds == null)
                {
                    lock (this.syncObj)
                    {
                        if (this.programmeIds == null)
                        {
                            this.programmeIds = this.programmesRepository.GetProgammeIds();
                        }
                    }
                }

                return this.programmeIds;
            }
        }

        public void ClearCache()
        {
            lock (this.syncObj)
            {
                this.programmeIds = null;
            }
        }
    }
}
