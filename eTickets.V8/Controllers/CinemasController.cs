using eTickets.V8.Data;
using eTickets.V8.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.V8.Controllers
{
    public class CinemasController : Controller
    {

        private readonly ICinemasService _service;

        public CinemasController(ICinemasService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var AllCinemas = await _service.GetAllAsync();
            return View(AllCinemas);
        }
        public IActionResult Create()
        {
            return View();
        }

    }
}
