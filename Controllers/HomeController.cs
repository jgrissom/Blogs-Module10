﻿using Blogs.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Blogs.Controllers
{
    public class HomeController : Controller
    {
        // this controller depends on the BloggingRepository
        private IBloggingRepository repository;
        public HomeController(IBloggingRepository repo) => repository = repo;

        public IActionResult Index() => View(repository.Blogs.OrderBy(b => b.Name));
        [Authorize(Roles = "moderate")]
        public IActionResult AddBlog() => View();
        [HttpPost, Authorize(Roles = "moderate"), ValidateAntiForgeryToken]
        public IActionResult AddBlog(Blog model)
        {
            if (ModelState.IsValid)
            {
                if (repository.Blogs.Any(b => b.Name == model.Name))
                {
                    ModelState.AddModelError("", "Name must be unique");
                }
                else
                {
                    repository.AddBlog(model);
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [Authorize(Roles = "moderate")]
        public IActionResult DeleteBlog(int id)
        {
            repository.DeleteBlog(repository.Blogs.FirstOrDefault(b => b.BlogId == id));
            return RedirectToAction("Index");
        }

        public IActionResult BlogDetail(int id) => View(new PostViewModel
        {
            blog = repository.Blogs.FirstOrDefault(b => b.BlogId == id),
            Posts = repository.Posts.Where(p => p.BlogId == id)
        });

        public IActionResult AddPost(int id)
        {
            ViewBag.BlogId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPost(int id, Post post)
        {
            post.BlogId = id;
            if (ModelState.IsValid)
            {
                repository.AddPost(post);
                return RedirectToAction("BlogDetail", new { id = id });
            }
            @ViewBag.BlogId = id;
            return View();
        }

        public IActionResult DeletePost(int id)
        {
            Post post = repository.Posts.FirstOrDefault(p => p.PostId == id);
            int BlogId = post.BlogId;
            repository.DeletePost(post);
            return RedirectToAction("BlogDetail", new { id = BlogId });
        }
    }
}