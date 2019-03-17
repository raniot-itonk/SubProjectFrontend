using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;
using Frontend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class CraftsmenController : Controller
    {
        private readonly string _baseUrl;

        public CraftsmenController(IHostingEnvironment env)
        {
            _baseUrl = env.IsDevelopment() ? "https://localhost:5003/api/Craftsmen" : "http://g6backend/api/Craftsmen";
        }

        // GET: Craftsmen1
        public async Task<IActionResult> Index()
        {
            var url = _baseUrl;
            var craftsmen = await url.GetJsonAsync<List<Craftsman>>();
            return View(craftsmen);
        }

        // GET: Craftsmen1/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var url = $"{_baseUrl}/{id}";
            var craftsman = await url.GetJsonAsync<Craftsman>();

            return View(craftsman);
        }

        // GET: Craftsmen1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Craftsmen1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateOfEmployment,LastName,SubjectArea,FirstName")] Craftsman craftsman)
        {
            if (!ModelState.IsValid) return View(craftsman);
            var url = _baseUrl;
            await url.PostJsonAsync(craftsman);
            return View(craftsman);
        }

        // GET: Craftsmen1/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var url = $"{_baseUrl}/{id}";
            var craftsman = await url.GetJsonAsync<Craftsman>();

            return View(craftsman);
        }

        // POST: Craftsmen1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,DateOfEmployment,LastName,SubjectArea,FirstName")] Craftsman craftsman)
        {
            if (id != craftsman.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid) return View(craftsman);
            {
                if (id != craftsman.Id)
                {
                    return NotFound();
                }
                var url = $"{_baseUrl}/{id}";
                await url.PutJsonAsync(craftsman);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Craftsmen1/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var url = $"{_baseUrl}/{id}";
            var craftsman = await url.GetJsonAsync<Craftsman>();

            return View(craftsman);
        }

        // POST: Craftsmen1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var url = $"{_baseUrl}/{id}";
            await url.DeleteAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
