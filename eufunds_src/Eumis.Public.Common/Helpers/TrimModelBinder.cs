using System.Web.Mvc;

namespace Eumis.Public.Common.Helpers
{
    public class TrimModelBinder : DefaultModelBinder
    {
        protected override void SetProperty(
            ControllerContext controllerContext,
            ModelBindingContext bindingContext,
            System.ComponentModel.PropertyDescriptor propertyDescriptor,
            object value)
        {
            if (propertyDescriptor.PropertyType == typeof(string))
            {
                var stringValue = (string)value;
                if (!string.IsNullOrEmpty(stringValue))
                {
                    stringValue = stringValue.Trim(new char[] { ' ', '<', '>' });
                }

                value = stringValue;
            }

            base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);
        }
    }
}