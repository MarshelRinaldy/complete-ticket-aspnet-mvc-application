using eTickets.V8.Models;
using Microsoft.AspNetCore.Mvc;
using eTickets.V8.Data.Services;
using System.Diagnostics;

namespace eTickets.V8.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            return View(actorDetails);
        }

        // GET: Actors/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            return View(actorDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
        {

            await _service.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }

        // GET: Actors/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            return View(actorDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //written down here just for infomartion of ID, i use that for know the Id in parameter.
            Console.WriteLine($"ID yang akan dihapus: {id}");
            Debugger.Break();
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
