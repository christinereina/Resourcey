using Microsoft.AspNetCore.Mvc;
using Resourcey.Models;
using Resourcey.Data; 
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Resourcey.Controllers
{
[Authorize]
public class SectionsController : Controller
  {
    private readonly ApplicationDbContext _db;
    private readonly UserManager <ApplicationUser> _userManager;

    public SectionsController (UserManager<ApplicationUser> userManager, ApplicationDbContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Create(int classroomId)
    {
      ViewBag.ClassroomId = classroomId;
      return View();
    }

    [HttpPost]
    public ActionResult Create(Section section)
    {
      _db.Sections.Add(section);
      _db.SaveChanges();
      return RedirectToAction("Details", "Classroom", new {id = section.ClassroomId}); 
    }

    public ActionResult Details(int id)
    {
      var thisSection = _db.Sections
      .Include(section => section.Resources)
      .FirstOrDefault(section => section.SectionId == id);
      return View(thisSection);
    }

    public ActionResult Edit(int id)
    {
      var thisSection = _db.Sections.FirstOrDefault(section => section.SectionId == id);
      return View(thisSection);
    }

    [HttpPost]
    public ActionResult Edit(Section section)
    {
      _db.Entry(section).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", "Classroom", new {id = section.ClassroomId});
    }

    public ActionResult Delete(int id)
    {
      var thisSection = _db.Sections.FirstOrDefault(section => section.SectionId == id);
      return View(thisSection);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisSection = _db.Sections.FirstOrDefault(sections => sections.SectionId == id);
      _db.Sections.Remove(thisSection);
      _db.SaveChanges();
      return RedirectToAction("Details", "Classroom", new {id = thisSection.ClassroomId});
    }
  }
}