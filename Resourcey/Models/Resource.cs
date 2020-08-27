using System;
using System.Collections.Generic;
using Resourcey.Enums;

namespace Resourcey.Models
{
  public class Resource
  {
    public string Name { get; set; }
    public int ResourceId { get; set; }
    public DateTime Date { get; set; }
    public string ResourceUrl { get; set; }
    public ResourceType Type { get; set; }
    public string Description { get; set; }
    public int SectionId { get; set; }
    public virtual Section Section { get; set; }
    public virtual ApplicationUser ResourceCreator {get; set;}
  }
}