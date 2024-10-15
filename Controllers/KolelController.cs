using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shafeh.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Shafeh.Controllers;

public class KolelController(ShafehContext context) : Controller
{
    // GET: /kolel
    public async Task<IActionResult> Index()
    {
        var kolels = await context.Kolels.ToListAsync();
        return View(kolels);
    }

    // GET: /kolel/Details/{id}
    public async Task<IActionResult> Details(int id)
    {
        var kolel = await context.Kolels
            .FirstOrDefaultAsync(k => k.Id == id);

        if (kolel == null)
        {
            return NotFound();
        }

        return View(kolel);
    }

    // GET: /kolel/Create
    [Authorize(Roles = "Admin,KolelAdmin")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: /kolel/Create
    [HttpPost]
    [Authorize(Roles = "Admin,KolelAdmin")]
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
    [Authorize(Roles = "Admin,KolelAdmin")]
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
    [Authorize(Roles = "Admin,KolelAdmin")]
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
    [Authorize(Roles = "Admin,KolelAdmin")]
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
    [Authorize(Roles = "Admin,KolelAdmin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var kolel = await context.Kolels.FindAsync(id);
        context.Kolels.Remove(kolel);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // Post: /kolel/{id}/Join
    [HttpPost]
    [Authorize]
    [Route("kolel/{id}/Join")]
    public async Task<IActionResult> Join(int id)
    {
        var memberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (memberId == null)
            return Unauthorized();
        var kolel = await context.Kolels.FindAsync(id);
        if (kolel == null)
            return NotFound();

        var joinRequest = new JoinRequest
        {
            UserId = memberId,
            KolelId = id,
            RequestDate = DateTime.UtcNow,
            Status = "Pending"
        };

        context.JoinRequests.Add(joinRequest);
        await context.SaveChangesAsync();

        ViewBag.RequestSent = true;

        return View("Details", kolel);
    }

    // POST: /kolel/{kolelId}/approve/{requestId}
    [HttpPost]
    [Authorize(Roles = "Admin,KolelAdmin")]
    [Route("kolel/{kolelId}/approve/{requestId}")]
    public async Task<IActionResult> ApproveJoinRequest(int kolelId, int requestId)
    {
        var joinRequest = await _context.JoinRequests.FindAsync(requestId);
        if (joinRequest == null || joinRequest.KolelId != kolelId)
        {
            return NotFound();
        }

        joinRequest.Status = "Approved";

        var personKolel = new PersonKolel() {
            
        };
        _context.KolelMembers.Add(kolelMember);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", new { id = kolelId });
    }

    private bool KolelExists(int id)
    {
        return context.Kolels.Any(e => e.Id == id);
    }
}
