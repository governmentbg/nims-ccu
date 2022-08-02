using System;

namespace Eumis.Authentication.Authorization
{
    public interface IAuthorizer
    {
        bool CanDo(Enum action);

        bool CanDo(Enum action, int id);

        bool CanDo(string action, int? id = null);
    }
}
