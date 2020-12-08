using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdentityCore.Data;
using IdentityCore.Models;

namespace IdentityCore.Controllers
{
    public class ApplicationAspNetUserRolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationAspNetUserRolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicationAspNetUserRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationAspNetUserRoles.ToListAsync());
        }

        // GET: ApplicationAspNetUserRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationAspNetUserRoles = await _context.ApplicationAspNetUserRoles
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (applicationAspNetUserRoles == null)
            {
                return NotFound();
            }

            return View(applicationAspNetUserRoles);
        }

        // GET: ApplicationAspNetUserRoles/Create
        public IActionResult Create()
        {
            var mls = (
               from geral in _context.ApplicationUsers
               orderby geral.UserName
               select new { value = geral.Id, text = geral.UserName }
             ).ToList();
            ViewBag.UserId = new SelectList(mls, "value", "text");

            mls = (
               from geral in _context.ApplicationRoles
               orderby geral.Name
               select new { value = geral.Id, text = geral.Name }
             ).ToList();

            ViewBag.RoleId = new SelectList(mls, "value", "text");


            return View();
        }

        // POST: ApplicationAspNetUserRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,RoleId")] ApplicationAspNetUserRoles2 applicationAspNetUserRoles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationAspNetUserRoles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicationAspNetUserRoles);
        }

        // GET: ApplicationAspNetUserRoles/Edit/5
        public async Task<IActionResult> Edit(string RoleId, string UserId)
        {
            if (RoleId == null)
            {
                return NotFound();
            }

            var applicationAspNetUserRoles = await _context.ApplicationAspNetUserRoles.FindAsync(RoleId, UserId);
            if (applicationAspNetUserRoles == null)
            {
                return NotFound();
            }
            return View(applicationAspNetUserRoles);
        }

        // POST: ApplicationAspNetUserRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string RoleId, string UserId, [Bind("RoleId,UserId")] ApplicationAspNetUserRoles applicationAspNetUserRoles)
        {
            if (UserId != applicationAspNetUserRoles.UserId && RoleId != applicationAspNetUserRoles.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationAspNetUserRoles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationAspNetUserRolesExists(applicationAspNetUserRoles.UserId))
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
            return View(applicationAspNetUserRoles);
        }

        // GET: ApplicationAspNetUserRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationAspNetUserRoles = await _context.ApplicationAspNetUserRoles
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (applicationAspNetUserRoles == null)
            {
                return NotFound();
            }

            return View(applicationAspNetUserRoles);
        }

        // POST: ApplicationAspNetUserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationAspNetUserRoles = await _context.ApplicationAspNetUserRoles.FindAsync(id);
            _context.ApplicationAspNetUserRoles.Remove(applicationAspNetUserRoles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationAspNetUserRolesExists(string id)
        {
            return _context.ApplicationAspNetUserRoles.Any(e => e.UserId == id);
        }
    }
}
