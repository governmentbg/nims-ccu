namespace Eumis.Authentication.Authorization
{
    public interface IProgrammeCacheManager
    {
        int[] ProgrammeIds { get; }

        void ClearCache();
    }
}
