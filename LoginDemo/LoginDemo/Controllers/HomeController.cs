using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginDemo.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LoginDemo.Controllers
{
    public class HomeController : Controller
    { 
        private static readonly HttpClient client = new HttpClient();
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.Clear();
            return View(await getAllUsers());
        }
        
        public IActionResult Login()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return View();
            return RedirectToAction("Details/");
        }

        [HttpPost, ActionName("Login")]
        public async Task<IActionResult> LoginUser(User user)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                TokenProvider _tokenProvider = new TokenProvider(await getAllUsers());
                var userToken = _tokenProvider.LoginUser(user.Username.Trim(), user.Password.Trim());
                if (userToken != null)
                {
                    HttpContext.Session.SetString("JWToken", userToken);
                    return RedirectToAction("Details/");
                }
                ViewBag.Message = "Incorrect Username or Password";
                return View(user);
            }
            return RedirectToAction("Details/");
        }

        [ActionName("Logoff")]
        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return View(await getUser());
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind] User emp)
        {
            if (ModelState.IsValid)
            {
                List<User> users = await getAllUsers();
                foreach (var user in users)
                {
                    if (user.Username.ToLower().Trim() == emp.Username.ToLower().Trim())
                    {
                        ModelState.AddModelError("Username", "Username already Exists");
                        return View(emp);
                    }
                }
                await client.GetStringAsync("https://localhost:44330/create?name="+emp.Name+"&gender="+emp.Gender+"&department="+emp.Department+"&username="+emp.Username.Trim()+"&password="+emp.Password);
                return RedirectToAction("Index");
            }
            return View(emp);
        }

        //[Authorize]
        public async Task<ActionResult> Delete()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return View(await getUser());
            return RedirectToAction("Login");
        }

        //[Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmp()
        {
            await client.GetStringAsync("https://localhost:44330/delete?id="+Int16.Parse(HttpContext.User.Identity.Name));
            return RedirectToAction("Index");
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return View(await getUser());
            return RedirectToAction("Login");
        }

        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind] User emp)
        {
            if (ModelState.IsValid)
            {
                List<User> users = await getAllUsers();
                foreach (var user in users)
                {
                    if (user.Username.ToLower().Trim() == emp.Username.ToLower().Trim() && user.ID!= Int16.Parse(HttpContext.User.Identity.Name))
                    {
                        ModelState.AddModelError("Username", "Username already Exists");
                        return View(emp);
                    }
                }
                await client.GetStringAsync("https://localhost:44330/edit?id="+Int16.Parse(HttpContext.User.Identity.Name)+"&name="+emp.Name+"&gender="+emp.Gender+"&department="+emp.Department+"&username="+emp.Username.Trim()+"&password="+emp.Password);
                return RedirectToAction("Details/");
            }
            return View(emp);
        }

        public async Task<User> getUser()
        {
            /*return new User { ID= Int16.Parse(HttpContext.User.Identity.Name),
                              Name= HttpContext.User.FindFirst("NAME").Value,
                              Username= HttpContext.User.FindFirst("USERNAME").Value,
                              Gender = HttpContext.User.FindFirst("GENDER").Value,
                              Department = HttpContext.User.FindFirst("PASSWORD").Value,
                              Password = HttpContext.User.FindFirst("DEPARTMENT").Value
                            };*/
            var responseString = await client.GetStringAsync("https://localhost:44330/emp?id="+Int16.Parse(HttpContext.User.Identity.Name));
            JsonTextReader rs = new JsonTextReader(new StringReader(responseString));
            return new JsonSerializer().Deserialize(rs, typeof(User)) as User;
        }

        public async Task<List<User>> getAllUsers()
        {
            var responseString = await client.GetStringAsync("https://localhost:44330/all");
            JsonTextReader rs = new JsonTextReader(new StringReader(responseString));
            return new JsonSerializer().Deserialize(rs, typeof(List<User>)) as List<User>;
        }
    }
}