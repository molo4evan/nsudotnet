using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebEffectiveWorkersMVC.DB;
using WebEffectiveWorkersMVC.Models;

namespace WebEffectiveWorkersMVC.Controllers {
    public class DataController : Controller {
        public DataController(WPDataContext ctx) {
            _context = ctx;
        }

        private readonly WPDataContext _context;

        public void AddWorker(string lastName, string firstName, string patronymic) {
            var worker = new Worker {
                FirstName = firstName,
                LastName = lastName,
                Patronymic = patronymic
            };
            _context.Workers.Add(worker);
            _context.SaveChanges();
        }

        public IEnumerable<Worker> GetWorkers(string lastName = null, string firstName = null, string patronymic = null) {
            return _context.Workers.Where(w =>
                    (string.IsNullOrEmpty(lastName) || w.LastName == lastName) &&
                    (string.IsNullOrEmpty(firstName) || w.FirstName == firstName) &&
                    (string.IsNullOrEmpty(patronymic) || w.Patronymic == patronymic)
                )
                .Include(w => w.Projects)
                .ToList();
        }

        public void RemoveWorker(Worker worker) {
            if (worker == null) return;

            _context.Workers.Attach(worker);
            _context.Workers.Remove(worker);
            _context.SaveChanges();
        }

        public void ChangeWorker(Worker worker, string newLastName, string newFirstName, string newPatronymic) {
            if (worker == null) return;

            _context.Workers.Attach(worker);
            worker.LastName = newLastName;
            worker.FirstName = newFirstName;
            worker.Patronymic = newPatronymic;
            _context.SaveChanges();
        }

        public void AddProject(string name, decimal price, Worker owner) {
            var project = new Project {
                Name = name,
                Price = price
            };
            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public IEnumerable<Project> GetProjects(string name) {
            return _context.Projects.Where(p => p.Name == name).Include(p => p.Worker).ToList();
        }

        public void RemoveProject(Project project) {
            if (project == null) return;

            _context.Projects.Attach(project);
            _context.Projects.Remove(project);
            _context.SaveChanges();
        }

        public void AddProjectToWorker(Worker worker, Project project) {
            if (project == null || worker == null) return;

            _context.Projects.Attach(project);
            project.Worker = worker;
            _context.SaveChanges();
        }

        public void SetProjectName(Project project, string newName) {
            if (project == null) return;

            _context.Projects.Attach(project);
            project.Name = newName;
            _context.SaveChanges();
        }

        public void SetProjectPrice(Project project, decimal newPrice) {
            if (project == null || newPrice < 0) return;

            _context.Projects.Attach(project);
            project.Price = newPrice;
            _context.SaveChanges();
        }

//        public IEnumerable<WorkerInfo> GetWorkersPrice() {
//            var workers = _context.Workers.Include(w => w.Projects);
//            return workers.Select(
//                worker => new WorkerInfo {
//                    FirstName = worker.FirstName,
//                    LastName = worker.LastName,
//                    Patronymic = worker.Patronymic,
//                    SumPrice = worker.Projects.Sum(item => item.Price)
//                }
//            );
//        }
    }
}