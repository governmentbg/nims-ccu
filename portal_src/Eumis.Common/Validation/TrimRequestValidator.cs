using System.Web;
using System.Web.Util;
namespace Eumis.Common.Validation
{
    public class TrimRequestValidator : RequestValidator
    {
        public TrimRequestValidator() { }

        protected override bool IsValidRequestString(
                                    HttpContext context, string value,
                                    RequestValidationSource requestValidationSource, string collectionKey,
                                    out int validationFailureIndex)
        {
            validationFailureIndex = -1;  //Set a default value for the out parameter.        

            if (requestValidationSource == RequestValidationSource.Form)
            {

                //If the form data contains less thand or greater than characters then use logic to identify the tag name 
                if (value.Contains("<") || value.Contains(">"))
                {
                    value = value.Trim(new char[] { '<', '>' });

                    //validationFailureIndex = -1;
                    //return true;
                }

                // //Leave any further checks to ASP.NET.
                // return base.IsValidRequestString(context, value, requestValidationSource,
                //                                 collectionKey, out validationFailureIndex);
            }

            return base.IsValidRequestString(context, value, requestValidationSource,
                                                collectionKey, out validationFailureIndex);
        }
    }
}