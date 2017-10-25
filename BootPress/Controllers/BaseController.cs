using BootPress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootPress.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
        // GET: Base
        public void InitiateConfig()
        {
            var userConfig = db.Users.Where(x => x.UserName == User.Identity.Name)
                .SingleOrDefault();
            ViewBag.FullName = userConfig.FullName;
            ViewBag.Email = userConfig.Email;
            ViewBag.Avatar = userConfig.AvatarUrl;
        }
    }
}