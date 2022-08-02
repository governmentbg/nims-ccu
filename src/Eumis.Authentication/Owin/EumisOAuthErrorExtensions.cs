using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eumis.Common.Json;
using Microsoft.Owin.Security.OAuth;

namespace Eumis.Authentication.Owin
{
    internal static class EumisOAuthErrorExtensions
    {
        public static void SetError<TOptions>(this BaseValidatingContext<TOptions> context, EumisOAuthErrors error)
        {
            context.SetError(EnumUtils.GetCamelCaseEnumValue(error));
        }
    }
}
