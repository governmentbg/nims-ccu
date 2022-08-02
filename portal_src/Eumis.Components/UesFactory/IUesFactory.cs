using System.Security.Cryptography.X509Certificates;

namespace Eumis.Components
{
    /// <summary>
    /// Factory интерфейс за Ues обекти.
    /// </summary>
    public interface IUesFactory
    {
        /// <summary>
        /// Връща конкретна имплементация на универсален електронен подпис в зависимост от издателя.
        /// </summary>
        UesBase GetUes(X509Certificate2 certificate);
    }
}