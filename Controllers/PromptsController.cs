using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LLM_Interaction.Data;
using LLM_Interaction.Models;
using LLM_Interaction.Services;
namespace LLM_Interaction.Controllers
{
    public class PromptsController : Controller
    {
        private LLMDbContext db = new LLMDbContext();

        public ActionResult Execute()
        {
            ViewBag.Models = new SelectList(db.Models, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Execute(int modelId, string promptText)
        {
            var model = db.Models.Find(modelId);
            var provider = model.Provider;

            // Call LLM API using HttpClient
            var result = await LLMService.ExecutePromptAsync(provider, model, promptText);

            ViewBag.Models = new SelectList(db.Models, "Id", "Name");
            ViewBag.Result = result;
            return View();
        }


        // GET: Prompts
        public async Task<ActionResult> Index()
        {
            var prompts = db.Prompts.Include(p => p.Model);
            return View(await prompts.ToListAsync());
        }

        // GET: Prompts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prompt prompt = await db.Prompts.FindAsync(id);
            if (prompt == null)
            {
                return HttpNotFound();
            }
            return View(prompt);
        }

        // GET: Prompts/Create
        public ActionResult Create()
        {
            ViewBag.ModelId = new SelectList(db.Models, "Id", "Name");
            return View();
        }

        // POST: Prompts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Content,Tags,ModelId")] Prompt prompt)
        {
            if (ModelState.IsValid)
            {
                db.Prompts.Add(prompt);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ModelId = new SelectList(db.Models, "Id", "Name", prompt.ModelId);
            return View(prompt);
        }

        // GET: Prompts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prompt prompt = await db.Prompts.FindAsync(id);
            if (prompt == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModelId = new SelectList(db.Models, "Id", "Name", prompt.ModelId);
            return View(prompt);
        }

        // POST: Prompts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Content,Tags,ModelId")] Prompt prompt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prompt).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ModelId = new SelectList(db.Models, "Id", "Name", prompt.ModelId);
            return View(prompt);
        }

        // GET: Prompts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prompt prompt = await db.Prompts.FindAsync(id);
            if (prompt == null)
            {
                return HttpNotFound();
            }
            return View(prompt);
        }

        // POST: Prompts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Prompt prompt = await db.Prompts.FindAsync(id);
            db.Prompts.Remove(prompt);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
