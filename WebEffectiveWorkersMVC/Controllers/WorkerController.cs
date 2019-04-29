using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Microsoft.EntityFrameworkCore;
using WebEffectiveWorkersMVC.DB;
using WebEffectiveWorkersMVC.Models;

namespace WebEffectiveWorkersMVC.Controllers {
    public class WorkersController : Controller {
        private readonly WPDataContext _context;

        public WorkersController(WPDataContext context) {
            _context = context;
        }
        
        public IActionResult Show() {
            var workers = _context.Workers.Include(w => w.Projects);
            ViewBag.WorkersPrice = 
                workers.ToDictionary(
                    worker => worker.Id, 
                    worker => worker.Projects.Sum(p => p.Price));
            return View(workers);
        }

        [HttpGet]
        public ActionResult Add() {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Worker worker) {
            _context.Add(worker);
            _context.SaveChanges();
            return Redirect("/workers");
        }

        [HttpGet]
        public ActionResult Edit(int? id) {
            if (id == null) return NotFound();
            var worker = _context.Workers.Find(id);
            if (worker != null) return View(worker);
            return NotFound();
        }

        [HttpPost]
        public ActionResult Edit(Worker worker) {
            _context.Entry(worker).State = EntityState.Modified;
            _context.SaveChanges();
            return Redirect("/workers");
        }

        [HttpGet]
        public ActionResult Delete(int? id) {
            if (id == null) return NotFound();
            var worker = _context.Workers.Find(id);
            if (worker == null) return NotFound();
            return View(worker);
        }

        [HttpPost, ActionName("delete")]
        public ActionResult DeleteConfirmed(int id) {
            var worker = _context.Workers.Find(id);
            if (worker == null) return NotFound();
            _context.Entry(worker).State = EntityState.Deleted;
            _context.SaveChanges();
            return Redirect("/workers");
        }
    }
}