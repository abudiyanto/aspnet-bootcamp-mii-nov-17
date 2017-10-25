using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BootPress.Models;
using BootPress.ViewModels;

namespace BootPress.Controllers
{
    [Authorize(Roles = "Author, Administrator")]
    public class NewsController : BaseController
    {
       
        // GET: News
        public async Task<ActionResult> Index()
        {
            //var currentUser = await db.Users.Where(x => x.UserName == User.Identity.Name)
            //    .SingleOrDefaultAsync();
            InitiateConfig();
            var news = await db.News.Include("Author")
                .Include("Category").Where(x => x.Author.UserName == User.Identity.Name)
                .ToListAsync();
            return View(news);
        }

        // GET: News/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }
        [Authorize]
        // GET: News/Create
        public async Task<ActionResult> Create()
        {
            InitiateConfig();
            var categories = await db.Categories.OrderBy(x => x.Name)
                .Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.IdCategory,
                    Selected = false
                }).ToArrayAsync();
            ViewBag.Categories = categories;
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddNews news)
        {
            if (ModelState.IsValid)
            {
                news.Permalink = news.Title.Replace(" ", "-")
                    .Replace("&","").ToLower();
                var user = await db.Users.Where(x => x.UserName == User.Identity.Name)
                    .SingleOrDefaultAsync();
                if(user != null)
                {
                    var addNews = new News();
                    addNews.Author = user;
                    addNews.Permalink = news.Permalink;
                    addNews.Content = news.Content;
                    addNews.Hightlight = news.Hightlight;
                    addNews.Status = Status.Draft;
                    addNews.Created = DateTimeOffset.Now;
                    addNews.Price = news.Price;
                    addNews.Title = news.Title;
                    var newsCategory = await db.Categories.FindAsync(news.Category);
                    if (newsCategory != null)
                    {
                        addNews.Category = newsCategory;
                        db.News.Add(addNews);
                        var result = await db.SaveChangesAsync();
                        if (result > 0)
                            return RedirectToAction("Index");
                    }
                }
                
            }
            var categories = await db.Categories.OrderBy(x => x.Name)
                .Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.IdCategory,
                    Selected = false
                }).ToArrayAsync();
            ViewBag.Categories = categories;
            return View(news);
        }

        // GET: News/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = await db.News.Include("Author")
                .Include("Category").Where(x => x.Permalink == id)
                .SingleOrDefaultAsync();
            if (news == null)
            {
                return HttpNotFound();
            }
            var categories = await db.Categories.OrderBy(x => x.Name)
                .Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.IdCategory,
                    Selected = false
                }).ToArrayAsync();
            foreach(var item in categories)
            {
                if (item.Value.Equals(news.Category.IdCategory))
                {
                    item.Selected = true;
                    break;
                }   
            }
            ViewBag.Categories = categories;
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateNews news)
        {
            if (ModelState.IsValid)
            {
                var currentNews = await db.News.Include("Category")
                    .Where(x => x.Permalink == news.Permalink)
                    .SingleOrDefaultAsync();
                currentNews.Title = news.Title;
                currentNews.Hightlight = news.Hightlight;
                currentNews.Content = news.Content;
                currentNews.Status = news.Status;
                if(!currentNews.Category.IdCategory.Equals(news.Category))
                {
                    var newCategory = await db.Categories.FindAsync(news.Category);
                    if(newCategory != null)
                    {
                        currentNews.Category = newCategory;
                    }
                }
                var result = await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            News news = await db.News.FindAsync(id);
            db.News.Remove(news);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
