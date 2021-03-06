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
  public class ResourcesController : Controller
  {
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ResourcesController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Create(int sectionId)
    {      
      ViewBag.SectionId = sectionId;
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Resource resource)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      resource.ResourceCreator = currentUser;
      _db.Resources.Add(resource);
      _db.SaveChanges();
      return RedirectToAction("Details", "Sections", new { id = resource.SectionId }); 
    }

    public async Task<ActionResult> Details(int id)
    {
      var thisResource = _db.Resources
      .Include(resource => resource.Section)
      .Include(resource => resource.Section.Classroom)
      .FirstOrDefault(resource => resource.ResourceId == id);
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      if(currentUser == thisResource.Section.Classroom.ClassroomCreator || currentUser == thisResource.ResourceCreator)
      {
        return View("Details", thisResource);
      }
      else 
      {
      return View("StudentDetails", thisResource);
      }
    }

    public async Task<ActionResult> Edit(int id)
    {
      var thisResource = _db.Resources
      .Include(resource => resource.Section)
      .Include(resource => resource.Section.Classroom)
      .FirstOrDefault(resource => resource.ResourceId == id);
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      if(currentUser == thisResource.Section.Classroom.ClassroomCreator || currentUser == thisResource.ResourceCreator)
      {
        return View(thisResource);
      }
      else
      {
        return RedirectToAction("Index", "Classrooms");
      }
    }

    [HttpPost]
    public ActionResult Edit(Resource resource)
    {
      _db.Entry(resource).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", "Sections", new { id = resource.SectionId });
    }

    public async Task<ActionResult> Delete(int id)
    {
      var thisResource = _db.Resources
      .Include(resource => resource.Section)
      .Include(resource => resource.Section.Classroom)
      .FirstOrDefault(resource => resource.ResourceId == id);
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      if(currentUser == thisResource.Section.Classroom.ClassroomCreator || currentUser == thisResource.ResourceCreator)
      {
        return View(thisResource);
      }
      else
      {
        return RedirectToAction("Index", "Classrooms");
      }
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisResource = _db.Resources.FirstOrDefault(resource => resource.ResourceId == id);
      _db.Resources.Remove(thisResource);
      _db.SaveChanges();
      return RedirectToAction("Details", "Sections", new { id = thisResource.SectionId });
    }
  }
}