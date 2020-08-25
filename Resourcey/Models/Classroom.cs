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
    public string CourseName {get; set;}
    public int ClassroomId {get; set;}
    public string ClassroomCreator {get; set;}
    public ICollection<Section> Sections {get;}



  }
}