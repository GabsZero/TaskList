using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TasksList.Data;
using TasksList.Models;

namespace TasksList.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly ApplicationContext _context;

        public AssignmentsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Assignments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tasks.ToListAsync());
        }

        // GET: Assignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                PageNotFound();
            }

            var assignment = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignment == null)
            {
                return PageNotFound();
            }

            return View(assignment);
        }

        // GET: Assignments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StartDate,ExpireDate,Weight")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assignment);
        }

        // GET: Assignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                PageNotFound();
            }

            var assignment = await _context.Tasks.FindAsync(id);
            if (assignment == null)
            {
                PageNotFound();
            }
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate,ExpireDate,Weight")] Assignment assignment)
        {
            if (id != assignment.Id)
            {
                PageNotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentExists(assignment.Id))
                    {
                        PageNotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                PageNotFound();
            }

            var assignment = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignment == null)
            {
                PageNotFound();
            }

            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(assignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }

        private IActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            return View("404");
        }
    }
}
