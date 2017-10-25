﻿using BootPress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootPress.ViewModels
{
    public class UpdateNews
    {
        public string Permalink { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Hightlight { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public Status Status { get; set; }
    }
}