using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using user_dashboard.Models;
using System.Linq;

namespace user_dashboard.Controllers
{
    public class HomeController : Controller
    {
        private DashboardContext _context;

        public HomeController ([FromServices] DashboardContext context) {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("manage")]
        public IActionResult ManageUsers(){
            ViewBag.users=_context.users.ToList();
            int id=Convert.ToInt32(HttpContext.Session.GetInt32("Session"));
            if(HttpContext.Session.GetInt32("Session")==null){
                return RedirectToAction("Logout","User");
            }
            ViewBag.status=_context.users.SingleOrDefault(u=>u.UserId==id).Status;
            return View();
        }

        [HttpPost]
        [Route("addmessage")]
        public IActionResult AddMessage(string message){
            int id=Convert.ToInt32(HttpContext.Session.GetInt32("Session"));
            Message newMessage=new Message(){

                UserId=id,
                ProfileId=Convert.ToInt32(HttpContext.Session.GetInt32("Profile")),
                Content=message,
            };
            _context.messages.Add(newMessage);
            _context.SaveChanges();
            return RedirectToAction ("Show","User",new{id=newMessage.ProfileId});
        }

        [HttpPost]
        [Route("addcomment")]
        public IActionResult AddComment(string comment, int messageid){
            int id=Convert.ToInt32(HttpContext.Session.GetInt32("Session"));
            int ProfileId=Convert.ToInt32(HttpContext.Session.GetInt32("Profile"));
            Comment newComment=new Comment(){
                Content=comment,
                MessageId=messageid,
                UserId=id,
            };
            _context.comments.Add(newComment);
            _context.SaveChanges();
            return RedirectToAction("Show","User",new{id=ProfileId});
        }
    }

}
