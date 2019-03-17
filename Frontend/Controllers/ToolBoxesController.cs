using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;
using Frontend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class ToolBoxesController : Controller
    {
        private readonly string _baseUrl;
        private string _baseCraftsmenUrl;

        public ToolBoxesController(IHostingEnvironment env)
        {
            _baseUrl = env.IsDevelopment() ? "https://localhost:5003/api/ToolBoxes" : "http://g6backend/api/ToolBoxes";
            _baseCraftsmenUrl = env.IsDevelopment() ? "https://localhost:5003/api/Craftsmen" : "http://g6backend/api/Craftsmen";
        }

        // GET: ToolBoxes1
        public async Task<IActionResult> Index()
        {
            var url = _baseUrl;
            var toolBoxes = await url.GetJsonAsync<List<ToolBox>>();
            return View( "Index",toolBoxes);
        }

        // GET: ToolBoxes1/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = $"{_baseUrl}/{id}";
            var toolBox = await url.GetJsonAsync<ToolBox>();

            return View(toolBox);
        }

        // GET: ToolBoxes1/Create
        public async Task<IActionResult> Create()
        {
            var url = _baseCraftsmenUrl;
            var craftsmen = await url.GetJsonAsync<List<Craftsman>>();
            ViewBag.craftsmen = craftsmen;
            return View();
        }

        // POST: ToolBoxes1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CraftsmanId,Purchased,Color,Model,SerialNumber")] ToolBox toolBox)
        {
            var url = _baseUrl;
            await url.PostJsonAsync(toolBox);
            return await Index();
        }

        // GET: ToolBoxes1/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = $"{_baseUrl}/{id}";
            var toolBox = await url.GetJsonAsync<ToolBox>();

            return View(toolBox);
        }

        // POST: ToolBoxes1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Purchased,Color,Model,SerialNumber")] ToolBox toolBox)
        {
            if (id != toolBox.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(toolBox);
            {
                if (id != toolBox.Id)
                {
                    return NotFound();
                }
                var url = $"{_baseUrl}/{id}";
                await url.PutJsonAsync(toolBox);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ToolBoxes1/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = $"{_baseUrl}/{id}";
            var toolBox = await url.GetJsonAsync<ToolBox>();

            return View(toolBox);
        }

        // POST: ToolBoxes1/Delete/5
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
