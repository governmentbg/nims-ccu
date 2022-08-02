using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.ApplicationServices.Services.RequestPackage
{
    public interface IRequestPackageService
    {
        IList<string> CanEndRequestPackage(int requestPackageId);

        void EndRequestPackage(int requestPackageId, byte[] version, string endingMessage);

        IList<string> CanRecoverUser(int userId);

        void RecoverUser(int userId, byte[] version);
    }
}
