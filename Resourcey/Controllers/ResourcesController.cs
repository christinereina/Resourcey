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
    public ActionResult Create(Resource resource)
    {
      _db.Resources.Add(resource);
      _db.SaveChanges();
      return RedirectToAction("Details", "Section", new { id = resource.SectionId }); 
    }

    public ActionResult Details(int id)
    {
      var thisResource = _db.Resources.FirstOrDefault(resource => resource.ResourceId == id);
      return View(thisResource);
    }

    public ActionResult Edit(int id)
    {
      var thisResource = _db.Resources.FirstOrDefault(resource => resource.ResourceId == id);
      return View(thisResource);
    }

    [HttpPost]
    public ActionResult Edit(Resource resource)
    {
      _db.Entry(resource).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", "Section", new { id = resource.SectionId });
    }

    public ActionResult Delete(int id)
    {
      var thisResource = _db.Resources.FirstOrDefault(resource => resource.ResourceId == id);
      return View(thisResource);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisResource = _db.Resources.FirstOrDefault(resource => resource.ResourceId == id);
      _db.Resources.Remove(thisResource);
      _db.SaveChanges();
      return RedirectToAction("Details", "Section", new { id = thisResource.SectionId });
    }
  }
}