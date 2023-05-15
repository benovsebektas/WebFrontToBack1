using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFrontToBack.DAL;
using WebFrontToBack.Models;

namespace WebFrontToBack.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class TeamMenberController : Controller
    {

        private readonly AppDbContext _context;

        public TeamMenberController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<TeamMenber> teamMenbers = await _context.teamMenbers.ToListAsync();
            return View(teamMenbers);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeamMenber teamMenber)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isExists = await _context.teamMenbers.AnyAsync(c =>
            c.fullname.ToLower().Trim() == teamMenber.fullname.ToLower().Trim());

            if (isExists)
            {
                ModelState.AddModelError("Name", "Service name already exists");
                return View();
            }
            await _context.teamMenbers.AddAsync(teamMenber);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int Id)
        {
            TeamMenber? teamMenber = _context.teamMenbers.Find(Id);

            if (teamMenber == null)
            {
                return NotFound();
            }

            return View(teamMenber);
        }

        [HttpPost]
        public IActionResult Update(TeamMenber teamMenber)
        {
            TeamMenber? editedTeaMenber = _context.teamMenbers.Find(teamMenber.id);
            if (editedTeaMenber == null)
            {
                return NotFound();
            }
            editedTeaMenber.fullname = teamMenber.fullname;
            _context.teamMenbers.Update(editedTeaMenber);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int Id)
        {
            TeamMenber? teamMenber = _context.teamMenbers.Find(Id);
            if (teamMenber == null)
            {
                return NotFound();
            }
            _context.teamMenbers.Remove(teamMenber);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}