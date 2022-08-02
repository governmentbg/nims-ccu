using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eumis.Common.ReCaptcha
{
    public static class HtmlExtension
    {
        public static MvcHtmlString ReCaptcha(this HtmlHelper helper, string siteKey)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<input type=\"hidden\" id=\"g-recaptcha-response\" name=\"g-recaptcha-response\" />");
            sb.Append("<script src=\"https://www.google.com/recaptcha/api.js?render=" + siteKey + "\"></script>" + Environment.NewLine);
            sb.Append("<script>" + Environment.NewLine);
            sb.Append(" $(function(){ " + Environment.NewLine);
            sb.Append("     grecaptcha.ready(function() {" + Environment.NewLine);
            sb.Append("         grecaptcha.execute('" + siteKey + "', {action: 'homepage'}).then(function(token) {" + Environment.NewLine);
            sb.Append("          document.getElementById('g-recaptcha-response').value = token;" + Environment.NewLine);
            sb.Append("         });" + Environment.NewLine);
            sb.Append("     });" + Environment.NewLine);
            sb.Append(" });" + Environment.NewLine);
            sb.Append("</script>" + Environment.NewLine);

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}
