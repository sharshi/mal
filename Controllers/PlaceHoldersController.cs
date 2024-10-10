using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shafeh.Models;

namespace Shafeh.Controllers;

public class PlaceHoldersController : Controller
{
    private readonly ShafehContext _context;

    public PlaceHoldersController(ShafehContext context)
    {
        _context = context;
    }

    // GET: PlaceHolders
    public async Task<IActionResult> Index()
    {
        return View(await _context.PlaceHolder.ToListAsync());
    }

    // GET: PlaceHolders/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var placeHolder = await _context.PlaceHolder
            .FirstOrDefaultAsync(m => m.Id == id);
        if (placeHolder == null)
        {
            return NotFound();
        }

        return View(placeHolder);
    }

    // GET: PlaceHolders/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PlaceHolders/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] PlaceHolder placeHolder)
    {
        if (ModelState.IsValid)
        {
            _context.Add(placeHolder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(placeHolder);
    }

    // GET: PlaceHolders/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var placeHolder = await _context.PlaceHolder.FindAsync(id);
        if (placeHolder == null)
        {
            return NotFound();
        }
        return View(placeHolder);
    }

    // POST: PlaceHolders/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] PlaceHolder placeHolder)
    {
        if (id != placeHolder.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(placeHolder);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceHolderExists(placeHolder.Id))
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
        return View(placeHolder);
    }

    // GET: PlaceHolders/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var placeHolder = await _context.PlaceHolder
            .FirstOrDefaultAsync(m => m.Id == id);
        if (placeHolder == null)
        {
            return NotFound();
        }

        return View(placeHolder);
    }

    // POST: PlaceHolders/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var placeHolder = await _context.PlaceHolder.FindAsync(id);
        _context.PlaceHolder.Remove(placeHolder);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PlaceHolderExists(int id)
    {
        return _context.PlaceHolder.Any(e => e.Id == id);
    }
}
