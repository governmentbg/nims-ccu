using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Eumis.Public.Common.Helpers
{
    /// <summary>
    /// Only allows authorized IP addresses access.
    /// </summary>
    public class AuthorizeIPAddressAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Compares an IP address to list of valid IP addresses attempting to
        /// find a match.
        /// </summary>
        /// <param name="ipAddress">String representation of a valid IP Address.</param>
        /// <returns>bool.</returns>
        public static bool IsIpAddressValid(string ipAddress)
        {
            // Split the users IP address into it's 4 octets (Assumes IPv4)
            string[] incomingOctets = ipAddress.Trim().Split(new char[] { '.' });

            // Store each valid IP address in a string array
            List<string> validIpAddresses = "::1".Trim().Split(new char[] { ',' }).ToList();

            List<string> cachedAddresses = new InMemoryCache()
                .GetOrSet(InMemoryCache.DefaultKey, () => new List<string>());

            validIpAddresses = validIpAddresses.Concat(cachedAddresses).Distinct().ToList();

            // Iterate through each valid IP address
            foreach (var validIpAddress in validIpAddresses)
            {
                // Return true if valid IP address matches the users
                if (validIpAddress.Trim() == ipAddress)
                {
                    return true;
                }

                // Split the valid IP address into it's 4 octets
                string[] validOctets = validIpAddress.Trim().Split(new char[] { '.' });

                bool matches = true;

                // Iterate through each octet
                for (int index = 0; index < validOctets.Length; index++)
                {
                    // Skip if octet is an asterisk indicating an entire
                    // subnet range is valid
                    if (validOctets[index] != "*")
                    {
                        if (validOctets[index] != incomingOctets[index])
                        {
                            matches = false;
                            break; // Break out of loop
                        }
                    }
                }

                if (matches)
                {
                    return true;
                }
            }

            // Found no matches
            return false;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                         || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);

            if (!skipAuthorization)
            {
                // Get users IP Address
                string ipAddress = System.Web.HttpContext.Current.Request.UserHostAddress;

                if (!IsIpAddressValid(ipAddress.Trim()))
                {
                    // Send back a HTTP Status code of 403 Forbidden
                    filterContext.Result = new HttpStatusCodeResult(403);
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
