using System.Collections.Generic;
using Microsoft.Owin.StaticFiles.ContentTypes;

namespace Eumis.Web.Host.Owin
{
    public class ContentTypeProvider : FileExtensionContentTypeProvider
    {
        public ContentTypeProvider()
            : base(new Dictionary<string, string>()
            {
                { ".htm", "text/html" },
                { ".html", "text/html" },
                { ".js", "application/javascript" },
                { ".css", "text/css" },
                { ".map", "application/json" },
                { ".ttf", "application/x-font-ttf" },
                { ".eot", "application/vnd.ms-fontobject" },
                { ".svg", "image/svg+xml" },
                { ".otf", "application/x-font-opentype" },
                { ".woff", "application/font-woff" },
                { ".woff2", "application/font-woff2" },
                { ".png", "image/png" },
                { ".jpg", "image/jpeg" },
                { ".gif", "image/gif" },
                { ".ico", "image/x-icon" },
            })
        {
        }
    }
}