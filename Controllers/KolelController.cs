using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shafeh.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Shafeh.Controllers;

[Authorize]
public class KolelController(ShafehContext context) : Controller
{
    // GET: /kolel
    public async Task<IActionResult> Index()
    {
        var kolels = await context.Kolels.ToListAsync();
        return View(kolels);
    }

    // GET: /kolel/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /kolel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Kolel kolel)
    {
        if (ModelState.IsValid)
        {
            context.Add(kolel);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(kolel);
    }

    // GET: /kolel/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var kolel = await context.Kolels.FindAsync(id);
        if (kolel == null)
        {
            return NotFound();
        }
        return View(kolel);
    }

    // POST: /kolel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Kolel kolel)
    {
        if (id != kolel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(kolel);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KolelExists(kolel.Id))
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
        return View(kolel);
    }

    // GET: /kolel/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var kolel = await context.Kolels
            .FirstOrDefaultAsync(m => m.Id == id);
        if (kolel == null)
        {
            return NotFound();
        }

        return View(kolel);
    }

    // POST: /kolel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var kolel = await context.Kolels.FindAsync(id);
        context.Kolels.Remove(kolel);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool KolelExists(int id)
    {
        return context.Kolels.Any(e => e.Id == id);
    }
}
