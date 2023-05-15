using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFrontToBack.DAL;
using WebFrontToBack.Models;

namespace WebFrontToBack.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _appContext;
        public AboutController(AppDbContext appContext)
        {
            _appContext = appContext;
            
        }
        public async Task<IActionResult> Index()
        {
            List<TeamMenber>menbers =await _appContext.teamMenbers.ToListAsync();
            return View(menbers);
        }
    }
}
