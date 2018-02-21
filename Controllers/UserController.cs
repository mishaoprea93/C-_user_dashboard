using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using user_dashboard.Models;

namespace user_dashboard.Controllers {

    public class UserController : Controller {

        private DashboardContext _context;

        public UserController ([FromServices] DashboardContext context) {
            _context = context;
        }

        [HttpGet]
        [Route("loginpage")]
        public IActionResult Login () {
            return View ();
        }

        [HttpPost]
        [Route ("login")]
        public IActionResult LoginProcess (string email, string password) {
            List<User> user = _context.users.Where (u => u.Email == email).ToList ();
            if (user.Count > 0) {
                if (user[0].Password == password) {
                    HttpContext.Session.SetInt32 ("Session", user[0].UserId);
                    return RedirectToAction ("ManageUsers", "Home");
                } else {
                    string error = "Password you entered does not match what we have in our records!";
                    ViewBag.err = error;
                    return View ("Login");
                }
            } else {
                ViewBag.err = "We could not find your email in our database!";
            }
            return View ("Login");
        }

        [HttpGet]
        [Route ("registerpage")]
        public IActionResult Register () {
            return View ();
        }

        [HttpPost]
        [Route ("register")]
        public IActionResult Registration (RegisterViews ruser) {
            if (ModelState.IsValid) {
                List<User> isuser = _context.users.Where (useri => useri.Email == ruser.Email).ToList ();
                if (isuser.Count () > 0) {
                    string message = "There is already another user with this email!Please use other email!";
                    ViewBag.message = message;
                    return View ("Register");
                }
                List<User> allusers = _context.users.ToList ();

                User neWuser = new User () {
                    FirstName = ruser.FirstName,
                    LastName = ruser.LastName,
                    Email = ruser.Email,
                    Password = ruser.Password,
                };
                if (allusers.Count () == 0) {
                    neWuser.Status = "Admin";
                }
                _context.users.Add (neWuser);
                _context.SaveChanges ();
            }
            List<User> users = _context.users.ToList ();
            HttpContext.Session.SetInt32 ("Session", users[users.Count () - 1].UserId);

            return RedirectToAction ("ManageUsers", "Home");
        }

        [HttpGet]
        [Route ("/logout")]
        public IActionResult Logout () {
            HttpContext.Session.Clear ();
            return RedirectToAction ("Index", "Home");
        }

        [HttpGet]
        [Route ("users/new")]
        public IActionResult New () {
            if (HttpContext.Session.GetInt32 ("Session") == null) {
                return RedirectToAction ("Logout", "User");
            }
            return View ();
        }

        [HttpPost]
        [Route ("adduser")]
        public IActionResult AddUser (RegisterViews ruser) {
            if (ModelState.IsValid) {
                List<User> isuser = _context.users.Where (useri => useri.Email == ruser.Email).ToList ();
                if (isuser.Count () > 0) {
                    string message = "There is already another user with this email!Please use other email!";
                    ViewBag.message = message;
                    return View ("New");
                }
                User neWuser = new User () {
                    FirstName = ruser.FirstName,
                    LastName = ruser.LastName,
                    Email = ruser.Email,
                    Password = ruser.Password,
                };
                _context.users.Add (neWuser);
                _context.SaveChanges ();
                return RedirectToAction ("ManageUsers", "Home");
            }
            return View ("New");

        }

        [HttpGet]
        [Route ("users/edit")]
        public IActionResult Edit () {
            int id = Convert.ToInt32 (HttpContext.Session.GetInt32 ("Session"));
            ViewBag.user = _context.users.SingleOrDefault (u => u.UserId == id);
            if (HttpContext.Session.GetInt32 ("Session") == null) {
                return RedirectToAction ("Logout", "User");
            }
            return View ();
        }

        [HttpPost]
        [Route ("users/saveinfo")]
        public IActionResult SaveInfo (RegisterViews reg) {
            Console.WriteLine ("%%%%%%%%%%%%%%%%%%" + reg);
            int id = Convert.ToInt32 (HttpContext.Session.GetInt32 ("Session"));
            User user = _context.users.SingleOrDefault (u => u.UserId == id);
            user.FirstName = reg.FirstName;
            user.LastName = reg.LastName;
            user.Email = reg.Email;
            _context.SaveChanges ();
            return RedirectToAction ("Edit");
        }

        [HttpPost]
        [Route ("users/savepassword")]
        public IActionResult SavePassword (RegisterViews reg) {
            int id = Convert.ToInt32 (HttpContext.Session.GetInt32 ("Session"));
            User user = _context.users.SingleOrDefault (u => u.UserId == id);
            if (reg.Password != reg.PasswordConfirmation) {
                ViewBag.error = "Password and password confirmation do not match!";
                ViewBag.user = user;
                return View ("Edit");
            } else {
                user.Password = reg.Password;
                _context.SaveChanges ();
            }
            return RedirectToAction ("Edit");
        }

        [HttpGet]
        [Route ("users/edit/{id}")]
        public IActionResult EditById (int id) {
            ViewBag.user = _context.users.SingleOrDefault (u => u.UserId == id);
            return View ();
        }

        [HttpPost]
        [Route ("users/{id}/saveinfo")]
        public IActionResult SaveInfo (RegisterViews reg, int id) {
            User user = _context.users.SingleOrDefault (u => u.UserId == id);
            user.FirstName = reg.FirstName;
            user.LastName = reg.LastName;
            user.Email = reg.Email;
            user.Status = reg.Status;
            _context.SaveChanges ();
            return RedirectToAction ("EditById");
        }

        [HttpPost]
        [Route ("users/{id}/savepassword")]
        public IActionResult SavePassword (RegisterViews reg, int id) {
            User user = _context.users.SingleOrDefault (u => u.UserId == id);
            if (reg.Password != reg.PasswordConfirmation) {
                ViewBag.error = "Password and password confirmation do not match!";
                ViewBag.user = user;
                return View ("EditById");
            } else {
                user.Password = reg.Password;
                _context.SaveChanges ();
            }
            return RedirectToAction ("EditById");
        }

        [HttpGet]
        [Route ("users/remove/{id}")]
        public IActionResult Remove (int id) {
            User rmUser = _context.users.SingleOrDefault (u => u.UserId == id);
            _context.users.Remove (rmUser);
            _context.SaveChanges ();
            return RedirectToAction ("ManageUsers", "Home");
        }

        [HttpGet]
        [Route ("users/show/{id}")]
        public IActionResult Show (int id) {
            if (HttpContext.Session.GetInt32 ("Session") == null) {
                return RedirectToAction ("Logout");
            }
            ViewBag.user = _context.users.SingleOrDefault (u => u.UserId == id);
            HttpContext.Session.SetInt32 ("Profile", id);
            List<Message> Messages = _context.messages.Include (m => m.User).Include (m => m.Comments).ThenInclude (comment => comment.User).Where (p => p.ProfileId == id).ToList ();
            ViewBag.messages = Messages;
            return View ();
        }

        [HttpPost]
        [Route ("users/savedesc")]
        public IActionResult SaveDescription (string Description) {
            int id = Convert.ToInt32 (HttpContext.Session.GetInt32 ("Session"));
            User user = _context.users.SingleOrDefault (u => u.UserId == id);
            user.Description = Description;
            _context.SaveChanges ();
            ViewBag.user = _context.users.SingleOrDefault (u => u.UserId == id);
            return RedirectToAction ("Edit");
        }

        
    }
    

}