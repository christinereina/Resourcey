using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Resourcey.Models
{
  public class Classroom
  {
    public Classroom()
    {
      this.Sections = new HashSet<Section>();
    }
    [Required]
    public string CourseName {get; set;}
    public int ClassroomId {get; set;}
    public virtual ApplicationUser ClassroomCreator {get; set;}
    public ICollection<Section> Sections {get;}
  }
}