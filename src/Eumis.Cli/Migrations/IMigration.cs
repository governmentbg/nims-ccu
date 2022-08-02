namespace Eumis.Cli
{
    public interface IMigration
    {
        string Name { get; }

        void Migrate();
    }
}
