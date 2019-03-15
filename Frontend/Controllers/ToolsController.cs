using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;
using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Frontend.Controllers
{
    public class ToolsController : Controller
    {
        private const string BaseUrl = "https://localhost:5003/api/Tools";
        // GET: Tools1
        public async Task<IActionResult> Index()
        {
            const string url = BaseUrl;
            var tools = await url.GetJsonAsync<List<Tool>>();
            return View(tools);
        }

        // GET: Tools1/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var url = $"{BaseUrl}/{id}";
            var tool = await url.GetJsonAsync<Tool>();

            return View(tool);
        }

        // GET: Tools1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tools1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Purchased,Product,Model,SerialNumber,Type")] Tool tool)
        {
            if (!ModelState.IsValid) return View(tool);
            var url = BaseUrl;
            await url.PostJsonAsync(tool);
            return View(tool);
        }

        // GET: Tools1/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = $"{BaseUrl}/{id}";
            var toolBox = await url.GetJsonAsync<Tool>();

            return View(toolBox);
        }

        // POST: Tools1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Purchased,Product,Model,SerialNumber,Type")] Tool tool)
        {
            if (id != tool.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(tool);
            {
                if (id != tool.Id)
                {
                    return NotFound();
                }
                var url = $"{BaseUrl}/{id}";
                await url.PutJsonAsync(tool);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Tools1/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = $"{BaseUrl}/{id}";
            var toolBox = await url.GetJsonAsync<Tool>();

            return View(toolBox);
        }

        // POST: Tools1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var url = $"{BaseUrl}/{id}";
            await url.DeleteAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
