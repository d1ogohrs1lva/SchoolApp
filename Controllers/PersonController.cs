using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolApp.SchoolModels;
using System.Data;

namespace SchoolApp.Controllers
{
    public class PersonController : Controller
    {
        //Dados da BD
        DbStudentsContext db_school = new DbStudentsContext();

        // GET: PersonController
        public ActionResult Index(string searchString, int? page)
        {
            var people = from p in db_school.People
                         orderby p.Id
                         where p.Roles == 1
                         select new Person
                         {
                             Id = p.Id,
                             FirstName = p.FirstName,
                             LastName = p.LastName,
                             BirthDate = p.BirthDate,
                             RolesNavigation = p.RolesNavigation
                         };

            // Filtering 
            if (!string.IsNullOrEmpty(searchString))
            {
                people = people.Where(p => p.FirstName.Contains(searchString));
            }

            var count = people.Count();
            int pageSize = 5;
            int pageNr = (page ?? 1);

            var paginatedPeople = people.Skip((pageNr - 1) * pageSize).Take(pageSize).ToList();

            int totalStudents = count;
            int totalPages = (int)Math.Ceiling((double)totalStudents / pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNr;
            ViewBag.CurrentFilter = searchString;

            return View(paginatedPeople);
        }

        // GET: PersonController/Details/5
        public ActionResult Details(int id)
        {
            var data = db_school.People;

            if (data != null)
            {
                var studentDetails = 
                    data.Include(s => s.RolesNavigation).FirstOrDefault(s => s.Id == id); // LINQ (Language-Integrated Query) 

                if (studentDetails != null)
                {
                    return View(studentDetails); // devolver a view do estudante
                }
            }
            return View();
        }

        // GET: PersonController/Create
        public ActionResult Create()
        {
            try
            {
                var roles = db_school.Roles.ToList();
                ViewBag.Roles = new SelectList(roles, "Id", "Label");

                return View();
            }
            catch
            {
                return View();
            }
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Person newPerson)
        {
            try
            {
                var person = new Person()
                {
                    FirstName = newPerson.FirstName,
                    LastName = newPerson.LastName,
                    BirthDate = newPerson.BirthDate,
                    Roles = newPerson.Roles,
                };

                await db_school.People.AddAsync(person);
                await db_school.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var person = await db_school.People
                            .Include(s => s.RolesNavigation)
                            .FirstOrDefaultAsync(s => s.Id == id);

            var roles = db_school.Roles.ToList();
            ViewBag.Roles = new SelectList(roles, "Id", "Label");

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Person updatedPerson)
        {
            try
            {
                var person = await db_school.People.FindAsync(id);

                if (person == null)
                {
                    return NotFound();
                }

                person.FirstName = updatedPerson.FirstName;
                person.LastName = updatedPerson.LastName;
                person.BirthDate = updatedPerson.BirthDate;
                person.RolesNavigation.Label = updatedPerson.RolesNavigation.Label;

                db_school.Entry(person).State = EntityState.Modified;
                await db_school.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(updatedPerson);
            }
        }

        // GET: PersonController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var person = await db_school.People
                            .Include(s => s.RolesNavigation)
                            .FirstOrDefaultAsync(s => s.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: PersonController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var person = await db_school.People.FindAsync(id);

                if (person == null)
                {
                    return NotFound();
                }

                db_school.People.Remove(person);
                await db_school.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
