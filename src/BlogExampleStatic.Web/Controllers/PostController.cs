using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogExampleStatic.Common.Entities;
using BlogExampleStatic.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogExampleStatic.Web.Controllers
{
    public class PostController: Controller
    {
        private readonly BlogExampleDbContext dbContext;
        private readonly UserManager<ApplicationUserEntity> userManager;

        public PostController(BlogExampleDbContext dbContext, UserManager<ApplicationUserEntity> userManager )
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<PostEntity> posts = this.dbContext.Posts
                .Include(x=>x.Author)
                .OrderByDescending(x=>x.CreateDate)
                .ToList();
            return View(posts);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(long? id)
        {
            PostEntity entity = id.HasValue 
                ? this.dbContext.Posts.FirstOrDefault(x => x.Id == id.Value) : new PostEntity();

            return View(entity);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(PostEntity model)
        {
            ApplicationUserEntity user = this.userManager.GetUserAsync(this.User).Result;

            var entity = this.dbContext.Posts.FirstOrDefault(x => x.Id == model.Id);
            if(entity == null) {
                entity = new PostEntity
                {
                    CreateDate = DateTime.Now,
                    Author = user
                };
                this.dbContext.Posts.Add(entity);
            }

            entity.Title = model.Title;
            entity.Text = model.Text;

            this.dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
