using System;
using System.Web.Mvc;

namespace Eumis.Public.Common.Helpers
{
    public class DecimalModelBinder : DefaultModelBinder
    {
        #region Implementation of IModelBinder

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult.AttemptedValue.Equals("N.aN") ||
                valueProviderResult.AttemptedValue.Equals("NaN") ||
                valueProviderResult.AttemptedValue.Equals("Infini.ty") ||
                valueProviderResult.AttemptedValue.Equals("Infinity") ||
                string.IsNullOrEmpty(valueProviderResult.AttemptedValue))
            {
                return 0m;
            }

            string decimalString = valueProviderResult.AttemptedValue.Replace(" ", string.Empty).Replace(",", ".");
            decimal result;

            if (decimal.TryParse(decimalString, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out result))
            {
                return result;
            }
            else
            {
                return 0m;
            }
        }

        #endregion
    }
}
