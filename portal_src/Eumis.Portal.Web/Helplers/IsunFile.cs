﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eumis.Portal.Web.Helplers
{
    [Serializable]
    public class IsunFile
    {
        public byte[] Content { get; set; }
        public string MimeType { get; set; }
        public string Filename { get; set; }
    }
}
