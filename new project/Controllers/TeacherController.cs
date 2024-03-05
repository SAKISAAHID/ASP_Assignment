using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using new_project.Models;
using static new_project.Repositary.Repository;

namespace new_project.Controllers
{
    //[Authorize]
    public class TeacherController : Controller
    {
        TeacherRepository repo;

        public TeacherController()
        {
            repo = new TeacherRepository();
        }

        public IActionResult Index()
        {
            try
            {
                var data = repo.getAll();
                return View(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Teacher model)
        {
            try
            {
                repo.create(model.Name, model.Email, model.Phone, model.Salary, model.Course_Title);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                var found = repo.get_by_id(id);
                return View(found);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Edit(Teacher model)
        {
            try
            {
                repo.update(model.id, model.Name, model.Email, model.Phone, model.Salary, model.Course_Title);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var found = repo.get_by_id(id);
                return View(found);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Delete(Teacher model)
        {
            try
            {
                repo.delete(model.id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
