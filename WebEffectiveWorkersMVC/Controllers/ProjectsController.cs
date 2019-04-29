using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebEffectiveWorkersMVC.DB;
using WebEffectiveWorkersMVC.Models;

namespace WebEffectiveWorkersMVC.Controllers {
    public class ProjectsController: Controller {
        private readonly WPDataContext _context;

        public ProjectsController(WPDataContext context) {
            _context = context;
        }

        public ActionResult Show() {
            return View(_context.Projects.Include(p => p.Worker).ToList());
        }
        
        [HttpGet]
        public ActionResult Add() {
            var workersData = _context.Workers.Include(w => w.Projects).ToList();
            var workers = new SelectList(workersData, "Id", "SecondName");
            ViewBag.Workers = workers;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Project project) {
            _context.Projects.Add(project);
            _context.SaveChanges();
            return Redirect("/projects");
        }

        [HttpGet]
        public ActionResult Edit(int? id) {
            if (id == null) return NotFound();
            var project = _context.Projects.Find(id);
            if (project != null) return View(project);
            return NotFound();
        }

        [HttpPost]
        public ActionResult Edit(Project project) {
            _context.Entry(project).State = EntityState.Modified;
            _context.SaveChanges();
            return Redirect("/projects");
        }
        
        [HttpGet]
        public ActionResult Delete(int? id) {
            if (id == null) return NotFound();
            var project = _context.Projects.Find(id);
            if (project == null) return NotFound();
            return View(project);
        }

        [HttpPost, ActionName("delete")]
        public ActionResult DeleteConfirmed(int id) {
            var project = _context.Projects.Find(id);
            if (project == null) return NotFound();
            _context.Entry(project).State = EntityState.Deleted;
            _context.SaveChanges();
            return Redirect("/projects");
        }
    }
}