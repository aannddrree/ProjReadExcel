using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjReadExcel.Web.Data;
using ProjReadExcel.Web.Models;

namespace ProjReadExcel.Web.Controllers
{
    public class DataColumnsController : Controller
    {
        private readonly ProjReadExcelWebContext _context;

        public DataColumnsController(ProjReadExcelWebContext context)
        {
            _context = context;
        }

        // GET: DataColumns
        public async Task<IActionResult> Index()
        {
            return View(await _context.DataColumn.ToListAsync());
        }

        // GET: DataColumns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataColumn = await _context.DataColumn
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dataColumn == null)
            {
                return NotFound();
            }

            return View(dataColumn);
        }

        // GET: DataColumns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DataColumns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] DataColumn dataColumn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataColumn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dataColumn);
        }

        // GET: DataColumns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataColumn = await _context.DataColumn.FindAsync(id);
            if (dataColumn == null)
            {
                return NotFound();
            }
            return View(dataColumn);
        }

        // POST: DataColumns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] DataColumn dataColumn)
        {
            if (id != dataColumn.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataColumn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataColumnExists(dataColumn.Id))
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
            return View(dataColumn);
        }

        // GET: DataColumns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataColumn = await _context.DataColumn
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dataColumn == null)
            {
                return NotFound();
            }

            return View(dataColumn);
        }

        // POST: DataColumns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataColumn = await _context.DataColumn.FindAsync(id);
            _context.DataColumn.Remove(dataColumn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DataColumnExists(int id)
        {
            return _context.DataColumn.Any(e => e.Id == id);
        }
    }
}
