using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab2.Models.Repositories;
using Lab2.Models.Entities;
using Lab2.ViewModels;
using Lab2.Models.SessionManager;

namespace Lab2.Controllers
{
    public class ForumsController : Controller {
        //
        // GET: /Forums/

        public ActionResult Index() {
            List<ForumThread> threads = Repository.Instance.GetSortedThreads();
            return View(threads);
        }

        public ActionResult Thread(Guid id) {
            ThreadViewModel vm = new ThreadViewModel();
            vm.Posts = Repository.Instance.GetPostsByThreadID(id);
            vm.Thread = Repository.Instance.Get<ForumThread>(id);
            return View(vm);
        }

        //Post: Get/
        [Authorize]
        public ActionResult CreatePost(Guid? threadID = null) {
            var post = new Post();
            if (threadID.HasValue)
                post.ThreadID = threadID.Value;
            return View(post);
        }
        //Post: /Create/
        [HttpPost]
        public ActionResult CreatePost(Post post) {
            if (ModelState.IsValid) {
                if (post.ThreadID == Guid.Empty) {
                    ForumThread thread = new ForumThread();
                    thread.ID = Guid.NewGuid();
                    post.CreateDate = DateTime.Now;
                    post.ThreadID = thread.ID;
                    thread.Title = post.Title;
                    thread.CreateDate = DateTime.Now;

                    Repository.Instance.Save<ForumThread>(thread);
                    Repository.Instance.Save<Post>(post);
                    return RedirectToAction("Thread", new { id = thread.ID });
                }
                else {
                    post.CreateDate = DateTime.Now;
                    Repository.Instance.Save<Post>(post);
                    return RedirectToAction("Thread", new { id = post.ThreadID });
                }
            }
            return View();
        }
          

           
        

    }
}
