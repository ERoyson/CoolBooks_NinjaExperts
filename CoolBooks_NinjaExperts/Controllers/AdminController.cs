#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoolBooks_NinjaExperts.Data;
using CoolBooks_NinjaExperts.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CoolBooks_NinjaExperts.Areas.Identity.Data;

namespace CoolBooks_NinjaExperts.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly CoolBooks_NinjaExpertsContext _context;

        public AdminController(CoolBooks_NinjaExpertsContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var test = _context.UserInfo.ToList();
            var userroles = _context.UserRoles.ToList();
            var VM = new List<AdminViewModel>();
            var temp = new List<string>();

            foreach (var item in test)
            {
                temp = userroles.Where(x => x.UserId == item.Id).Select(x => x.RoleId).ToList();
                VM.Add(new AdminViewModel
                {
                    User = item,
                    RoleName = _context.Roles.Where(x => x.Id == temp[0]).Select(x => x.Name).First().ToString()
                });
            }
            
            return View(VM);
        }

        //GET
        [HttpGet]
        public async Task<IActionResult> RoleManager(string? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var VM = new AdminViewModel();
            VM.User = await _context.UserInfo.FindAsync(id);
            VM.Roles =  _context.UserRoles.Where(x => x.UserId == VM.User.Id).ToList();
            VM.RoleList = _context.Roles.ToList();
            var roleId = VM.Roles.Select(x => x.RoleId).FirstOrDefault().ToString();
            VM.RoleName = _context.Roles.Where(x => x.Id == roleId).Select(x => x.Name).First().ToString();

            if (VM.User == null)
            {
                return NotFound();
            }
            return View(VM);
        }
        //POST
        [HttpPost]
        public  IActionResult RoleManager(string RoleId, [Bind("Id,FirstName,LastName,UserName,Email,Created")] UserInfo User)
        {
            var VM = new AdminViewModel();
            var oldUserRole = _context.UserRoles.Where(x => x.UserId == User.Id).FirstOrDefault();

            VM.User = _context.Users.Where(x => x.Id == User.Id).FirstOrDefault();
            var currentRole = oldUserRole.RoleId;
            VM.User.FirstName = User.FirstName;
            VM.User.LastName = User.LastName;
            VM.User.UserName = User.UserName;
            VM.User.Email = User.Email;

            if (RoleId == null || RoleId == currentRole)
            {
                _context.Update(VM.User);
                _context.SaveChanges();
            }

            else if (RoleId != null)
            {
                var newUserRole = new IdentityUserRole<string>() { UserId = User.Id, RoleId = RoleId };

                try
                {
                    _context.Update(VM.User);
                    _context.UserRoles.Add(newUserRole);
                    _context.UserRoles.Remove(oldUserRole);
                    _context.SaveChanges();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (User.Id == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(VM);

        }
    }
}
