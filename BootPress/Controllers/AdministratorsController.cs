using BootPress.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BootPress.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministratorsController : BaseController
    {
        // GET: Administrators
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> News()
        {
            InitiateConfig();
            var news = await db.News.Include("Author")
                .Include("Category").Where(x => x.IsDeleted == false)
                .ToListAsync();
            return View(news);
        }
        public async Task<ActionResult> NewsPublished()
        {
            InitiateConfig();
            var news = await db.News.Include("Author")
                .Include("Category").Where(x => x.IsDeleted == false && x.Status == Status.Published)
                .ToListAsync();
            return View(news);
        }
        [HttpGet]
        public async Task<ActionResult> NewsDetail(string permalink)
        {
            InitiateConfig();
            if (permalink == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = await db.News.Include("Author")
                .Include("Category").Where(x => x.Permalink == permalink)
                .SingleOrDefaultAsync();
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }
        [HttpPost]
        public async Task<ActionResult> NewsDelete(string permalink)
        {
            if (permalink == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = await db.News.Include("Author")
                .Include("Category").Where(x => x.Permalink == permalink)
                .SingleOrDefaultAsync();
            if (news == null)
            {
                return HttpNotFound();
            }
            else
            {
                news.IsDeleted = true;
                news.Updated = DateTimeOffset.Now;
                var result = await db.SaveChangesAsync();
                if(result > 0)
                    return RedirectToAction("News");
            }
            return RedirectToAction("NewsDetail", new { permalink = news.Permalink });
        }
        [HttpPost]
        public async Task<ActionResult> NewsApprove(string permalink)
        {
            if (permalink == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var adminUser = await db.Staffs.Where(x => x.UserName == User.Identity.Name)
                .SingleOrDefaultAsync();
            var news = await db.News.Include("Author")
                .Include("Category").Where(x => x.Permalink == permalink)
                .SingleOrDefaultAsync();
            if (news == null)
            {
                return HttpNotFound();
            }
            else
            {
                news.Status = Status.Published;
                news.Updated = DateTimeOffset.Now;
                var newsTransaction = new NewsTransaction()
                {
                    News = news,
                    Staff = adminUser,
                    Approved = DateTimeOffset.Now,
                    IdTransaction = Guid.NewGuid().ToString(),
                    Writer = news.Author,
                    Price = news.Price
                };
                db.NewsTransactions.Add(newsTransaction);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                    return RedirectToAction("News");
            }
            return RedirectToAction("NewsDetail", new { permalink = news.Permalink });
        }
    }
}