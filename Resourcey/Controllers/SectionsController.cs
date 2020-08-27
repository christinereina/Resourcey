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

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Section section)
    {
      _db.Sections.Add(section);
      _db.SaveChanges();
      return RedirectToAction("Details", "Classrooms", new {id = section.ClassroomId}); 
    }

    public async Task<ActionResult> Details(int id)
    {
      var thisSection = _db.Sections
      .Include(section => section.Resources)
      .Include(section => section.Classroom)
      .FirstOrDefault(section => section.SectionId == id);
      TempData["sectionId"] = id;
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      if(currentUser == thisSection.Classroom.ClassroomCreator)
      {
        return View("Details", thisSection);
      }
      else
      {
        return View("StudentDetails", thisSection);
      }
    }

    public async Task<ActionResult> Edit(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisSection = _db.Sections.Include(section => section.Classroom).FirstOrDefault(section => section.SectionId == id);
      if(currentUser == thisSection.Classroom.ClassroomCreator)
      {
        return View(thisSection);
      }
      else
      {
        return RedirectToAction("Index", "Classrooms");
      }
    }

    [HttpPost]
    public ActionResult Edit(Section section)
    {
      _db.Entry(section).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", "Classrooms", new {id = section.ClassroomId});
    }

    public async Task<ActionResult> Delete(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisSection = _db.Sections.Include(section => section.Classroom).FirstOrDefault(section => section.SectionId == id);
      if(currentUser == thisSection.Classroom.ClassroomCreator)
      {
        return View(thisSection);
      }
      else
      {
        return RedirectToAction("Index", "Classrooms");
      }
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisSection = _db.Sections.FirstOrDefault(sections => sections.SectionId == id);
      _db.Sections.Remove(thisSection);
      _db.SaveChanges();
      return RedirectToAction("Details", "Classrooms", new {id = thisSection.ClassroomId});
    }
  }
}